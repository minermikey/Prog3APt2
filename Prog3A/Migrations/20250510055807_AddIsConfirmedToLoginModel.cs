using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prog3A.Migrations
{
    /// <inheritdoc />
    public partial class AddIsConfirmedToLoginModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "LoginModel",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "LoginModel");
        }
    }
}
