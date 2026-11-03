using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace gearApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedPackinglists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Gear",
                newName: "GearId");

            migrationBuilder.CreateTable(
                name: "PackingLists",
                columns: table => new
                {
                    PackingListId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingLists", x => x.PackingListId);
                });

            migrationBuilder.CreateTable(
                name: "PackingListItem",
                columns: table => new
                {
                    PackingListItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GearId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    PackingListId = table.Column<int>(type: "integer", nullable: false),
                    IsPacked = table.Column<bool>(type: "boolean", nullable: false),
                    IsWorn = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingListItem", x => x.PackingListItemId);
                    table.ForeignKey(
                        name: "FK_PackingListItem_Gear_GearId",
                        column: x => x.GearId,
                        principalTable: "Gear",
                        principalColumn: "GearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackingListItem_PackingLists_PackingListId",
                        column: x => x.PackingListId,
                        principalTable: "PackingLists",
                        principalColumn: "PackingListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackingListItem_GearId",
                table: "PackingListItem",
                column: "GearId");

            migrationBuilder.CreateIndex(
                name: "IX_PackingListItem_PackingListId",
                table: "PackingListItem",
                column: "PackingListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackingListItem");

            migrationBuilder.DropTable(
                name: "PackingLists");

            migrationBuilder.RenameColumn(
                name: "GearId",
                table: "Gear",
                newName: "Id");
        }
    }
}
