using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Licenta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobTableConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MinSalary = table.Column<long>(type: "bigint", nullable: false),
                    MaxSalary = table.Column<long>(type: "bigint", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PartnerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Address", "Description", "Experience", "MaxSalary", "MinSalary", "PartnerId", "Title" },
                values: new object[,]
                {
                    { 1L, "Bucharest", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ", 0, 2500L, 2000L, 1L, "Job 1" },
                    { 2L, "Bucharest", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ", 2, 5500L, 5000L, 2L, "Job 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_PartnerId",
                table: "Jobs",
                column: "PartnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
