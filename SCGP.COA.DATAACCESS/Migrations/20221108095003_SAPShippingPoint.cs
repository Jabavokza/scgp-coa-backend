using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCGP.COA.DATAACCESS.Migrations
{
    public partial class SAPShippingPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Shipping_Point",
                table: "SAPShippingPoints",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "InterCom_Status",
                table: "SAPShippingPoints",
                type: "char(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Company_Code",
                table: "SAPShippingPoints",
                type: "varchar(5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Shipping_Point",
                table: "SAPShippingPoints",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "InterCom_Status",
                table: "SAPShippingPoints",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Company_Code",
                table: "SAPShippingPoints",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)");
        }
    }
}
