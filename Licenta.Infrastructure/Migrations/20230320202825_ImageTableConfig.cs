using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licenta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImageTableConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PartnerId",
                table: "Posts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "PostId",
                table: "Images",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PostId",
                table: "Images",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Posts_PostId",
                table: "Images",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Posts_PostId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PostId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Images");

            migrationBuilder.AlterColumn<long>(
                name: "PartnerId",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
