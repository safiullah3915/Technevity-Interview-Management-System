using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrPortal3.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interviewee",
                columns: table => new
                {
                    IntervieweeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResumeFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PanelId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviewee", x => x.IntervieweeId);
                });

            migrationBuilder.CreateTable(
                name: "Panel",
                columns: table => new
                {
                    PanelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PanelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanelMemberEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanelMemberPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanelMemberRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panel", x => x.PanelId);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interviewee");

            migrationBuilder.DropTable(
                name: "Panel");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
