using Microsoft.EntityFrameworkCore.Migrations;

namespace FitComrade.Data.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerAdress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerPostalCode",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "CustomerAdresses",
                columns: table => new
                {
                    CustomerAdressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    OrderID = table.Column<int>(nullable: false),
                    CustomerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAdresses", x => x.CustomerAdressID);
                    table.ForeignKey(
                        name: "FK_CustomerAdresses_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAdresses_CustomerID",
                table: "CustomerAdresses",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAdresses");

            migrationBuilder.AddColumn<string>(
                name: "CustomerAdress",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPostalCode",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
