using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreP4.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorSurname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ISBN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    VAT = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.ISBN);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPESEL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeePESEL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "AuthorDTOBookDTO",
                columns: table => new
                {
                    AuthorsAuthorID = table.Column<int>(type: "int", nullable: false),
                    BooksISBN = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorDTOBookDTO", x => new { x.AuthorsAuthorID, x.BooksISBN });
                    table.ForeignKey(
                        name: "FK_AuthorDTOBookDTO_Authors_AuthorsAuthorID",
                        column: x => x.AuthorsAuthorID,
                        principalTable: "Authors",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorDTOBookDTO_Books_BooksISBN",
                        column: x => x.BooksISBN,
                        principalTable: "Books",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderEmployeeIDEmployeeID = table.Column<int>(type: "int", nullable: true),
                    OrderCustomerIDCustomerID = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_OrderCustomerIDCustomerID",
                        column: x => x.OrderCustomerIDCustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK_Orders_Employees_OrderEmployeeIDEmployeeID",
                        column: x => x.OrderEmployeeIDEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(type: "int", nullable: false),
                    OrderItemBookISBN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderItemOrderOrderID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BookPrice = table.Column<float>(type: "real", nullable: false),
                    BookVAT = table.Column<float>(type: "real", nullable: true),
                    BookNettoValue = table.Column<float>(type: "real", nullable: false),
                    BookBruttoValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItems_Books_OrderItemBookISBN",
                        column: x => x.OrderItemBookISBN,
                        principalTable: "Books",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderItemOrderOrderID",
                        column: x => x.OrderItemOrderOrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorDTOBookDTO_BooksISBN",
                table: "AuthorDTOBookDTO",
                column: "BooksISBN");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderItemBookISBN",
                table: "OrderItems",
                column: "OrderItemBookISBN");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderItemOrderOrderID",
                table: "OrderItems",
                column: "OrderItemOrderOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCustomerIDCustomerID",
                table: "Orders",
                column: "OrderCustomerIDCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderEmployeeIDEmployeeID",
                table: "Orders",
                column: "OrderEmployeeIDEmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorDTOBookDTO");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
