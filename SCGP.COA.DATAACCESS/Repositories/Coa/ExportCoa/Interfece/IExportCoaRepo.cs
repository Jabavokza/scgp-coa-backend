using SCGP.COA.DATAACCESS.Entities.Coa;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.ExportCoa.Interfece
{
    public interface IExportCoaRepo
    {
        public bool SetDtBatchToDtTblRepo(List<ConvertingBatchDatum> request);
    }
}
