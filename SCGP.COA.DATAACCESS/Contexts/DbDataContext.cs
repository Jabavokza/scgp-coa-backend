using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using static SCGP.COA.COMMON.Constants.AppConstant;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using System.Text.RegularExpressions;
using SCGP.COA.DATAACCESS.Entities.Coa;

namespace SCGP.COA.DATAACCESS.Contexts
{
    [ScopedPriorityRegistration]
    public class DbDataContext : DbContext
    {
        public DbDataContext()
        {
        }

        public DbDataContext(DbContextOptions<DbDataContext> options) : base(options)
        {
        }

        protected DbDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbConnection DbConnenct => Database.GetDbConnection();

        public void RefreshAll()
        {
            foreach (var entity in ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MASTER_GROUP_ROLE>().HasKey(x => new { x.GroupId, x.RoleId });
            modelBuilder.Entity<MASTER_USER_GROUP>().HasKey(x => new { x.UserId, x.GroupId });
            modelBuilder.Entity<MASTER_MENU_ROLE>().HasKey(x => new { x.MenuId, x.RoleId });
            modelBuilder.Entity<MASTER_USER>().Property(x => x.ActiveFlag).HasDefaultValue(true);
            modelBuilder.Entity<MASTER_USER>().HasIndex(x => x.UserName);
            modelBuilder.Entity<MASTER_USER>().HasIndex(x => new { x.NormalizedUserName, x.Domain });
            modelBuilder.Entity<MASTER_ROLE>().Property(x => x.ActiveFlag).HasDefaultValue(true);
            modelBuilder.Entity<MASTER_GROUP>().Property(x => x.ActiveFlag).HasDefaultValue(true);
            modelBuilder.Entity<MASTER_GROUP>().Property(x => x.IsAdmin).HasDefaultValue(false);
            modelBuilder.Entity<MASTER_MENU>().Property(x => x.ActiveFlag).HasDefaultValue(true);
            modelBuilder.Entity<REFRESH_TOKEN>().HasIndex(x => x.Token);



            modelBuilder.Entity<LogCoa>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__LOG_COA__4364C8829DEE2B6E");

                entity.ToTable("LOG_COA");

                entity.Property(e => e.LogId).HasColumnName("LOG_ID");

                entity.Property(e => e.DocumentNumber)
                    .HasMaxLength(20)
                    .HasColumnName("DOCUMENT_NUMBER");

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(20)
                    .HasColumnName("DOCUMENT_TYPE");

                entity.Property(e => e.LogTimestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("LOG_TIMESTAMP");

                entity.Property(e => e.Message).HasColumnName("MESSAGE");

                entity.Property(e => e.OutputType)
                    .HasMaxLength(20)
                    .HasColumnName("OUTPUT_TYPE");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("STATUS");
            });

            modelBuilder.Entity<MasterAutoCoaCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerCode)
                    .HasName("PK__MASTER_A__8E71B5A88F3F4AF1");

                entity.ToTable("MASTER_AUTO_COA_CUSTOMER");

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_CODE")
                    .IsFixedLength();

                entity.Property(e => e.AutocoaActive).HasColumnName("AUTOCOA_ACTIVE");

                entity.Property(e => e.ShiptoCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SHIPTO_CODE")
                    .IsFixedLength();
            });

            modelBuilder.Entity<MasterCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerCode)
                    .HasName("PK__MASTER_C__8E71B5A803ABB44D");

                entity.ToTable("MASTER_CUSTOMER");

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_CODE")
                    .IsFixedLength();

                entity.Property(e => e.CoaFooterText)
                    .HasColumnName("COA_FOOTER_TEXT")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(100)
                    .HasColumnName("CUSTOMER_NAME")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultOutputDp).HasColumnName("DEFAULT_OUTPUT_DP");

                entity.Property(e => e.DefaultOutputDpBarcode).HasColumnName("DEFAULT_OUTPUT_DP_BARCODE");

                entity.Property(e => e.DefaultOutputExcel).HasColumnName("DEFAULT_OUTPUT_EXCEL");

                entity.Property(e => e.DefaultOutputPdf).HasColumnName("DEFAULT_OUTPUT_PDF");

                entity.Property(e => e.DefaultOutputText).HasColumnName("DEFAULT_OUTPUT_TEXT");
            });

            modelBuilder.Entity<MasterForm>(entity =>
            {
                entity.HasKey(e => e.FormId)
                    .HasName("PK__MASTER_F__85052F68235A6657");

                entity.ToTable("MASTER_FORM");

                entity.Property(e => e.FormId).HasColumnName("FORM_ID");

                entity.Property(e => e.FormName)
                    .HasMaxLength(20)
                    .HasColumnName("FORM_NAME");

                entity.Property(e => e.FormTemplateId).HasColumnName("FORM_TEMPLATE_ID");

                entity.Property(e => e.Property01Id).HasColumnName("PROPERTY01_ID");

                entity.Property(e => e.Property02Id).HasColumnName("PROPERTY02_ID");

                entity.Property(e => e.Property03Id).HasColumnName("PROPERTY03_ID");

                entity.Property(e => e.Property04Id).HasColumnName("PROPERTY04_ID");

                entity.Property(e => e.Property05Id).HasColumnName("PROPERTY05_ID");

                entity.Property(e => e.Property06Id).HasColumnName("PROPERTY06_ID");

                entity.Property(e => e.Property07Id).HasColumnName("PROPERTY07_ID");

                entity.Property(e => e.Property08Id).HasColumnName("PROPERTY08_ID");

                entity.Property(e => e.Property09Id).HasColumnName("PROPERTY09_ID");

                entity.Property(e => e.Property10Id).HasColumnName("PROPERTY10_ID");

                entity.Property(e => e.Property11Id).HasColumnName("PROPERTY11_ID");

                entity.Property(e => e.Property12Id).HasColumnName("PROPERTY12_ID");

                entity.Property(e => e.Property13Id).HasColumnName("PROPERTY13_ID");

                entity.Property(e => e.Property14Id).HasColumnName("PROPERTY14_ID");

                entity.Property(e => e.Property15Id).HasColumnName("PROPERTY15_ID");

                entity.Property(e => e.Property16Id).HasColumnName("PROPERTY16_ID");

                entity.Property(e => e.Property17Id).HasColumnName("PROPERTY17_ID");

                entity.Property(e => e.Property18Id).HasColumnName("PROPERTY18_ID");

                entity.Property(e => e.Property19Id).HasColumnName("PROPERTY19_ID");

                entity.Property(e => e.Property20Id).HasColumnName("PROPERTY20_ID");
            });

            modelBuilder.Entity<MasterFormFooter>(entity =>
            {
                entity.HasKey(e => e.FormId)
                    .HasName("PK__MASTER_F__85052F689EF24B51");

                entity.ToTable("MASTER_FORM_FOOTER");

                entity.Property(e => e.FormId)
                    .ValueGeneratedNever()
                    .HasColumnName("FORM_ID");

                entity.Property(e => e.TextAdditional1).HasColumnName("TEXT_ADDITIONAL1");

                entity.Property(e => e.TextAdditional2).HasColumnName("TEXT_ADDITIONAL2");

                entity.Property(e => e.TextApprovedby).HasColumnName("TEXT_APPROVEDBY");

                entity.Property(e => e.TextPrintedby).HasColumnName("TEXT_PRINTEDBY");

                entity.Property(e => e.TextTelnumber).HasColumnName("TEXT_TELNUMBER");

                entity.Property(e => e.TextTestcondition).HasColumnName("TEXT_TESTCONDITION");
            });

            modelBuilder.Entity<MasterFormHeader>(entity =>
            {
                entity.HasKey(e => e.FormTemplateId)
                    .HasName("PK__MASTER_F__D1C4DC3E5591C006");

                entity.ToTable("MASTER_FORM_HEADER");

                entity.Property(e => e.FormTemplateId)
                    .ValueGeneratedNever()
                    .HasColumnName("FORM_TEMPLATE_ID");

                entity.Property(e => e.DatetimeFormat)
                    .HasMaxLength(30)
                    .HasColumnName("DATETIME_FORMAT");
            });

            modelBuilder.Entity<MasterFormMappingRule>(entity =>
            {
                entity.HasKey(e => e.RuleId)
                    .HasName("PK__MASTER_F__E103520CA4DBCEB5");

                entity.ToTable("MASTER_FORM_MAPPING_RULES");

                entity.Property(e => e.RuleId).HasColumnName("RULE_ID");

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_CODE")
                    .IsFixedLength();

                entity.Property(e => e.FormExcelId).HasColumnName("FORM_EXCEL_ID");

                entity.Property(e => e.FormPdfId).HasColumnName("FORM_PDF_ID");

                entity.Property(e => e.FormTextId).HasColumnName("FORM_TEXT_ID");

                entity.Property(e => e.Grade)
                    .HasMaxLength(3)
                    .HasColumnName("GRADE")
                    .IsFixedLength();

                entity.Property(e => e.Gram)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("GRAM");

                entity.Property(e => e.MaterialSale)
                    .HasMaxLength(1)
                    .HasColumnName("MATERIAL_SALE")
                    .IsFixedLength();

                entity.Property(e => e.RuleOrder).HasColumnName("RULE_ORDER");
            });
            modelBuilder.Entity<MasterProperty>(entity =>
            {
                entity.HasKey(e => e.PropertyId)
                    .HasName("PK__MASTER_P__DD51AF0B5412FDD2");

                entity.ToTable("MASTER_PROPERTY");

                entity.Property(e => e.PropertyId).HasColumnName("PROPERTY_ID");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .HasColumnName("DISPLAY_NAME");

                entity.Property(e => e.OutputFormat)
                    .HasMaxLength(50)
                    .HasColumnName("OUTPUT_FORMAT");

                entity.Property(e => e.OutputName)
                    .HasMaxLength(50)
                    .HasColumnName("OUTPUT_NAME");

                entity.Property(e => e.PropertyName)
                    .HasMaxLength(50)
                    .HasColumnName("PROPERTY_NAME");
            });

            modelBuilder.Entity<MasterFormTemplate>(entity =>
            {
                entity.HasKey(e => e.FormTemplateId)
                    .HasName("PK__MASTER_F__D1C4DC3E91F43A39");

                entity.ToTable("MASTER_FORM_TEMPLATE");

                entity.Property(e => e.FormTemplateId).HasColumnName("FORM_TEMPLATE_ID");

                entity.Property(e => e.FormTemplateName)
                    .HasMaxLength(20)
                    .HasColumnName("FORM_TEMPLATE_NAME");
            });
            modelBuilder.Entity<MasterSiamToppanGrade>(entity =>
            {
                entity.HasKey(e => e.SiamToppanGradeId)
                    .HasName("PK__MASTER_S__3B5452A6D3712AA1");

                entity.ToTable("MASTER_SIAM_TOPPAN_GRADE");

                entity.Property(e => e.SiamToppanGradeId).HasColumnName("SIAM_TOPPAN_GRADE_ID");

                entity.Property(e => e.Grade)
                    .HasMaxLength(3)
                    .HasColumnName("GRADE")
                    .IsFixedLength();

                entity.Property(e => e.Gram)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("GRAM");

                entity.Property(e => e.MaterialSale)
                    .HasMaxLength(1)
                    .HasColumnName("MATERIAL_SALE")
                    .IsFixedLength();

                entity.Property(e => e.Remarks)
                    .HasMaxLength(10)
                    .HasColumnName("REMARKS")
                    .IsFixedLength();

                entity.Property(e => e.SiamToppanNumber)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SIAM_TOPPAN_NUMBER")
                    .IsFixedLength();
            });
            modelBuilder.Entity<REFRESH_TOKEN>(entity =>
            {
                entity.ToTable("REFRESH_TOKENS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Token).HasMaxLength(255);
            });
            modelBuilder.Entity<ConvertingBatchDatum>(entity =>
            {
                entity.HasKey(e => e.Batch)
                    .HasName("PK__Converti__77F1BF27073C44BF");

                entity.ToTable("Converting_Batch_Data");

                entity.Property(e => e.Batch)
                    .HasMaxLength(10)
                    .HasColumnName("BATCH")
                    .IsFixedLength();

                entity.Property(e => e.FilmThickness).HasColumnName("FILM_THICKNESS");

                entity.Property(e => e.Grade)
                    .HasMaxLength(3)
                    .HasColumnName("GRADE")
                    .IsFixedLength();

                entity.Property(e => e.Gram)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("GRAM");

                entity.Property(e => e.Porosity).HasColumnName("POROSITY");

                entity.Property(e => e.ProductionDate)
                    .HasColumnType("date")
                    .HasColumnName("PRODUCTION_DATE");

                entity.Property(e => e.UploadedDatetime).HasColumnName("UPLOADED_DATETIME");
            });
        }

        #region Master

        public DbSet<MASTER_USER> MASTER_USERS { get; set; }
        public DbSet<MASTER_USER_GROUP> MASTER_USER_GROUPS { get; set; }
        public DbSet<MASTER_GROUP> MASTER_GROUPS { get; set; }
        public DbSet<MASTER_ROLE> MASTER_ROLES { get; set; }
        public DbSet<MASTER_GROUP_ROLE> MASTER_GROUP_ROLES { get; set; }
        public DbSet<MASTER_MENU> MASTER_MENUS { get; set; }
        public DbSet<MASTER_MENU_ROLE> MASTER_MENU_ROLES { get; set; }
        public DbSet<REFRESH_TOKEN> REFRESH_TOKENS { get; set; }

        public virtual DbSet<LogCoa> LogCoas { get; set; } = null!;
        public virtual DbSet<MasterAutoCoaCustomer> MasterAutoCoaCustomers { get; set; } = null!;
        public virtual DbSet<MasterCustomer> MasterCustomers { get; set; } = null!;
        public virtual DbSet<MasterForm> MasterForms { get; set; } = null!;
        public virtual DbSet<MasterFormFooter> MasterFormFooters { get; set; } = null!;
        public virtual DbSet<MasterFormHeader> MasterFormHeaders { get; set; } = null!;
        public virtual DbSet<MasterFormMappingRule> MasterFormMappingRules { get; set; } = null!;
        public virtual DbSet<MasterFormTemplate> MasterFormTemplates { get; set; } = null!;
        public virtual DbSet<MasterProperty> MasterProperties { get; set; } = null!;
        public virtual DbSet<MasterSiamToppanGrade> MasterSiamToppanGrades { get; set; } = null!;
        public virtual DbSet<ConvertingBatchDatum> ConvertingBatchData { get; set; } = null!;
        #endregion Master

        #region Running number

        //public virtual DbSet<RUNNING_NUMBER_CONFIG> RUNNING_NUMBER_CONFIGs { get; set; }
        //public virtual DbSet<RUNNING_NUMBER_SERIES> RUNNING_NUMBER_SERIESs { get; set; }

        #endregion Running number

        #region Log

        //public virtual DbSet<LOG_INTERFACE> LOG_INTERFACE { get; set; }
        //public virtual DbSet<CONFIG_SYSTEM> CONFIG_SYSTEM { get; set; }

        #endregion Log
    }
}