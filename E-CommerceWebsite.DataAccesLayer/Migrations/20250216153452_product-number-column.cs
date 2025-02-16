using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceWebsite.DataAccesLayer.Migrations
{
    /// <inheritdoc />
    public partial class productnumbercolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductNumber",
                table: "Orders");
        }
    }
}
