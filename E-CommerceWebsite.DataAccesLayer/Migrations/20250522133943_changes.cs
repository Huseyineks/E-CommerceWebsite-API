using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceWebsite.DataAccesLayer.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveryAdresses_AspNetUsers_userId",
                table: "OrderDeliveryAdresses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveryAdresses_Orders_orderId",
                table: "OrderDeliveryAdresses");

            migrationBuilder.DropIndex(
                name: "IX_OrderDeliveryAdresses_orderId",
                table: "OrderDeliveryAdresses");

            migrationBuilder.DropIndex(
                name: "IX_OrderDeliveryAdresses_userId",
                table: "OrderDeliveryAdresses");

            migrationBuilder.DropColumn(
                name: "orderId",
                table: "OrderDeliveryAdresses");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "OrderDeliveryAdresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "orderId",
                table: "OrderDeliveryAdresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "OrderDeliveryAdresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryAdresses_orderId",
                table: "OrderDeliveryAdresses",
                column: "orderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryAdresses_userId",
                table: "OrderDeliveryAdresses",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDeliveryAdresses_AspNetUsers_userId",
                table: "OrderDeliveryAdresses",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDeliveryAdresses_Orders_orderId",
                table: "OrderDeliveryAdresses",
                column: "orderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
