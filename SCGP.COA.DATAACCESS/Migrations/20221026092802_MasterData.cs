using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCGP.COA.DATAACCESS.Migrations
{
    public partial class MasterData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterDatabase",
                table: "MasterDatabase");

            migrationBuilder.RenameTable(
                name: "MasterDatabase",
                newName: "MASTER_DATABASE");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MASTER_SIAM_TOPPAN_GRADE",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MASTER_PROPERTY",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MASTER_FORM_MAPPING_RULES",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MASTER_FORM_HEADER",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MASTER_FORM_FOOTER",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MASTER_FORM",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MASTER_CUSTOMER",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MASTER_AUTO_COA_CUSTOMER",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                name: "IsActive",
                table: "MASTER_SIAM_TOPPAN_GRADE");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MASTER_PROPERTY");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MASTER_FORM_MAPPING_RULES");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MASTER_FORM_HEADER");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MASTER_FORM_FOOTER");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MASTER_FORM");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MASTER_CUSTOMER");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MASTER_AUTO_COA_CUSTOMER");

            migrationBuilder.RenameTable(
                name: "MASTER_DATABASE",
                newName: "MasterDatabase");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterDatabase",
                table: "MasterDatabase",
                column: "DatabaseId");
        }
    }
}
