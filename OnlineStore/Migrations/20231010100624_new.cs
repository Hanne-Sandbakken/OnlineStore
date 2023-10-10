using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    DeliveryAdress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CartId", "Description", "ImageUrl", "Name", "OrderId", "Price" },
                values: new object[,]
                {
                    { 1, null, "Bukett vase klar 210 mm", "https://www.magnor.no/wp-content/uploads/2020/10/8712012-Bukett-Vase-332x530.png", "Bukett vase klar", null, 2649 },
                    { 2, null, "Family vase brun medium, 17 cm", "https://www.magnor.no/wp-content/uploads/2023/06/209021-406x530.jpg", "Family vase brun medium", null, 1299 },
                    { 3, null, "Family vase brun stor, 26 cm", "https://www.magnor.no/wp-content/uploads/2023/06/209031-426x530.jpg", "Family vase brun stor", null, 1799 },
                    { 4, null, "Family vase klar liten 11 cm", "https://www.magnor.no/wp-content/uploads/2023/01/Galaxie_08-420x530.jpg", "Family vase klar liten", null, 629 },
                    { 5, null, "Family vase klar medium 17 cm", "https://www.magnor.no/wp-content/uploads/2023/01/209020-1-433x530.jpg", "Family vase klar medium", null, 1049 },
                    { 6, null, "Family vase klar stor 26 cm", "https://www.magnor.no/wp-content/uploads/2023/01/209030-1-436x530.jpg", "Family vase klar stor", null, 1579 },
                    { 7, null, "Galaxie lykt/vase koks liten 8,5 cm", "https://www.magnor.no/wp-content/uploads/2023/01/220711-507x530.jpg", "Galaxie lykt/vase koks liten", null, 529 },
                    { 8, null, "Galaxie lykt/vase koks medium 16,5 cm", "https://www.magnor.no/wp-content/uploads/2023/01/220721-489x530.jpg", "Galaxie lykt/vase koks medium", null, 849 },
                    { 9, null, "Galaxie lykt/vase koks stor 23 cm", "https://www.magnor.no/wp-content/uploads/2023/01/220731-497x530.jpg", "Galaxie lykt/vase koks stor", null, 1149 },
                    { 10, null, "Liten varm cognac 90 mm", "https://www.magnor.no/wp-content/uploads/2021/01/Iglo_07-460x530.jpg", "Iglo telykt", null, 629 },
                    { 11, null, "Stor varm cognac 220 mm", "https://www.magnor.no/wp-content/uploads/2021/01/Iglo_06-353x530.jpg", "Iglo stormlykt/vase", null, 1569 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_OrderId",
                table: "Carts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartId",
                table: "Products",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
