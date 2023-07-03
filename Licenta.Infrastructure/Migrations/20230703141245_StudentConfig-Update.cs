using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licenta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StudentConfigUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentJobs_Students_StudentId",
                table: "StudentJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPartners_Students_StudentId",
                table: "StudentPartners");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentJobs_Students_StudentId",
                table: "StudentJobs",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPartners_Students_StudentId",
                table: "StudentPartners",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentJobs_Students_StudentId",
                table: "StudentJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPartners_Students_StudentId",
                table: "StudentPartners");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentJobs_Students_StudentId",
                table: "StudentJobs",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPartners_Students_StudentId",
                table: "StudentPartners",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
