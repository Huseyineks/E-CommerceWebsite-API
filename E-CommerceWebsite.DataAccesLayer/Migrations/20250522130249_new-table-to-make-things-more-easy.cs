using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceWebsite.DataAccesLayer.Migrations
{
    /// <inheritdoc />
    public partial class newtabletomakethingsmoreeasy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdutNumber",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "masterOrderId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "masterOrderId",
                table: "OrderDeliveryAdresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MasterOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterOrders_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_masterOrderId",
                table: "Orders",
                column: "masterOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryAdresses_masterOrderId",
                table: "OrderDeliveryAdresses",
                column: "masterOrderId",
                unique: true,
                filter: "[masterOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MasterOrders_userId",
                table: "MasterOrders",
                column: "userId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDeliveryAdresses_MasterOrders_masterOrderId",
                table: "OrderDeliveryAdresses",
                column: "masterOrderId",
                principalTable: "MasterOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_MasterOrders_masterOrderId",
                table: "Orders",
                column: "masterOrderId",
                principalTable: "MasterOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveryAdresses_MasterOrders_masterOrderId",
                table: "OrderDeliveryAdresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_MasterOrders_masterOrderId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "MasterOrders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_masterOrderId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderDeliveryAdresses_masterOrderId",
                table: "OrderDeliveryAdresses");

            migrationBuilder.DropColumn(
                name: "masterOrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "masterOrderId",
                table: "OrderDeliveryAdresses");

            migrationBuilder.AddColumn<string>(
                name: "ProdutNumber",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
