using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediportaSOTags.Migrations
{
    /// <inheritdoc />
    public partial class PercentageAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PercentPart",
                table: "Tags",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentPart",
                table: "Tags");
        }
    }
}
