using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PureCosmetics.AuthService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addaddressuserv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Address");
        }
    }
}
