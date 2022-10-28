using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCGP.COA.DATAACCESS.Migrations
{
    public partial class MasterDatabase : Migration
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
                    CUSTOMER_CODE = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    SHIPTO_CODE = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    AUTOCOA_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_A__8E71B5A88F3F4AF1", x => x.CUSTOMER_CODE);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_CUSTOMER",
                columns: table => new
                {
                    CUSTOMER_CODE = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CUSTOMER_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    DEFAULT_OUTPUT_PDF = table.Column<bool>(type: "bit", nullable: false),
                    DEFAULT_OUTPUT_TEXT = table.Column<bool>(type: "bit", nullable: false),
                    DEFAULT_OUTPUT_EXCEL = table.Column<bool>(type: "bit", nullable: false),
                    DEFAULT_OUTPUT_DP = table.Column<bool>(type: "bit", nullable: false),
                    DEFAULT_OUTPUT_DP_BARCODE = table.Column<bool>(type: "bit", nullable: false),
                    COA_FOOTER_TEXT = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_C__8E71B5A803ABB44D", x => x.CUSTOMER_CODE);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_DATABASE",
                columns: table => new
                {
                    APP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MACHINE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_HOST = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_UID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_PWD = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MASTER_DATABASE", x => x.APP);
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
                    PROPERTY20_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_F__85052F68235A6657", x => x.FORM_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_FORM_FOOTER",
                columns: table => new
                {
                    FORM_ID = table.Column<int>(type: "int", nullable: false),
                    TEXT_TESTCONDITION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEXT_APPROVEDBY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEXT_PRINTEDBY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEXT_TELNUMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEXT_ADDITIONAL1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEXT_ADDITIONAL2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_F__85052F689EF24B51", x => x.FORM_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_FORM_HEADER",
                columns: table => new
                {
                    FORM_TEMPLATE_ID = table.Column<int>(type: "int", nullable: false),
                    DATETIME_FORMAT = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_F__D1C4DC3E5591C006", x => x.FORM_TEMPLATE_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_FORM_MAPPING_RULES",
                columns: table => new
                {
                    RULE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RULE_ORDER = table.Column<int>(type: "int", nullable: false),
                    GRADE = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: true),
                    GRAM = table.Column<decimal>(type: "numeric(3,0)", nullable: true),
                    MATERIAL_SALE = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    CUSTOMER_CODE = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    FORM_PDF_ID = table.Column<int>(type: "int", nullable: true),
                    FORM_TEXT_ID = table.Column<int>(type: "int", nullable: true),
                    FORM_EXCEL_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_F__E103520CA4DBCEB5", x => x.RULE_ID);
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
                name: "MASTER_PROPERTY",
                columns: table => new
                {
                    PROPERTY_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PROPERTY_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DISPLAY_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OUTPUT_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OUTPUT_FORMAT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_P__DD51AF0B5412FDD2", x => x.PROPERTY_ID);
                });

            migrationBuilder.CreateTable(
                name: "MASTER_SIAM_TOPPAN_GRADE",
                columns: table => new
                {
                    SIAM_TOPPAN_GRADE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRADE = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    GRAM = table.Column<decimal>(type: "numeric(3,0)", nullable: true),
                    MATERIAL_SALE = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    SIAM_TOPPAN_NUMBER = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    REMARKS = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MASTER_S__3B5452A6D3712AA1", x => x.SIAM_TOPPAN_GRADE_ID);
                });
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
                name: "MASTER_CUSTOMER");

            migrationBuilder.DropTable(
                name: "MASTER_DATABASE");

            migrationBuilder.DropTable(
                name: "MASTER_FORM");

            migrationBuilder.DropTable(
                name: "MASTER_FORM_FOOTER");

            migrationBuilder.DropTable(
                name: "MASTER_FORM_HEADER");

            migrationBuilder.DropTable(
                name: "MASTER_FORM_MAPPING_RULES");

            migrationBuilder.DropTable(
                name: "MASTER_FORM_TEMPLATE");

            migrationBuilder.DropTable(
                name: "MASTER_PROPERTY");

            migrationBuilder.DropTable(
                name: "MASTER_SIAM_TOPPAN_GRADE");
        }
    }
}
