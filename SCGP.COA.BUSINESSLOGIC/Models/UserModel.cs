using SCGP.COA.COMMON.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace SCGP.COA.BUSINESSLOGIC.Models
{
    public class UserModel
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? AccessFailedCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Domain { get; set; }
        public string? Organization { get; set; }
        public bool? LockoutEnabled { get; set; }
        public bool? MustChangePassword { get; set; }
        public string? LastChangePasswordDate { get; set; }
        public bool ActiveFlag { get; set; }
        public int? GroupId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(this.NormalizedUserName.Trim()))
                throw new BusinessException("Invalid UserName");

            if (string.IsNullOrEmpty(this.FirstName.Trim()))
                throw new BusinessException("Invalid First Name");

            if (string.IsNullOrEmpty(this.LastName.Trim()))
                throw new BusinessException("Invalid Last Name");

            if (string.IsNullOrEmpty(this.Email.Trim()) || !new EmailAddressAttribute().IsValid(this.Email))
                throw new BusinessException("Invalid Email");

            if (string.IsNullOrEmpty(this.Domain.Trim()))
                throw new BusinessException("Invalid Domain");
        }
    }

    public class UserCreateResultModel
    {
        public string UserName { get; set; }
    }

    public class GetUserDetailQuery
    {
        public string UserName { get; set; }
    }
}
