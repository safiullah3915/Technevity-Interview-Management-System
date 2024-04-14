using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrPortal3.Data.Migrations
{
    /// <inheritdoc />
    public partial class UseraddedinInterviewee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Interviewee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Interviewee");
        }
    }
}
