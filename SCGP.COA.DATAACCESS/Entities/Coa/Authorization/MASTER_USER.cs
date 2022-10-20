using SCGP.COA.COMMON;
using SCGP.COA.COMMON.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization
{
    [Table("MASTER_USER")]
    public class MASTER_USER
    {
        public MASTER_USER()
        {
            UserId = Guid.NewGuid();
        }

        [Key]
        public Guid UserId { get; private set; }

        [Required]
        [StringLength(128)]
        public string UserName { get; private set; }

        [Required]
        [StringLength(128)]
        public string NormalizedUserName { get; private set; }

        [Required]
        [StringLength(128)]
        public string Email { get; private set; }

        [StringLength(256)]
        public string? PasswordEncrypt { get; private set; }

        [StringLength(50)]
        public string? PhoneNumber { get; private set; }

        public int? AccessFailedCount { get; private set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; private set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; private set; }

        [StringLength(255)]
        public string? Domain { get; private set; }

        public bool? LockoutEnabled { get; private set; }

        public bool? MustChangePassword { get; private set; }

        public DateTime? LastChangePasswordDate { get; private set; }

        [StringLength(255)]
        public string? Organization { get; private set; }

        public bool ActiveFlag { get; private set; } = true;

        public DateTime CreatedDate { get; private set; } = DateTime.Now;

        [Required]
        [StringLength(128)]
        public string CreatedBy { get; private set; }

        public DateTime? UpdatedDate { get; private set; } = DateTime.Now;

        [StringLength(128)]
        public string? UpdatedBy { get; private set; }


        public void Create(string userName, string normalizedUserName, string email, string password, string phoneNumber
                                , string firstName, string lastName, string domain, string org, string createdBy)
        {
            UserName = userName;
            NormalizedUserName = normalizedUserName;
            Email = email;
            PasswordEncrypt = password;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            AccessFailedCount = 0;
            Domain = domain;
            Organization = org;
            CreatedBy = createdBy;
            UpdatedBy = createdBy;
            MustChangePassword = domain == AppConstant.Domain.EXTERNAL;

        }

        public static MASTER_USER CreateGuest(string username)
        {
            return new MASTER_USER()
            {
                UserName = username,
                NormalizedUserName = username,
                FirstName = username,
                LastName = "",
                Email = ""
            };
        }

        public void Update(string email, string phoneNumber, string firstName, string lastName, bool activeFlag, string org, string updatedBy)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            ActiveFlag = activeFlag;
            Organization = org;
            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.Now;
        }

        public void ChangePassword(string password, DateTime changeDate, string updatedBy)
        {
            PasswordEncrypt = password;
            MustChangePassword = false;
            LastChangePasswordDate = changeDate;
            UpdatedDate = DateTime.Now;
            UpdatedBy = updatedBy;
        }

        public void AddAccessFailedCount()
        {
            AccessFailedCount = AccessFailedCount + 1;
            UpdatedDate = DateTime.Now;
        }

        public void ResetAccessFailedCount()
        {
            AccessFailedCount = 0;
        }

        public void ValidateMustChangePassword()
        {
            if (Domain == AppConstant.Domain.EXTERNAL)
            {
                var passwordAge = 75;
                if (LastChangePasswordDate == null
                    || LastChangePasswordDate.Value.AddDays(passwordAge) < DateTime.Now)
                {
                    MustChangePassword = true;
                }
            }
        }

        public void SetInactive(string updatedBy)
        {
            ActiveFlag = false;
            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.Now;
        }
    }
}
