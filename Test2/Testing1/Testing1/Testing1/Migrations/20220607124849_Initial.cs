using Microsoft.EntityFrameworkCore.Migrations;

namespace Testing1.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(nullable: true),
                    AccountMail = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    RememberMe = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => new { x.Email, x.Password });
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(nullable: false),
                    imageURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountID", "AccountMail", "AccountName", "Password" },
                values: new object[,]
                {
                    { 1, "Master@gmail.com", "Admin", "Admin" },
                    { 2, "Moh6719@gmail.com", "Mohamad", "12345" }
                });

            migrationBuilder.InsertData(
                table: "Login",
                columns: new[] { "Email", "Password", "RememberMe" },
                values: new object[] { "Admin@gmail.com", "Admin@1", false });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Category", "Name", "Price", "imageURL" },
                values: new object[,]
                {
                    { 1, "Laptop", "Acer 2", 450m, null },
                    { 2, "Laptop", "Lenovo Thinkpad 3", 580m, null },
                    { 3, "PC", "Lenovo X2", 300m, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
