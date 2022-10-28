using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
using SCGP.COA.DATAACCESS.Repositories.Coa.Authorization;
using SCGP.COA.DATAACCESS.Repositories.Coa.Authorization.Interface;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master
{
    [TransientRegistration]
    public class MasterGroupCommand : IMasterGroupCommand
    {
        private IUserLocalService _userService;
        private IGroupRepository _groupRepository;
        private IGroupRoleRepository _groupRoleRepository;
        private IMenuRoleRepository _menuRoleRepository;

        public MasterGroupCommand(
            IUserLocalService userService
            , IGroupRepository groupRepository
            , IGroupRoleRepository groupRoleRepository
            , IMenuRoleRepository menuRoleRepository)
        {
            _userService = userService;
            _groupRepository = groupRepository;
            _groupRoleRepository = groupRoleRepository;
            _menuRoleRepository = menuRoleRepository;
        }

        public GroupCreateResultModel CreateGroup(GroupDetailModel groupModel)
        {
            var res = new GroupCreateResultModel();

            if (groupModel.GroupName.IsNullOrEmpty())
                throw new BusinessException("Group Name is required");

            var exist = _groupRepository.Get(q => q.GroupName == groupModel.GroupName).FirstOrDefault();
            if (exist != null)
                throw new BusinessException("Group Name already exists");

            MASTER_GROUP group;
            var user = _userService.GetUserCredential();
            group = MASTER_GROUP.Create(groupModel.GroupName, groupModel.ActiveFlag ?? false, user?.UserId);
            _groupRepository.Add(group);

            ModifyRole(groupModel, group);

            _groupRepository.Commit();
            res.GroupId = group.GroupId;
            return res;
        }

        private void ModifyRole(GroupDetailModel groupModel,MASTER_GROUP group)
        {
            var roleModels = groupModel.MenuRoles.SelectMany(q => q.Roles).Where(x => x.IsSelect == true).Distinct().ToList();
            foreach (var roleModel in roleModels)
            {
                if (!group.GROUP_ROLEs.Any(q => q.RoleId == roleModel.RoleId))
                {
                    var groupRole = MASTER_GROUP_ROLE.Create(roleModel.RoleId, group);
                    _groupRoleRepository.Add(groupRole);
                    group.GROUP_ROLEs.Add(groupRole);
                }
            }
            var removeRoles = group.GROUP_ROLEs.Where(q => !roleModels.Any(r => r.RoleId == q.RoleId)).ToList();
            foreach (var removeRole in removeRoles)
            {
                group.GROUP_ROLEs.Remove(removeRole);
                _groupRoleRepository.Delete(removeRole);
            }

        }

        public SearchResModel<GroupSearchResultModel> SearchGroup(SearchReqModel<GroupSearchCriterialModel> req)
        {
            var query = _groupRepository.SearchGroup(req.Criteria, _userService.GetUserCredential()?.IsAdmin ?? false);

            var res = new SearchResModel<GroupSearchResultModel>()
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
    
        public  GroupDetailModel GetGroup(GetGroupDetailQuery req)
        {
            var group = _groupRepository.Get(q => q.GroupId == req.GroupId).FirstOrDefault();
            if (group == null) throw new BusinessException("Group : " + req.GroupId + " is not found");
            var groupModel = new GroupDetailModel();
            ObjectUtil.CopyProperties(group, groupModel);
            groupModel.UpdatedBy = group.UpdatedBy ?? group.CreatedBy;
            groupModel.UpdatedDate = group.UpdatedDate ?? group.CreatedDate;

            groupModel.MenuRoles = GetMenuRole();
            var groupRoles = _groupRoleRepository.Get(q => q.GroupId == group.GroupId).ToList();
            groupModel.MenuRoles.ForEach(m =>
            {
                m.Roles.ForEach(r =>
                {
                    var matchRole = groupRoles.Any(x => x.RoleId == r.RoleId);
                    if (matchRole)
                    {
                        r.IsSelect = true;
                    }
                });
            });

            return groupModel;
        }

        public void UpdateGroup(GroupDetailModel groupModel)
        {
            var res = new GroupCreateResultModel();

            if (groupModel.GroupName.IsNullOrEmpty())
                throw new BusinessException("Group Name is required");

            var user = _userService.GetUserCredential();
            MASTER_GROUP? group = _groupRepository.Get(q => q.GroupId == groupModel.GroupId).FirstOrDefault();
            if (group == null)
                throw new BusinessException("Group does not exist");

            group.GROUP_ROLEs = _groupRoleRepository.Get(q => q.GroupId == group.GroupId).ToList();

            group.Update(groupModel.GroupName, groupModel.ActiveFlag ?? false, user?.UserId);

            ModifyRole(groupModel, group);

            _groupRepository.Commit();

        }

        public void SetInActive(GroupSearchResultModel groupModel)
        {
            var login = _userService.GetUserCredential();

            var group = _groupRepository.Get(q => q.GroupId == groupModel.GroupId).FirstOrDefault();
            if (group == null)
                throw new BusinessException("Group not exist");

            if (group.ActiveFlag != true)
                throw new BusinessException("Group has already inactive");

            // Delete
            group.SetInactive(login.UserId);
            _groupRepository.Commit();
        }

        public List<MenuRoleModel> GetMenuRole()
        {
            var menuRoles = new List<MenuRoleModel>();
            var dbMenuRoles = _menuRoleRepository.Read().Where(q => q.MENU.ActiveFlag && q.ROLE.ActiveFlag)
                .Include(x => x.MENU).Include(x => x.ROLE).ToList();
            var menus = dbMenuRoles.Where(q => q.MENU.ParentMenu == null).Select(q => q.MENU).OrderBy(q => q.MenuId).Distinct().ToList();
            foreach (var menu in menus)
            {
                MenuRoleModel groupMenuRole = new MenuRoleModel
                {
                    MenuId = menu.MenuId,
                    MenuName = menu.MenuName,
                    Roles = new List<RoleModel>()
                };
                var roles = dbMenuRoles.Where(q => q.MenuId == menu.MenuId).ToList();
                foreach (var role in roles)
                {
                    groupMenuRole.Roles.Add(new RoleModel()
                    {
                        IsSelect = false,
                        RoleId = role.RoleId,
                        RoleName = role.ROLE.RoleName,
                    });
                }
                menuRoles.Add(groupMenuRole);
            }

            return menuRoles;
        }
    }
}
