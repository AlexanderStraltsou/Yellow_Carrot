using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YellowCarrot.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTags",
                columns: table => new
                {
                    RecipesRecipeId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTags", x => new { x.RecipesRecipeId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_RecipeTags_Recipes_RecipesRecipeId",
                        column: x => x.RecipesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTags_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "Name", "TagId" },
                values: new object[,]
                {
                    { 1, "Pasta Pesto", null },
                    { 2, "Pizza Salami", 5 },
                    { 3, "Pizza Hawaii", 6 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "TagName" },
                values: new object[,]
                {
                    { 1, "Vegetarian" },
                    { 2, "Vegan" },
                    { 3, "Dairy" },
                    { 4, "Sugar Free" },
                    { 5, "Gluten Free" },
                    { 6, "Lactose Free" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Name", "IngredientId", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { "Ananas", 12, "10 slices", 3 },
                    { "Curry", 9, "10 gr", 3 },
                    { "Mozarella Cheese", 5, "100", 2 },
                    { "Parma", 10, "50 gr", 3 },
                    { "Parmegano Cheese", 3, "100 gr", 1 },
                    { "Pasta Rigatoni", 1, "200 gr", 1 },
                    { "Pesto", 2, "70 gr", 1 },
                    { "Pizza Bottom", 11, "2 pcs", 3 },
                    { "Pizza Dough", 4, "1 kg", 2 },
                    { "Red Paprika", 6, "50 gr", 2 },
                    { "Salami", 7, "100 gr", 2 },
                    { "Tomato Sauce", 8, "200 gr", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTags_TagsTagId",
                table: "RecipeTags",
                column: "TagsTagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "RecipeTags");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
