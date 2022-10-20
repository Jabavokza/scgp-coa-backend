using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using SCGP.COA.COMMON.Attributes;

namespace SCGP.COA.DATAACCESS.Contexts
{
    [ScopedPriorityRegistration]
    public class CoaSkicPM17KraftContext : DbContext
    {
        public CoaSkicPM17KraftContext(DbContextOptions<CoaSkicPM17KraftContext> options) : base(options)
        {
        }

        protected CoaSkicPM17KraftContext(DbContextOptions options) : base(options)
        {
        }

        public DbConnection DbConnenct => this.Database.GetDbConnection();

        public void RefreshAll()
        {
            foreach (var entity in this.ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }

        //public DbSet<MASTER_USER> MASTER_USERS { get; set; }
    }
}