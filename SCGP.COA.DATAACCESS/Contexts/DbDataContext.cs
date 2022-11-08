using Microsoft.EntityFrameworkCore;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using System.Data.Common;

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
            modelBuilder.Entity<SAPShippingPoint>().HasNoKey();



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

            modelBuilder.Entity<MasterMaintainAutoCoa>(entity =>
            {
                entity.HasKey(e => e.AutoCoaId)
                    .HasName("PK__MASTER_A__8E71B5A88F3F4AF1");

                entity.ToTable("MASTER_AUTO_COA_CUSTOMER");

                entity.Property(e => e.AutoCoaId)
                    .HasColumnName("AUTO_COA_ID");

                entity.Property(e => e.AutocoaActive).HasColumnName("AUTOCOA_ACTIVE");

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_CODE")
                    .IsFixedLength();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("IS_ACTIVE")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.ShipToCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SHIP_TO_CODE")
                    .IsFixedLength();
            });

            modelBuilder.Entity<MasterMaintainCustomerCoaOption>(entity =>
                {
                    entity.HasKey(e => e.CustomerCoaOptionId)
                        .HasName("PK__MASTER_C__8E71B5A803ABB44D");

                    entity.ToTable("MASTER_CUSTOMER_COA_OPTION");

                    entity.Property(e => e.CustomerCoaOptionId)
                        .HasColumnName("CUSTOMER_COA_OPTION_ID");

                    entity.Property(e => e.CoaFooterText)
                        .HasColumnName("COA_FOOTER_TEXT")
                        .HasDefaultValueSql("('')");

                    entity.Property(e => e.CustomerCode)
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnName("CUSTOMER_CODE")
                        .IsFixedLength();

                    entity.Property(e => e.CustomerName)
                        .HasMaxLength(100)
                        .HasColumnName("CUSTOMER_NAME")
                        .HasDefaultValueSql("('')");

                    entity.Property(e => e.DefaultOutputDp).HasColumnName("DEFAULT_OUTPUT_DP");

                    entity.Property(e => e.DefaultOutputDpBarcode).HasColumnName("DEFAULT_OUTPUT_DP_BARCODE");

                    entity.Property(e => e.DefaultOutputExcel).HasColumnName("DEFAULT_OUTPUT_EXCEL");

                    entity.Property(e => e.DefaultOutputPdf).HasColumnName("DEFAULT_OUTPUT_PDF");

                    entity.Property(e => e.DefaultOutputText).HasColumnName("DEFAULT_OUTPUT_TEXT");

                    entity.Property(e => e.IsActive)
                        .IsRequired()
                        .HasColumnName("IS_ACTIVE")
                        .HasDefaultValueSql("(CONVERT([bit],(0)))");
                });

            modelBuilder.Entity<MasterMaintainForm>(entity =>
            {
                entity.HasKey(e => e.FormId)
                    .HasName("PK__MASTER_F__85052F68235A6657");

                entity.ToTable("MASTER_FORM");

                entity.Property(e => e.FormId).HasColumnName("FORM_ID");

                entity.Property(e => e.FormName)
                    .HasMaxLength(20)
                    .HasColumnName("FORM_NAME");

                entity.Property(e => e.FormTemplateId).HasColumnName("FORM_TEMPLATE_ID");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("IS_ACTIVE")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

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

                entity.HasOne(d => d.FormTemplate)
                    .WithMany(p => p.MasterForms)
                    .HasForeignKey(d => d.FormTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_FORM_TEMPLATE");

                entity.HasOne(d => d.Property01)
                    .WithMany(p => p.MasterFormProperty01s)
                    .HasForeignKey(d => d.Property01Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY1");

                entity.HasOne(d => d.Property02)
                    .WithMany(p => p.MasterFormProperty02s)
                    .HasForeignKey(d => d.Property02Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY2");

                entity.HasOne(d => d.Property03)
                    .WithMany(p => p.MasterFormProperty03s)
                    .HasForeignKey(d => d.Property03Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY3");

                entity.HasOne(d => d.Property04)
                    .WithMany(p => p.MasterFormProperty04s)
                    .HasForeignKey(d => d.Property04Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY4");

                entity.HasOne(d => d.Property05)
                    .WithMany(p => p.MasterFormProperty05s)
                    .HasForeignKey(d => d.Property05Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY5");

                entity.HasOne(d => d.Property06)
                    .WithMany(p => p.MasterFormProperty06s)
                    .HasForeignKey(d => d.Property06Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY6");

                entity.HasOne(d => d.Property07)
                    .WithMany(p => p.MasterFormProperty07s)
                    .HasForeignKey(d => d.Property07Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY7");

                entity.HasOne(d => d.Property08)
                    .WithMany(p => p.MasterFormProperty08s)
                    .HasForeignKey(d => d.Property08Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY8");

                entity.HasOne(d => d.Property09)
                    .WithMany(p => p.MasterFormProperty09s)
                    .HasForeignKey(d => d.Property09Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY9");

                entity.HasOne(d => d.Property10)
                    .WithMany(p => p.MasterFormProperty10s)
                    .HasForeignKey(d => d.Property10Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY10");

                entity.HasOne(d => d.Property11)
                    .WithMany(p => p.MasterFormProperty11s)
                    .HasForeignKey(d => d.Property11Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY11");

                entity.HasOne(d => d.Property12)
                    .WithMany(p => p.MasterFormProperty12s)
                    .HasForeignKey(d => d.Property12Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY12");

                entity.HasOne(d => d.Property13)
                    .WithMany(p => p.MasterFormProperty13s)
                    .HasForeignKey(d => d.Property13Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY13");

                entity.HasOne(d => d.Property14)
                    .WithMany(p => p.MasterFormProperty14s)
                    .HasForeignKey(d => d.Property14Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY14");

                entity.HasOne(d => d.Property15)
                    .WithMany(p => p.MasterFormProperty15s)
                    .HasForeignKey(d => d.Property15Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY15");

                entity.HasOne(d => d.Property16)
                    .WithMany(p => p.MasterFormProperty16s)
                    .HasForeignKey(d => d.Property16Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY16");

                entity.HasOne(d => d.Property17)
                    .WithMany(p => p.MasterFormProperty17s)
                    .HasForeignKey(d => d.Property17Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY17");

                entity.HasOne(d => d.Property18)
                    .WithMany(p => p.MasterFormProperty18s)
                    .HasForeignKey(d => d.Property18Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY18");

                entity.HasOne(d => d.Property19)
                    .WithMany(p => p.MasterFormProperty19s)
                    .HasForeignKey(d => d.Property19Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY19");

                entity.HasOne(d => d.Property20)
                    .WithMany(p => p.MasterFormProperty20s)
                    .HasForeignKey(d => d.Property20Id)
                    .HasConstraintName("FK_MASTER_FORM_MASTER_PROPERTY20");
            });

            modelBuilder.Entity<MasterMaintainFooter>(entity =>
            {
                entity.HasKey(e => e.FooterId)
                    .HasName("PK__MASTER_F__85052F689EF24B51");

                entity.ToTable("MASTER_FOOTER");

                entity.Property(e => e.FooterId)
                    .HasColumnName("FOOTER_ID");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("IS_ACTIVE")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.FormName)
                    .HasMaxLength(20)
                    .HasColumnName("FORM_NAME");

                entity.Property(e => e.TextAdditional1).HasColumnName("TEXT_ADDITIONAL1");

                entity.Property(e => e.TextAdditional2).HasColumnName("TEXT_ADDITIONAL2");

                entity.Property(e => e.TextApprovedby).HasColumnName("TEXT_APPROVEDBY");

                entity.Property(e => e.TextPrintedby).HasColumnName("TEXT_PRINTEDBY");

                entity.Property(e => e.TextTelnumber).HasColumnName("TEXT_TELNUMBER");

                entity.Property(e => e.TextTestcondition).HasColumnName("TEXT_TESTCONDITION");
            });

            modelBuilder.Entity<MasterMaintainHeader>(entity =>
            {
                entity.HasKey(e => e.HeaderId)
                    .HasName("PK__MASTER_F__D1C4DC3E5591C006");

                entity.ToTable("MASTER_HEADER");

                entity.Property(e => e.HeaderId)
                    .HasColumnName("HEADER_ID");

                entity.Property(e => e.FormName)
                    .HasMaxLength(20)
                    .HasColumnName("FORM_NAME");

                entity.Property(e => e.DatetimeFormat)
                    .HasMaxLength(30)
                    .HasColumnName("DATETIME_FORMAT");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("IS_ACTIVE")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");
            });

            modelBuilder.Entity<MasterMaintainFormCoa>(entity =>
            {
                entity.HasKey(e => e.FormCoaId)
                    .HasName("PK__MASTER_F__E103520CA4DBCEB5");

                entity.ToTable("MASTER_FORM_COA");

                entity.Property(e => e.FormCoaId).HasColumnName("FORM_COA_ID");

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

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("IS_ACTIVE")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.MaterialSale)
                    .HasMaxLength(1)
                    .HasColumnName("MATERIAL_SALE")
                    .IsFixedLength();

                entity.Property(e => e.SequenceNo).HasColumnName("SEQUENCE_NO");
            });
            modelBuilder.Entity<MasterMaintainProperty>(entity =>
            {
                entity.HasKey(e => e.PropertyId)
                    .HasName("PK__MASTER_P__DD51AF0B5412FDD2");

                entity.ToTable("MASTER_PROPERTY");

                entity.Property(e => e.PropertyId).HasColumnName("PROPERTY_ID");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .HasColumnName("DISPLAY_NAME");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("IS_ACTIVE")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

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
            modelBuilder.Entity<MasterMaintainSiamToppanGrade>(entity =>
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

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("IS_ACTIVE")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Remark)
                    .HasMaxLength(10)
                    .HasColumnName("REMARK")
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

                entity.Property(e => e.Id);

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
        public virtual DbSet<MasterMaintainAutoCoa> MASTER_MAINTAIN_AUTO_COA { get; set; } = null!;
        public virtual DbSet<MasterMaintainCustomerCoaOption> MASTER_MAINTAIN_CUSTOMER_COA_OPTION { get; set; } = null!;
        public virtual DbSet<MasterMaintainForm> MASTER_MAINTAIN_FORM { get; set; } = null!;
        public virtual DbSet<MasterMaintainFooter> MASTER_MAINTAIN_FOOTER { get; set; } = null!;
        public virtual DbSet<MasterMaintainHeader> MASTER_MAINTAIN_HEADER { get; set; } = null!;
        public virtual DbSet<MasterMaintainFormCoa> MASTER_MAINTAIN_FORM_COA { get; set; } = null!;
        public virtual DbSet<MasterFormTemplate> MASTER_MAINTAIN_FORM_TEMPLATE { get; set; } = null!;
        public virtual DbSet<MasterMaintainProperty> MASTER_MAINTAIN_PROPERTY { get; set; } = null!;
        public virtual DbSet<MasterMaintainSiamToppanGrade> MASTER_MAINTAIN_SIAM_TOPPAN_GRADE { get; set; } = null!;
        public virtual DbSet<MasterDatabase> MASTER_DATABASE { get; set; } = null!;
        public virtual DbSet<ConvertingBatchDatum> ConvertingBatchData { get; set; } = null!;
        public virtual DbSet<SAPShippingPoint> SAPShippingPoints { get; set; } = null!;
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