using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licenta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascadeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentJobs_Jobs_JobId",
                table: "StudentJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPartners_Partners_PartnerId",
                table: "StudentPartners");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentJobs_Jobs_JobId",
                table: "StudentJobs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPartners_Partners_PartnerId",
                table: "StudentPartners",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentJobs_Jobs_JobId",
                table: "StudentJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPartners_Partners_PartnerId",
                table: "StudentPartners");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentJobs_Jobs_JobId",
                table: "StudentJobs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPartners_Partners_PartnerId",
                table: "StudentPartners",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id");
        }
    }
}
