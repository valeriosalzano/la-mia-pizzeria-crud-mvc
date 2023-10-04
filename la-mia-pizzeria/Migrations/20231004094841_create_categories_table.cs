using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class create_categories_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "category_id",
                table: "pizzas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pizzas_category_id",
                table: "pizzas",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pizzas_Categories_category_id",
                table: "pizzas",
                column: "category_id",
                principalTable: "Categories",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pizzas_Categories_category_id",
                table: "pizzas");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_pizzas_category_id",
                table: "pizzas");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "pizzas");
        }
    }
}
