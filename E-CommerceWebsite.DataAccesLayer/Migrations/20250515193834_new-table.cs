using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceWebsite.DataAccesLayer.Migrations
{
    /// <inheritdoc />
    public partial class newtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrderDeliveryAdresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    orderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDeliveryAdresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDeliveryAdresses_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDeliveryAdresses_Orders_orderId",
                        column: x => x.orderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryAdresses_orderId",
                table: "OrderDeliveryAdresses",
                column: "orderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryAdresses_userId",
                table: "OrderDeliveryAdresses",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDeliveryAdresses");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
