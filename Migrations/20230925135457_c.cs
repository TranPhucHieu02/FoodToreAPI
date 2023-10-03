using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiforapp.Migrations
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipeIngredientParts",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipeInstructions",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeIngredientParts",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "RecipeInstructions",
                table: "Foods");
        }
    }
}
