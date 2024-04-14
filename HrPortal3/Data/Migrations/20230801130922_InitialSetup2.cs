using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrPortal3.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PanelMemberEmail",
                table: "Panel");

            migrationBuilder.DropColumn(
                name: "PanelMemberPassword",
                table: "Panel");

            migrationBuilder.DropColumn(
                name: "PanelMemberRole",
                table: "Panel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PanelMemberEmail",
                table: "Panel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PanelMemberPassword",
                table: "Panel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PanelMemberRole",
                table: "Panel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
