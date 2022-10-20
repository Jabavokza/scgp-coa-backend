using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCGP.COA.DATAACCESS.Migrations
{
    public partial class add_auth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "MASTER_GROUP_ROLE");

            migrationBuilder.DropTable(
                name: "MASTER_MENU_ROLE");

            migrationBuilder.DropTable(
                name: "MASTER_USER");

            migrationBuilder.DropTable(
                name: "MASTER_USER_GROUP");

            migrationBuilder.DropTable(
                name: "REFRESH_TOKENS");

            migrationBuilder.DropTable(
                name: "MASTER_GROUP");

            migrationBuilder.DropTable(
                name: "MASTER_MENU");

            migrationBuilder.DropTable(
                name: "MASTER_ROLE");
        }
    }
}
