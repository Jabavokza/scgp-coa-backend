using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCGP.COA.DATAACCESS.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Converting_Batch_Data",
                columns: table => new
                {
                    BATCH = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    UPLOADED_DATETIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PRODUCTION_DATE = table.Column<DateTime>(type: "date", nullable: true),
                    GRADE = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    GRAM = table.Column<decimal>(type: "numeric(3,0)", nullable: false),
                    FILM_THICKNESS = table.Column<double>(type: "float", nullable: true),
                    POROSITY = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Converti__77F1BF27073C44BF", x => x.BATCH);
                });

            migrationBuilder.CreateTable(
                name: "LOG_COA",
                columns: table => new
                {
                    LOG_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOG_TIMESTAMP = table.Column<DateTime>(type: "datetime", nullable: false),
                    DOCUMENT_TYPE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DOCUMENT_NUMBER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OUTPUT_TYPE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MESSAGE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LOG_COA__4364C8829DEE2B6E", x => x.LOG_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_AUTO_COA_CUSTOMER",
                columns: table => new
                {
                    AUTO_COA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUSTOMER_CODE = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    SHIP_TO_CODE = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    AUTOCOA_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_A__8E71B5A88F3F4AF1", x => x.AUTO_COA_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_CUSTOMER_COA_OPTION",
                columns: table => new
                {
                    CUSTOMER_COA_OPTION_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUSTOMER_CODE = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CUSTOMER_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    DEFAULT_OUTPUT_PDF = table.Column<bool>(type: "bit", nullable: false),
                    DEFAULT_OUTPUT_TEXT = table.Column<bool>(type: "bit", nullable: false),
                    DEFAULT_OUTPUT_EXCEL = table.Column<bool>(type: "bit", nullable: false),
                    DEFAULT_OUTPUT_DP = table.Column<bool>(type: "bit", nullable: false),
                    DEFAULT_OUTPUT_DP_BARCODE = table.Column<bool>(type: "bit", nullable: false),
                    COA_FOOTER_TEXT = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('')"),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_C__8E71B5A803ABB44D", x => x.CUSTOMER_COA_OPTION_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_DATABASE",
                columns: table => new
                {
                    DatabaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    APP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MACHINE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_HOST = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_UID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_PWD = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MASTER_DATABASE", x => x.DatabaseId);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_FOOTER",
                columns: table => new
                {
                    FOOTER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FORM_NAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TEXT_TESTCONDITION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEXT_APPROVEDBY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEXT_PRINTEDBY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEXT_TELNUMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEXT_ADDITIONAL1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEXT_ADDITIONAL2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_F__85052F689EF24B51", x => x.FOOTER_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_FORM_COA",
                columns: table => new
                {
                    FORM_COA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SEQUENCE_NO = table.Column<int>(type: "int", nullable: false),
                    GRADE = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: true),
                    GRAM = table.Column<decimal>(type: "numeric(3,0)", nullable: true),
                    MATERIAL_SALE = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    CUSTOMER_CODE = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    FORM_PDF_ID = table.Column<int>(type: "int", nullable: true),
                    FORM_TEXT_ID = table.Column<int>(type: "int", nullable: true),
                    FORM_EXCEL_ID = table.Column<int>(type: "int", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_F__E103520CA4DBCEB5", x => x.FORM_COA_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_FORM_TEMPLATE",
                columns: table => new
                {
                    FORM_TEMPLATE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FORM_TEMPLATE_NAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_F__D1C4DC3E91F43A39", x => x.FORM_TEMPLATE_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_GROUP",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MASTER_GROUP", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_HEADER",
                columns: table => new
                {
                    HEADER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FORM_NAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DATETIME_FORMAT = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_F__D1C4DC3E5591C006", x => x.HEADER_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_MENU",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ParentMenu = table.Column<int>(type: "int", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MASTER_MENU", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_PROPERTY",
                columns: table => new
                {
                    PROPERTY_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PROPERTY_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DISPLAY_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OUTPUT_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OUTPUT_FORMAT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_P__DD51AF0B5412FDD2", x => x.PROPERTY_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_ROLE",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MASTER_ROLE", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_SIAM_TOPPAN_GRADE",
                columns: table => new
                {
                    SIAM_TOPPAN_GRADE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRADE = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    GRAM = table.Column<decimal>(type: "numeric(3,0)", nullable: true),
                    SIAM_TOPPAN_NUMBER = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    REMARK = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_S__3B5452A6D3712AA1", x => x.SIAM_TOPPAN_GRADE_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_USER",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PasswordEncrypt = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: true),
                    MustChangePassword = table.Column<bool>(type: "bit", nullable: true),
                    LastChangePasswordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MASTER_USER", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_USER_GROUP",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MASTER_USER_GROUP", x => new { x.UserId, x.GroupId });
                });

            migrationBuilder.CreateTable(
                name: "REFRESH_TOKENS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REFRESH_TOKENS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_FORM",
                columns: table => new
                {
                    FORM_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FORM_NAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FORM_TEMPLATE_ID = table.Column<int>(type: "int", nullable: false),
                    PROPERTY01_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY02_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY03_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY04_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY05_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY06_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY07_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY08_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY09_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY10_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY11_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY12_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY13_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY14_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY15_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY16_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY17_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY18_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY19_ID = table.Column<int>(type: "int", nullable: true),
                    PROPERTY20_ID = table.Column<int>(type: "int", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_F__85052F68235A6657", x => x.FORM_ID);
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_FORM_TEMPLATE",
                        column: x => x.FORM_TEMPLATE_ID,
                        principalTable: "MASTER_FORM_TEMPLATE",
                        principalColumn: "FORM_TEMPLATE_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY1",
                        column: x => x.PROPERTY01_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY10",
                        column: x => x.PROPERTY10_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY11",
                        column: x => x.PROPERTY11_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY12",
                        column: x => x.PROPERTY12_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY13",
                        column: x => x.PROPERTY13_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY14",
                        column: x => x.PROPERTY14_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY15",
                        column: x => x.PROPERTY15_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY16",
                        column: x => x.PROPERTY16_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY17",
                        column: x => x.PROPERTY17_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY18",
                        column: x => x.PROPERTY18_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY19",
                        column: x => x.PROPERTY19_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY2",
                        column: x => x.PROPERTY02_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY20",
                        column: x => x.PROPERTY20_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY3",
                        column: x => x.PROPERTY03_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY4",
                        column: x => x.PROPERTY04_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY5",
                        column: x => x.PROPERTY05_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY6",
                        column: x => x.PROPERTY06_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY7",
                        column: x => x.PROPERTY07_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY8",
                        column: x => x.PROPERTY08_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                    table.ForeignKey(
                        name: "FK_MASTER_FORM_MASTER_PROPERTY9",
                        column: x => x.PROPERTY09_ID,
                        principalTable: "MASTER_PROPERTY",
                        principalColumn: "PROPERTY_ID");
                });

            migrationBuilder.CreateTable(
                name: "MASTER_GROUP_ROLE",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MASTER_GROUP_ROLE", x => new { x.GroupId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_MASTER_GROUP_ROLE_MASTER_GROUP_GroupId",
                        column: x => x.GroupId,
                        principalTable: "MASTER_GROUP",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MASTER_GROUP_ROLE_MASTER_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "MASTER_ROLE",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_MENU_ROLE",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MASTER_MENU_ROLE", x => new { x.MenuId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_MASTER_MENU_ROLE_MASTER_MENU_MenuId",
                        column: x => x.MenuId,
                        principalTable: "MASTER_MENU",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MASTER_MENU_ROLE_MASTER_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "MASTER_ROLE",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_FORM_TEMPLATE_ID",
                table: "MASTER_FORM",
                column: "FORM_TEMPLATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY01_ID",
                table: "MASTER_FORM",
                column: "PROPERTY01_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY02_ID",
                table: "MASTER_FORM",
                column: "PROPERTY02_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY03_ID",
                table: "MASTER_FORM",
                column: "PROPERTY03_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY04_ID",
                table: "MASTER_FORM",
                column: "PROPERTY04_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY05_ID",
                table: "MASTER_FORM",
                column: "PROPERTY05_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY06_ID",
                table: "MASTER_FORM",
                column: "PROPERTY06_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY07_ID",
                table: "MASTER_FORM",
                column: "PROPERTY07_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY08_ID",
                table: "MASTER_FORM",
                column: "PROPERTY08_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY09_ID",
                table: "MASTER_FORM",
                column: "PROPERTY09_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY10_ID",
                table: "MASTER_FORM",
                column: "PROPERTY10_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY11_ID",
                table: "MASTER_FORM",
                column: "PROPERTY11_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY12_ID",
                table: "MASTER_FORM",
                column: "PROPERTY12_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY13_ID",
                table: "MASTER_FORM",
                column: "PROPERTY13_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY14_ID",
                table: "MASTER_FORM",
                column: "PROPERTY14_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY15_ID",
                table: "MASTER_FORM",
                column: "PROPERTY15_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY16_ID",
                table: "MASTER_FORM",
                column: "PROPERTY16_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY17_ID",
                table: "MASTER_FORM",
                column: "PROPERTY17_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY18_ID",
                table: "MASTER_FORM",
                column: "PROPERTY18_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY19_ID",
                table: "MASTER_FORM",
                column: "PROPERTY19_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_FORM_PROPERTY20_ID",
                table: "MASTER_FORM",
                column: "PROPERTY20_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_GROUP_ROLE_RoleId",
                table: "MASTER_GROUP_ROLE",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_MENU_ROLE_RoleId",
                table: "MASTER_MENU_ROLE",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_USER_NormalizedUserName_Domain",
                table: "MASTER_USER",
                columns: new[] { "NormalizedUserName", "Domain" });

            migrationBuilder.CreateIndex(
                name: "IX_MASTER_USER_UserName",
                table: "MASTER_USER",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_REFRESH_TOKENS_Token",
                table: "REFRESH_TOKENS",
                column: "Token");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Converting_Batch_Data");

            migrationBuilder.DropTable(
                name: "LOG_COA");

            migrationBuilder.DropTable(
                name: "MASTER_AUTO_COA_CUSTOMER");

            migrationBuilder.DropTable(
                name: "MASTER_CUSTOMER_COA_OPTION");

            migrationBuilder.DropTable(
                name: "MASTER_DATABASE");

            migrationBuilder.DropTable(
                name: "MASTER_FOOTER");

            migrationBuilder.DropTable(
                name: "MASTER_FORM");

            migrationBuilder.DropTable(
                name: "MASTER_FORM_COA");

            migrationBuilder.DropTable(
                name: "MASTER_GROUP_ROLE");

            migrationBuilder.DropTable(
                name: "MASTER_HEADER");

            migrationBuilder.DropTable(
                name: "MASTER_MENU_ROLE");

            migrationBuilder.DropTable(
                name: "MASTER_SIAM_TOPPAN_GRADE");

            migrationBuilder.DropTable(
                name: "MASTER_USER");

            migrationBuilder.DropTable(
                name: "MASTER_USER_GROUP");

            migrationBuilder.DropTable(
                name: "REFRESH_TOKENS");

            migrationBuilder.DropTable(
                name: "MASTER_FORM_TEMPLATE");

            migrationBuilder.DropTable(
                name: "MASTER_PROPERTY");

            migrationBuilder.DropTable(
                name: "MASTER_GROUP");

            migrationBuilder.DropTable(
                name: "MASTER_MENU");

            migrationBuilder.DropTable(
                name: "MASTER_ROLE");
        }
    }
}
