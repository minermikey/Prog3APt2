using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prog3A.Migrations
{
    /// <inheritdoc />
    public partial class AddFarmersProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsModels",
                table: "ProductsModels");

            migrationBuilder.RenameTable(
                name: "ProductsModels",
                newName: "ProductsModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsModel",
                table: "ProductsModel",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsModel",
                table: "ProductsModel");

            migrationBuilder.RenameTable(
                name: "ProductsModel",
                newName: "ProductsModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsModels",
                table: "ProductsModels",
                column: "Id");
        }
    }
}
