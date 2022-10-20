using System;
using System.ComponentModel.DataAnnotations;

namespace SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization
{
    public class REFRESH_TOKEN
    {
        [Key]
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        [Required]
        [StringLength(255)]
        public string Token { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public bool DeleteFlag { get; private set; }

        public REFRESH_TOKEN()
        {
            Id = Guid.NewGuid();
        }

        public static REFRESH_TOKEN Create(Guid userId, string token, DateTime expire)
        {
            return new REFRESH_TOKEN()
            {
                UserId = userId,
                Token = token,
                CreatedDate = DateTime.Now,
                ExpireDate = expire,
                DeleteFlag = false
            };
        }

        public void Delete()
        {
            DeleteFlag = true;
        }
    }
}
