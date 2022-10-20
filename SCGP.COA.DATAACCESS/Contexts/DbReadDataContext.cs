using Microsoft.EntityFrameworkCore;
using SCGP.COA.COMMON.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGP.COA.DATAACCESS.Contexts
{
    [ScopedPriorityRegistration]
    public class DbReadDataContext : DbDataContext
    {
        public DbReadDataContext(DbContextOptions<DbReadDataContext> options) : base(options)
        { }
    }

}
