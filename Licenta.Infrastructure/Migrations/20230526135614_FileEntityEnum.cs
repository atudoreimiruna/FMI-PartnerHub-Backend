using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licenta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FileEntityEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_FirstName_LastName_Email",
                table: "Students",
                columns: new[] { "FirstName", "LastName", "Email" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_FirstName_LastName_Email",
                table: "Students");
        }
    }
}
