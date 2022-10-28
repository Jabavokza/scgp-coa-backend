﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SCGP.COA.DATAACCESS.Contexts;

#nullable disable

namespace SCGP.COA.DATAACCESS.Migrations
{
    [DbContext(typeof(DbDataContext))]
    partial class DbDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.ConvertingBatchDatum", b =>
                {
                    b.Property<string>("Batch")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("BATCH")
                        .IsFixedLength();

                    b.Property<double?>("FilmThickness")
                        .HasColumnType("float")
                        .HasColumnName("FILM_THICKNESS");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nchar(3)")
                        .HasColumnName("GRADE")
                        .IsFixedLength();

                    b.Property<decimal>("Gram")
                        .HasColumnType("numeric(3,0)")
                        .HasColumnName("GRAM");

                    b.Property<double?>("Porosity")
                        .HasColumnType("float")
                        .HasColumnName("POROSITY");

                    b.Property<DateTime?>("ProductionDate")
                        .HasColumnType("date")
                        .HasColumnName("PRODUCTION_DATE");

                    b.Property<DateTime>("UploadedDatetime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UPLOADED_DATETIME");

                    b.HasKey("Batch")
                        .HasName("PK__Converti__77F1BF27073C44BF");

                    b.ToTable("Converting_Batch_Data", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.LogCoa", b =>
                {
                    b.Property<long>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("LOG_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LogId"), 1L, 1);

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("DOCUMENT_NUMBER");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("DOCUMENT_TYPE");

                    b.Property<DateTime>("LogTimestamp")
                        .HasColumnType("datetime")
                        .HasColumnName("LOG_TIMESTAMP");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MESSAGE");

                    b.Property<string>("OutputType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("OUTPUT_TYPE");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("STATUS");

                    b.HasKey("LogId")
                        .HasName("PK__LOG_COA__4364C8829DEE2B6E");

                    b.ToTable("LOG_COA", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.MASTER_DATABASE", b =>
                {
                    b.Property<int>("DatabaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DatabaseId"), 1L, 1);

                    b.Property<string>("APP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DB_HOST")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DB_NAME")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DB_PWD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DB_UID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MACHINE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DatabaseId");

                    b.ToTable("MASTER_DATABASE");
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.MASTER_MAINTAIN_AUTO_COA", b =>
                {
                    b.Property<string>("CustomerCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("CUSTOMER_CODE")
                        .IsFixedLength();

                    b.Property<int>("AutoCoaId")
                        .HasColumnType("int");

                    b.Property<bool>("AutocoaActive")
                        .HasColumnType("bit")
                        .HasColumnName("AUTOCOA_ACTIVE");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ShipToCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("SHIPTO_CODE")
                        .IsFixedLength();

                    b.HasKey("CustomerCode")
                        .HasName("PK__MASTER_A__8E71B5A88F3F4AF1");

                    b.ToTable("MASTER_AUTO_COA_CUSTOMER", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.MASTER_MAINTAIN_CUSTOMER_COA_OPTION", b =>
                {
                    b.Property<string>("CustomerCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("CUSTOMER_CODE")
                        .IsFixedLength();

                    b.Property<string>("CoaFooterText")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("COA_FOOTER_TEXT")
                        .HasDefaultValueSql("('')");

                    b.Property<int>("CustomerCoaOptionId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("CUSTOMER_NAME")
                        .HasDefaultValueSql("('')");

                    b.Property<bool>("DefaultOutputDp")
                        .HasColumnType("bit")
                        .HasColumnName("DEFAULT_OUTPUT_DP");

                    b.Property<bool>("DefaultOutputDpBarcode")
                        .HasColumnType("bit")
                        .HasColumnName("DEFAULT_OUTPUT_DP_BARCODE");

                    b.Property<bool>("DefaultOutputExcel")
                        .HasColumnType("bit")
                        .HasColumnName("DEFAULT_OUTPUT_EXCEL");

                    b.Property<bool>("DefaultOutputPdf")
                        .HasColumnType("bit")
                        .HasColumnName("DEFAULT_OUTPUT_PDF");

                    b.Property<bool>("DefaultOutputText")
                        .HasColumnType("bit")
                        .HasColumnName("DEFAULT_OUTPUT_TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("CustomerCode")
                        .HasName("PK__MASTER_C__8E71B5A803ABB44D");

                    b.ToTable("MASTER_CUSTOMER", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.MASTER_MAINTAIN_FOOTER", b =>
                {
                    b.Property<int>("FooterId")
                        .HasColumnType("int")
                        .HasColumnName("FORM_ID");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("TextAdditional1")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TEXT_ADDITIONAL1");

                    b.Property<string>("TextAdditional2")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TEXT_ADDITIONAL2");

                    b.Property<string>("TextApprovedby")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TEXT_APPROVEDBY");

                    b.Property<string>("TextPrintedby")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TEXT_PRINTEDBY");

                    b.Property<string>("TextTelnumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TEXT_TELNUMBER");

                    b.Property<string>("TextTestcondition")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TEXT_TESTCONDITION");

                    b.HasKey("FooterId")
                        .HasName("PK__MASTER_F__85052F689EF24B51");

                    b.ToTable("MASTER_FORM_FOOTER", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.MASTER_MAINTAIN_FORM", b =>
                {
                    b.Property<int>("FormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FORM_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FormId"), 1L, 1);

                    b.Property<string>("FormName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("FORM_NAME");

                    b.Property<int>("FormTemplateId")
                        .HasColumnType("int")
                        .HasColumnName("FORM_TEMPLATE_ID");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("Property01Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY01_ID");

                    b.Property<int?>("Property02Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY02_ID");

                    b.Property<int?>("Property03Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY03_ID");

                    b.Property<int?>("Property04Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY04_ID");

                    b.Property<int?>("Property05Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY05_ID");

                    b.Property<int?>("Property06Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY06_ID");

                    b.Property<int?>("Property07Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY07_ID");

                    b.Property<int?>("Property08Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY08_ID");

                    b.Property<int?>("Property09Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY09_ID");

                    b.Property<int?>("Property10Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY10_ID");

                    b.Property<int?>("Property11Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY11_ID");

                    b.Property<int?>("Property12Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY12_ID");

                    b.Property<int?>("Property13Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY13_ID");

                    b.Property<int?>("Property14Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY14_ID");

                    b.Property<int?>("Property15Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY15_ID");

                    b.Property<int?>("Property16Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY16_ID");

                    b.Property<int?>("Property17Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY17_ID");

                    b.Property<int?>("Property18Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY18_ID");

                    b.Property<int?>("Property19Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY19_ID");

                    b.Property<int?>("Property20Id")
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY20_ID");

                    b.HasKey("FormId")
                        .HasName("PK__MASTER_F__85052F68235A6657");

                    b.ToTable("MASTER_FORM", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.MASTER_MAINTAIN_FORM_COA", b =>
                {
                    b.Property<int>("FormCoaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RULE_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FormCoaId"), 1L, 1);

                    b.Property<string>("CustomerCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("CUSTOMER_CODE")
                        .IsFixedLength();

                    b.Property<int?>("FormExcelId")
                        .HasColumnType("int")
                        .HasColumnName("FORM_EXCEL_ID");

                    b.Property<int?>("FormPdfId")
                        .HasColumnType("int")
                        .HasColumnName("FORM_PDF_ID");

                    b.Property<int?>("FormTextId")
                        .HasColumnType("int")
                        .HasColumnName("FORM_TEXT_ID");

                    b.Property<string>("Grade")
                        .HasMaxLength(3)
                        .HasColumnType("nchar(3)")
                        .HasColumnName("GRADE")
                        .IsFixedLength();

                    b.Property<decimal?>("Gram")
                        .HasColumnType("numeric(3,0)")
                        .HasColumnName("GRAM");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MaterialSale")
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)")
                        .HasColumnName("MATERIAL_SALE")
                        .IsFixedLength();

                    b.Property<int>("SequenceNo")
                        .HasColumnType("int")
                        .HasColumnName("RULE_ORDER");

                    b.HasKey("FormCoaId")
                        .HasName("PK__MASTER_F__E103520CA4DBCEB5");

                    b.ToTable("MASTER_FORM_MAPPING_RULES", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.MASTER_MAINTAIN_FORM_TEMPLATE", b =>
                {
                    b.Property<int>("FormTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FORM_TEMPLATE_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FormTemplateId"), 1L, 1);

                    b.Property<string>("FormTemplateName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("FORM_TEMPLATE_NAME");

                    b.HasKey("FormTemplateId")
                        .HasName("PK__MASTER_F__D1C4DC3E91F43A39");

                    b.ToTable("MASTER_FORM_TEMPLATE", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.MASTER_MAINTAIN_HEADER", b =>
                {
                    b.Property<int>("HeaderId")
                        .HasColumnType("int")
                        .HasColumnName("FORM_TEMPLATE_ID");

                    b.Property<string>("DatetimeFormat")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("DATETIME_FORMAT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("HeaderId")
                        .HasName("PK__MASTER_F__D1C4DC3E5591C006");

                    b.ToTable("MASTER_FORM_HEADER", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.MASTER_MAINTAIN_PROPERTY", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PROPERTY_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyId"), 1L, 1);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("DISPLAY_NAME");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("OutputFormat")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("OUTPUT_FORMAT");

                    b.Property<string>("OutputName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("OUTPUT_NAME");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("PROPERTY_NAME");

                    b.HasKey("PropertyId")
                        .HasName("PK__MASTER_P__DD51AF0B5412FDD2");

                    b.ToTable("MASTER_PROPERTY", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.MASTER_MAINTAIN_SIAM_TOPPAN_GRADE", b =>
                {
                    b.Property<int>("SiamToppanGradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SIAM_TOPPAN_GRADE_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SiamToppanGradeId"), 1L, 1);

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nchar(3)")
                        .HasColumnName("GRADE")
                        .IsFixedLength();

                    b.Property<decimal?>("Gram")
                        .HasColumnType("numeric(3,0)")
                        .HasColumnName("GRAM");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Remark")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("REMARKS")
                        .IsFixedLength();

                    b.Property<string>("SiamToppanNumber")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("char(3)")
                        .HasColumnName("SIAM_TOPPAN_NUMBER")
                        .IsFixedLength();

                    b.HasKey("SiamToppanGradeId")
                        .HasName("PK__MASTER_S__3B5452A6D3712AA1");

                    b.ToTable("MASTER_SIAM_TOPPAN_GRADE", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_GROUP", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"), 1L, 1);

                    b.Property<bool>("ActiveFlag")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GroupName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("GroupId");

                    b.ToTable("MASTER_GROUP");
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_GROUP_ROLE", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("GroupId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("MASTER_GROUP_ROLE");
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_MENU", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"), 1L, 1);

                    b.Property<string>("Action")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("ActiveFlag")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Icon")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("MenuName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ParentMenu")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MenuId");

                    b.ToTable("MASTER_MENU");
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_MENU_ROLE", b =>
                {
                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("MenuId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("MASTER_MENU_ROLE");
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_ROLE", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<bool>("ActiveFlag")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RoleId");

                    b.ToTable("MASTER_ROLE");
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_USER", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("ActiveFlag")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Domain")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("LastChangePasswordDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<bool?>("MustChangePassword")
                        .HasColumnType("bit");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Organization")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PasswordEncrypt")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("UserId");

                    b.HasIndex("UserName");

                    b.HasIndex("NormalizedUserName", "Domain");

                    b.ToTable("MASTER_USER");
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_USER_GROUP", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.HasKey("UserId", "GroupId");

                    b.ToTable("MASTER_USER_GROUP");
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.REFRESH_TOKEN", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DeleteFlag")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Token");

                    b.ToTable("REFRESH_TOKENS", (string)null);
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_GROUP_ROLE", b =>
                {
                    b.HasOne("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_GROUP", "GROUP")
                        .WithMany("GROUP_ROLEs")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_ROLE", "ROLE")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GROUP");

                    b.Navigation("ROLE");
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_MENU_ROLE", b =>
                {
                    b.HasOne("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_MENU", "MENU")
                        .WithMany("Roles")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_ROLE", "ROLE")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MENU");

                    b.Navigation("ROLE");
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_GROUP", b =>
                {
                    b.Navigation("GROUP_ROLEs");
                });

            modelBuilder.Entity("SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization.MASTER_MENU", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
