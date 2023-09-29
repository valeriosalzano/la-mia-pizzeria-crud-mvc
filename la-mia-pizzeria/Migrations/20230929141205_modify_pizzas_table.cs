using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class modify_pizzas_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "pizzas",
                type: "VARCHAR(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_pizzas_slug",
                table: "pizzas",
                column: "slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pizzas_slug",
                table: "pizzas");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "pizzas");
        }
    }
}
