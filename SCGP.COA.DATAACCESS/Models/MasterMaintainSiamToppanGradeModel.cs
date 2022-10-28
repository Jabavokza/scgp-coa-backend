using SCGP.COA.COMMON.Exceptions;

namespace SCGP.COA.DATAACCESS.Models
{
    public class MasterMaintainSiamToppanGradeSearchResultModel
    {
        public int SiamToppanGradeId { get; set; }
        public string Grade { get; set; }
        public decimal Gram { get; set; }
        //public string MaterialSale { get; set; }
        public string SiamToppanNumber { get; set; }
        public string? Remark { get; set; }
    }

    public class MasterMaintainSiamToppanGradeModel
    {
        public int SiamToppanGradeId { get; set; }
        public string Grade { get; set; }
        public decimal Gram { get; set; }
        //public string MaterialSale { get; set; }
        public string SiamToppanNumber { get; set; }
        public string? Remark { get; set; }

        //public void Validate()
        //{
        //    if (string.IsNullOrEmpty(this.NormalizedUserName.Trim()))
        //        throw new BusinessException("Invalid UserName");

        //    if (string.IsNullOrEmpty(this.FirstName.Trim()))
        //        throw new BusinessException("Invalid First Name");

        //    if (string.IsNullOrEmpty(this.LastName.Trim()))
        //        throw new BusinessException("Invalid Last Name");

        //    if (string.IsNullOrEmpty(this.Email.Trim()) || !new EmailAddressAttribute().IsValid(this.Email))
        //        throw new BusinessException("Invalid Email");

        //    if (string.IsNullOrEmpty(this.Domain.Trim()))
        //        throw new BusinessException("Invalid Domain");
        //}
    }

    //public class GetMasterMaintainSiamToppanGradeQuery
    //{
    //    public int SiamToppanGradeId { get; set; }
    //}
}
