using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using SCGP.COA.BUSINESSLOGIC.Commands.Interface;
using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.BUSINESSLOGIC.Services.Interface;
using SCGP.COA.COMMON;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Authentications;
using SCGP.COA.COMMON.Constants;
using SCGP.COA.COMMON.Exceptions;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Repositories.Coa.Authorization.Interface;
using System.Linq;
using static SCGP.COA.COMMON.Constants.AppConstant;

namespace SCGP.COA.BUSINESSLOGIC.Commands
{
    [TransientRegistration]
    public class AuthrorizationCommand : IAuthrorizationCommand
    {
        private IUserRepository _userRepository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private AppSettings _appSettings;
        private IGroupRepository _groupRepository;
        private IGroupRoleRepository _groupRoleRepository;
        private IAuthrorizationService _authrorizationService;
        private IUserGroupRepository _userGroupRepository;
        private IUserLocalService _userLocalService;
        private IMenuRoleRepository _menuRoleRepository;

        public AuthrorizationCommand(IUserRepository userRepository
            , IRefreshTokenRepository refreshTokenRepository
            , AppSettings appSettings
            , IGroupRepository groupRepository
            , IGroupRoleRepository groupRoleRepository
            , IAuthrorizationService authrorizationService
            , IUserGroupRepository userGroupRepository
            , IUserLocalService userLocalService
            , IMenuRoleRepository menuRoleRepository)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _appSettings = appSettings;
            _groupRepository = groupRepository;
            _groupRoleRepository = groupRoleRepository;
            _authrorizationService = authrorizationService;
            _userGroupRepository = userGroupRepository;
            _userLocalService = userLocalService;
            _menuRoleRepository = menuRoleRepository;
        }


        public TokenModel Token(TokenQuery request)
        {
            MASTER_USER user = null;

            if (request.grant_type == "password")
            {
                user = _authrorizationService.Login(request);
            }
            else if (request.grant_type == "refresh_token")
            {
                if (string.IsNullOrEmpty(request.refresh_token))
                    throw new Exception("refresh_token is required");
                var dbRefreshToken = _refreshTokenRepository.Get(q => q.Token == request.refresh_token).FirstOrDefault();
                if (dbRefreshToken == null)
                    throw new Exception("refresh_token is invalid");
                else if (dbRefreshToken.ExpireDate <= DateTime.Now)
                    throw new Exception("refresh_token expired");

                user = _userRepository.FindActiveByUserId(dbRefreshToken.UserId);
            }
            else
            {
                throw new Exception("Unsupported grant_type");
            }

            if (user == null)
                throw new Exception("User does not exist");

            var userGroupIds = _userGroupRepository.Read().Where(x => x.UserId == user.UserId).Select(x => x.GroupId).ToList();
            var groups = _groupRepository.Read().Where(x => userGroupIds.Contains(x.GroupId)).ToList();
            var group = groups.FirstOrDefault(x => x.ActiveFlag);

            if (group == null)
            {
                throw new Exception("User Group does not maintain,Please contact admin");
            }

            // Generate JWT
            REFRESH_TOKEN refreshToken = null;
            var credential = new UserCredential();
            var jwtExpireSec = _appSettings.JwtExpireMinutes * 60;
            var refreshTokenExpireSec = _appSettings.RefreshTokenExpireMinutes * 60;
            credential.Expires = DateTime.Now.AddSeconds(jwtExpireSec);

            credential.UserId = user.UserName;
            credential.UserName = user.NormalizedUserName;
            credential.Domain = user.Domain;
            credential.Email = user.Email;
            credential.Name = $"{user.FirstName} {user.LastName}";
            credential.FirstName = user.FirstName;
            credential.LastName = user.LastName;
            credential.PhoneNumber = user.PhoneNumber;
            credential.GroupId = group.GroupId;
            credential.GroupName = group.GroupName;
            credential.Roles = _groupRoleRepository.Get(q => q.GroupId == group.GroupId).Include(q => q.ROLE)
                                .Select(x => x.ROLE).Where(x => x.ActiveFlag).Select(q => q.RoleName).ToList();
            credential.Organization = user.Organization;
            credential.IsAdmin = group.IsAdmin;

            // Generate Refresh Token
            refreshToken = REFRESH_TOKEN.Create(user.UserId
                , _authrorizationService.RandomString(25)
                , DateTime.Now.AddSeconds(refreshTokenExpireSec));
            _refreshTokenRepository.Add(refreshToken);

            _refreshTokenRepository.Commit();

            var jwt = _authrorizationService.GenerateJWT(credential);

            TokenModel token = new TokenModel
            {
                token_type = "bearer",
                access_token = jwt,
                expires_in = jwtExpireSec,
                refresh_token = refreshToken?.Token,
                refresh_token_expires_in = refreshTokenExpireSec
            };

            return token;
        }

        public List<MenuTree> GetMyMenu()
        {
            var user = _userLocalService.GetUserCredential();
            if (user == null)
                throw new BusinessException("Unauthenticated");

            var roleIds = _groupRoleRepository.Get(q => q.GroupId == user.GroupId && q.ROLE.ActiveFlag).Select(q => q.RoleId).ToList();
            var menus = _menuRoleRepository.Get(q => roleIds.Contains(q.RoleId) && q.MENU.ActiveFlag).Select(q => q.MENU).Distinct().ToList();
            var menuTree = new List<MenuTree>();
            menus.Where(q => q.ParentMenu == null)
                .ToList()
                .ForEach(q =>
                {
                    var m = new MenuTree()
                    {
                        Id = q.MenuId,
                        Name = q.MenuName,
                        Action = q.Action,
                        Icon = q.Icon,
                        Level = q.Level,
                    };
                    menuTree.Add(m);
                });
            SetMenuChildren(menuTree, menus);
            return menuTree;
        }

        private void SetMenuChildren(List<MenuTree> parent, List<MASTER_MENU> allMenus)
        {
            foreach (var menu in parent)
            {
                menu.Items = new List<MenuTree>();
                allMenus.Where(q => q.ParentMenu == menu.Id)
                    .ToList()
                    .ForEach(q =>
                    {
                        var m = new MenuTree()
                        {
                            Id = q.MenuId,
                            Name = q.MenuName,
                            Action = q.Action,
                            Icon = q.Icon,
                            Level = q.Level,
                        };
                        menu.Items.Add(m);
                    });
                if (menu.Items.Count > 0)
                {
                    menu.ShowItems = true;
                    SetMenuChildren(menu.Items, allMenus);
                }
            }
        }

    }
}
