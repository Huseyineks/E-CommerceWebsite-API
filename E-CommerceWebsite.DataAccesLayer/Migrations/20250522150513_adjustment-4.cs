using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceWebsite.DataAccesLayer.Migrations
{
    /// <inheritdoc />
    public partial class adjustment4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MasterOrders_userId",
                table: "MasterOrders");

            migrationBuilder.CreateIndex(
                name: "IX_MasterOrders_userId",
                table: "MasterOrders",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MasterOrders_userId",
                table: "MasterOrders");

            migrationBuilder.CreateIndex(
                name: "IX_MasterOrders_userId",
                table: "MasterOrders",
                column: "userId",
                unique: true);
        }
    }
}
