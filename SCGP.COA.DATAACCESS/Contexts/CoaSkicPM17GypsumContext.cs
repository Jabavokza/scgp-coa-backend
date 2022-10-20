using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using SCGP.COA.DATAACCESS.Entities.CoaSkicPM17Gypsum;
using SCGP.COA.COMMON.Attributes;

namespace SCGP.COA.DATAACCESS.Contexts
{
    [ScopedPriorityRegistration]
    public class CoaSkicPM17GypsumContext : DbContext
    {
        public CoaSkicPM17GypsumContext(DbContextOptions<CoaSkicPM17GypsumContext> options) : base(options)
        {
        }

        protected CoaSkicPM17GypsumContext(DbContextOptions options) : base(options)
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

        public virtual DbSet<COA_Form> COA_Forms { get; set; }
    }
}