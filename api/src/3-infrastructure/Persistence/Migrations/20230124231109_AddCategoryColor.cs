using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Categories",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Color",
                table: "Categories",
                column: "Color",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Color",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Categories");
        }
    }
}
