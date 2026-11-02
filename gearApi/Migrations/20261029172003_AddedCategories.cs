using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace gearApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Gear");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Gear",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    ParentName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gear_CategoryId",
                table: "Gear",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gear_Categories_CategoryId",
                table: "Gear",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gear_Categories_CategoryId",
                table: "Gear");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Gear_CategoryId",
                table: "Gear");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Gear");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Gear",
                type: "text",
                nullable: true);
        }
    }
}
