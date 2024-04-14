using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrPortal3.Data.Migrations
{
    /// <inheritdoc />
    public partial class PasswordAddedinUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SimPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SimPassword",
                table: "AspNetUsers");
        }
    }
}
