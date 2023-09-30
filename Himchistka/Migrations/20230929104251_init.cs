using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Himchistka.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhysicalPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Sex = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalPersons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    PhysicalPersonId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.PhysicalPersonId);
                    table.ForeignKey(
                        name: "FK_Client_PhysicalPerson",
                        column: x => x.PhysicalPersonId,
                        principalTable: "PhysicalPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    PhysicalPersonId = table.Column<int>(type: "int", nullable: false),
                    Post = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.PhysicalPersonId);
                    table.ForeignKey(
                        name: "FK_Employee_PhysicalPerson",
                        column: x => x.PhysicalPersonId,
                        principalTable: "PhysicalPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServicePrice = table.Column<decimal>(type: "money", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProcessingType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ServiceTimeSpent = table.Column<int>(type: "int", nullable: false),
                    ResourcesExpention = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Service",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "PhysicalPersonId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AcceptanceDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReadyDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReceptionPoint = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Delivery = table.Column<bool>(type: "bit", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "money", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Order",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "PhysicalPersonId");
                    table.ForeignKey(
                        name: "FK_Orders_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    DeclaredValue = table.Column<decimal>(type: "money", nullable: true),
                    FabricType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DirtStains = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Defects = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Label = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServiceId",
                table: "Orders",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_EmployeeId",
                table: "Services",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "PhysicalPersons");
        }
    }
}
