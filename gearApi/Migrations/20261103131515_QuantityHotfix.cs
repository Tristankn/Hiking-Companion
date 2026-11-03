using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gearApi.Migrations
{
    /// <inheritdoc />
    public partial class QuantityHotfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackingListItem_Gear_GearId",
                table: "PackingListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PackingListItem_PackingLists_PackingListId",
                table: "PackingListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackingListItem",
                table: "PackingListItem");

            migrationBuilder.RenameTable(
                name: "PackingListItem",
                newName: "PackingListItems");

            migrationBuilder.RenameIndex(
                name: "IX_PackingListItem_PackingListId",
                table: "PackingListItems",
                newName: "IX_PackingListItems_PackingListId");

            migrationBuilder.RenameIndex(
                name: "IX_PackingListItem_GearId",
                table: "PackingListItems",
                newName: "IX_PackingListItems_GearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackingListItems",
                table: "PackingListItems",
                column: "PackingListItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackingListItems_Gear_GearId",
                table: "PackingListItems",
                column: "GearId",
                principalTable: "Gear",
                principalColumn: "GearId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PackingListItems_PackingLists_PackingListId",
                table: "PackingListItems",
                column: "PackingListId",
                principalTable: "PackingLists",
                principalColumn: "PackingListId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackingListItems_Gear_GearId",
                table: "PackingListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PackingListItems_PackingLists_PackingListId",
                table: "PackingListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackingListItems",
                table: "PackingListItems");

            migrationBuilder.RenameTable(
                name: "PackingListItems",
                newName: "PackingListItem");

            migrationBuilder.RenameIndex(
                name: "IX_PackingListItems_PackingListId",
                table: "PackingListItem",
                newName: "IX_PackingListItem_PackingListId");

            migrationBuilder.RenameIndex(
                name: "IX_PackingListItems_GearId",
                table: "PackingListItem",
                newName: "IX_PackingListItem_GearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackingListItem",
                table: "PackingListItem",
                column: "PackingListItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackingListItem_Gear_GearId",
                table: "PackingListItem",
                column: "GearId",
                principalTable: "Gear",
                principalColumn: "GearId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PackingListItem_PackingLists_PackingListId",
                table: "PackingListItem",
                column: "PackingListId",
                principalTable: "PackingLists",
                principalColumn: "PackingListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
