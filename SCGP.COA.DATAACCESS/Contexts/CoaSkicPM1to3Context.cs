using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using SCGP.COA.DATAACCESS.Entities.CoaSkicPM1to3;
using SCGP.COA.COMMON.Attributes;

namespace SCGP.COA.DATAACCESS.Contexts
{
    [ScopedPriorityRegistration]
    public class CoaSkicPM1to3Context : DbContext
    {
        public CoaSkicPM1to3Context(DbContextOptions<CoaSkicPM1to3Context> options) : base(options)
        {
        }

        protected CoaSkicPM1to3Context(DbContextOptions options) : base(options)
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
            modelBuilder.Entity<COA_Form>(entity =>
            {
                entity.HasNoKey();
            });

        }

        public virtual DbSet<COA_Form> COA_Forms { get; set; }
    }
}