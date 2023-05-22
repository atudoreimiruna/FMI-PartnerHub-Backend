using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licenta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldsOnPartner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "Partners",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Partners",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Email",
                value: "Email: partner1@gmail.com");

            migrationBuilder.UpdateData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Email", "Phone" },
                values: new object[] { "Email: office@partner.com", "Phone: 0886435767" });

            migrationBuilder.UpdateData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Email",
                value: "Email: partner3@gmail.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Partners");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Partners",
                newName: "Contact");

            migrationBuilder.UpdateData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Contact",
                value: "Email: office@partner.com");
        }
    }
}
