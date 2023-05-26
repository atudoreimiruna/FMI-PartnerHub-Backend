using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licenta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StudentJobEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentJobs",
                columns: table => new
                {
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentJobs", x => new { x.StudentId, x.JobId });
                    table.ForeignKey(
                        name: "FK_StudentJobs_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentJobs_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StudentJobs_JobId",
                table: "StudentJobs",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentJobs");
        }
    }
}
