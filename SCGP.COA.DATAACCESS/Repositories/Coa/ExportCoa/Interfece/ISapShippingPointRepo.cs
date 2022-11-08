using SCGP.COA.DATAACCESS.Entities.Coa;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.ExportCoa.Interfece
{
    public interface ISapShippingPointRepo
    {
        public IQueryable<SAPShippingPoint> GetShippingPoints();
    }
}
