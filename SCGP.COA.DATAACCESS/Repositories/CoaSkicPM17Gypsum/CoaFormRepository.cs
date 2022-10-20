using SCGP.COA.COMMON.Attributes;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Entities.CoaSkicPM17Gypsum;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Repositories.CoaSkicPM17Gypsum.Interface;
                                                                                                                                                                                                                                                                                                
namespace SCGP.COA.DATAACCESS.Repositories.CoaSkicPM17Gypsum
{
    [ScopedRegistration]

    public class CoaFormRepository : RepositoryBaseShare<COA_Form>, ICoaFormRepository
    {
        public CoaSkicPM17GypsumContext _context;
        public CoaFormRepository(CoaSkicPM17GypsumContext context) : base(context, context)
        {
            _context = context;
        }
    }
}
