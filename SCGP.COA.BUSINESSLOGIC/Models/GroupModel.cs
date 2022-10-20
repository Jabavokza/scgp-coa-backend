namespace SCGP.COA.BUSINESSLOGIC.Models
{
    public class GroupCreateResultModel
    {
        public int GroupId { get; set; }
    }

    public class GetGroupDetailQuery
    {
        public int GroupId { get; set; }
    }

    public class GroupDetailModel
    {
        public int? GroupId { get; set; }
        public string? GroupName { get; set; }
        public bool? ActiveFlag { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<MenuRoleModel>? MenuRoles { get; set; }
    }

    public class MenuRoleModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public List<RoleModel> Roles { get; set; }
    }

    public class RoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? IsSelect { get; set; }
    }

}
