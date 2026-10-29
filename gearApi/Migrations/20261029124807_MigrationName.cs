using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gearApi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Packed",
                table: "Gear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Packed",
                table: "Gear",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
