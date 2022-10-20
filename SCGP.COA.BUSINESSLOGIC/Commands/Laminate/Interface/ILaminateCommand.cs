using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Laminate.Interface
{
    public interface ILaminateCommand
    {
        public SearchResModel<DataLabModel> SearchDisplayLab(SearchReqModel<DataLabModel> req);
        public List<MasterLabDataModel> GetMasterLabData();

        public List<DataLabModel> SetExcelToDataTable(List<DataLabModel> dataLabModels);
        public FileDataModel PrintExportExcel();
    }
}
