using SCGP.COA.COMMON.Attributes;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Entities.CoaSkicPM1to3;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Repositories.CoaSkicPM1to3.Interface;

namespace SCGP.COA.DATAACCESS.Repositories.CoaSkicPM1to3
{
    [ScopedRegistration]
    public class CoaFormRepository : RepositoryBaseShare<COA_Form>, ICoaFormRepository
    {
        public CoaSkicPM1to3Context _context;
        public CoaFormRepository(CoaSkicPM1to3Context context) : base(context, context)
        {
            _context = context;
        }


    }
}
