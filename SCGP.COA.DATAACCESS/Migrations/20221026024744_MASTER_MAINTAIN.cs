using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCGP.COA.DATAACCESS.Migrations
{
    public partial class MASTER_MAINTAIN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MASTER_DATABASE",
                table: "MASTER_DATABASE");

            migrationBuilder.DropColumn(
                name: "MATERIAL_SALE",
                table: "MASTER_SIAM_TOPPAN_GRADE");

            migrationBuilder.AlterColumn<string>(
                name: "APP",
                table: "MASTER_DATABASE",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "DatabaseId",
                table: "MASTER_DATABASE",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CustomerCoaOptionId",
                table: "MASTER_CUSTOMER",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AutoCoaId",
                table: "MASTER_AUTO_COA_CUSTOMER",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MASTER_DATABASE",
                table: "MASTER_DATABASE",
                column: "DatabaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MASTER_DATABASE",
                table: "MASTER_DATABASE");

            migrationBuilder.DropColumn(
                name: "DatabaseId",
                table: "MASTER_DATABASE");

            migrationBuilder.DropColumn(
                name: "CustomerCoaOptionId",
                table: "MASTER_CUSTOMER");

            migrationBuilder.DropColumn(
                name: "AutoCoaId",
                table: "MASTER_AUTO_COA_CUSTOMER");

            migrationBuilder.AddColumn<string>(
                name: "MATERIAL_SALE",
                table: "MASTER_SIAM_TOPPAN_GRADE",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "APP",
                table: "MASTER_DATABASE",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MASTER_DATABASE",
                table: "MASTER_DATABASE",
                column: "APP");
        }
    }
}
