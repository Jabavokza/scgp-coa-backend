using SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface;
using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.BUSINESSLOGIC.Services.Interface;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Authentications;
using SCGP.COA.COMMON.Constants;
using SCGP.COA.COMMON.Exceptions;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using SCGP.COA.DATAACCESS.Models;
using SCGP.COA.DATAACCESS.Repositories.Coa.Authorization.Interface;
using System.Collections.Generic;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master
{
    [TransientRegistration]
    public class MasterUserCommand : IMasterUserCommand
    {
        private IUserRepository _userRepository;
        private IUserLocalService _userService;
        private IAuthrorizationService _authrorizationService;
        private IUserGroupRepository _userGroupRepository;
        private IGroupRepository _groupRepository;

        public MasterUserCommand(IUserRepository userRepository
            , IUserLocalService userService
            , IAuthrorizationService authrorizationService
            , IUserGroupRepository userGroupRepository
            , IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _userService = userService;
            _authrorizationService = authrorizationService;
            _userGroupRepository = userGroupRepository;
            _groupRepository = groupRepository;
        }

        public UserCreateResultModel CreateUser(UserModel request)
        {
            var res = new UserCreateResultModel();

            request.Validate();
           
            string? password = null;
            string? passwordEncrypt = null;
            string userName = request.NormalizedUserName?.ToLower();
            if (request.Domain == AppConstant.Domain.CEMENTHAI)
                userName += "@cementhai.co.th";

            var exist = _userRepository.Get(q => q.UserName == userName).FirstOrDefault();
            if (exist != null)
                throw new BusinessException("Username already exists");

            // random password for external user & set send email.
            if (request.Domain == AppConstant.Domain.EXTERNAL)
            {
                password = _authrorizationService.GenerateRandomPassword();
                passwordEncrypt = CryptoUtilities.sha256_hash(password);
            }

            MASTER_USER user = new MASTER_USER();
            user.Create(userName, request.NormalizedUserName?.ToLower(), request.Email, passwordEncrypt, request.PhoneNumber,
                request.FirstName, request.LastName, request.Domain, request.Organization, _userService.GetUserCredential()?.UserId ?? "admin");
            _userRepository.Add(user);

            if (request.GroupId.HasValue)
            {
                var group = MASTER_USER_GROUP.Create(user.UserId, request.GroupId.Value);
                _userGroupRepository.Add(group);
            }

            _userRepository.Commit();

            res.UserName = user.UserName;

            return res;
        }

        public SearchResModel<UserSearchResultModel> SearchUser(SearchReqModel<UserSearchCriterialModel> req)
        {
            var query = _userRepository.SearchUser(req.Criteria, _userService.GetUserCredential()?.IsAdmin ?? false);

            var res = new SearchResModel<UserSearchResultModel>()
            {
                PageIndex = req.PageIndex ?? 1,
                PageSize = req.PageSize ?? 10
            };
            var totalRecord = query.Count();
            var data = query.Page(res.PageSize, res.PageIndex).ToList();

            res.TotalRecord = totalRecord;
            res.Result = data;

            return res;
        }
    
        public  UserModel GetUser(GetUserDetailQuery req)
        {
            var user = _userRepository.Get(q => q.UserName == req.UserName).FirstOrDefault();
            if (user == null) throw new BusinessException("User : " + req.UserName + " is not found");
            var userModel = new UserModel();
            ObjectUtil.CopyProperties(user, userModel);

            var group = _userGroupRepository.FindActiveUserGroup(user.UserId).FirstOrDefault();
            userModel.GroupId = group?.GroupId;
            userModel.UpdatedBy = user.UpdatedBy ?? user.CreatedBy;
            userModel.UpdatedDate = user.UpdatedDate ?? user.CreatedDate;

            return userModel;
        }

        public void UpdateUser(UserModel userModel)
        {
            var login = _userService.GetUserCredential();

            var dbUser = _userRepository.Get(q => q.UserId == userModel.UserId).FirstOrDefault();
            if (dbUser == null)
                throw new BusinessException("User does not exist");

            dbUser.Update(userModel.Email, userModel.PhoneNumber, userModel.FirstName, userModel.LastName, userModel.ActiveFlag, userModel.Organization, login?.UserId);

            if (userModel.GroupId.HasValue)
            {
                var userGroups = _userGroupRepository.Query().Where(x => x.UserId == dbUser.UserId).ToList();
                _userGroupRepository.Delete(userGroups);

                var group = MASTER_USER_GROUP.Create(dbUser.UserId, userModel.GroupId.Value);
                _userGroupRepository.Add(group);
            }

            _userRepository.Commit();

        }

        public void SetInActive(UserSearchResultModel userModel)
        {
            var login = _userService.GetUserCredential();

            var dbUser = _userRepository.Get(q => q.UserId == userModel.UserId).FirstOrDefault();
            if (dbUser == null)
                throw new BusinessException("User does not exist");

            if (dbUser.ActiveFlag != true)
                throw new BusinessException("User has already inactive");

            // Delete
            dbUser.SetInactive(login.UserId);
            _userRepository.Commit();
        }

        public List<SelectItemModel<int>> GetGroups()
        {
            var res = _groupRepository.Read()
                .Where(x=>x.ActiveFlag)
                .Select(x=> new SelectItemModel<int>()
                {
                    Label = x.GroupName,
                    Name = x.GroupName,
                    Value = x.GroupId
                }).ToList();

            return res;
        }
    }
}
