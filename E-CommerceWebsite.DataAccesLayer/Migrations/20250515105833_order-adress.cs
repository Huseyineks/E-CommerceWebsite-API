using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceWebsite.DataAccesLayer.Migrations
{
    /// <inheritdoc />
    public partial class orderadress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Orders");
        }
    }
}
