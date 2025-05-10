using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prog3A.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsTableAgainAgainRenameTableThisTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "ProductsModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsModels",
                table: "ProductsModels",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsModels",
                table: "ProductsModels");

            migrationBuilder.RenameTable(
                name: "ProductsModels",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");
        }
    }
}
