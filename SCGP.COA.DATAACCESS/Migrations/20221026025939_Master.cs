using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCGP.COA.DATAACCESS.Migrations
{
    public partial class Master : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MASTER_DATABASE");

            migrationBuilder.CreateTable(
                name: "MasterDatabase",
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
                    table.PrimaryKey("PK_MasterDatabase", x => x.DatabaseId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterDatabase");

            migrationBuilder.CreateTable(
                name: "MASTER_DATABASE",
                columns: table => new
                {
                    DatabaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    APP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_HOST = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_PWD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DB_UID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MACHINE = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MASTER_DATABASE", x => x.DatabaseId);
                });
        }
    }
}
