using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Discount.Grpc.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Amount", "Description", "ProductName" },
                values: new object[,]
                {
                    { 1, 1, "Smartphone with ai", "Samsung S25" },
                    { 2, 1, "Smartphone with ai", "Samsung S25" },
                    { 3, 1, "Smartphone with ai", "Samsung S25" },
                    { 4, 1, "Smartphone with ai", "Samsung S25" },
                    { 5, 1, "Smartphone with ai", "Samsung S25" },
                    { 6, 1000, "Smartphone with ai", "Samsung S25" },
                    { 7, 250, "Smartphone with ai", "Samsung S25" },
                    { 8, 750, "Smartphone with ai", "Samsung S25" },
                    { 9, 999, "Smartphone with ai", "Samsung S25" },
                    { 10, 1200, "Smartphone with ai", "Samsung S25" },
                    { 11, 1500, "Smartphone with ai", "Samsung S25" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
