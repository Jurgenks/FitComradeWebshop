using Microsoft.EntityFrameworkCore.Migrations;

namespace FitComrade.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bank",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPostalCode",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerSurName",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerPostalCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerSurName",
                table: "Customers");
        }
    }
}
