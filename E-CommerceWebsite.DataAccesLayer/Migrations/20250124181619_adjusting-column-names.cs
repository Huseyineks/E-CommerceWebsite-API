using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceWebsite.DataAccesLayer.Migrations
{
    /// <inheritdoc />
    public partial class adjustingcolumnnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rowGuid",
                table: "Products",
                newName: "RowGuid");

            migrationBuilder.RenameColumn(
                name: "productPrice",
                table: "Products",
                newName: "ProductPrice");

            migrationBuilder.RenameColumn(
                name: "productName",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "productDescription",
                table: "Products",
                newName: "ProductDescription");

            migrationBuilder.RenameColumn(
                name: "productPrice",
                table: "Orders",
                newName: "ProductPrice");

            migrationBuilder.RenameColumn(
                name: "productName",
                table: "Orders",
                newName: "ProductName");

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "RowGuid",
                table: "Products",
                newName: "rowGuid");

            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "Products",
                newName: "productPrice");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "productName");

            migrationBuilder.RenameColumn(
                name: "ProductDescription",
                table: "Products",
                newName: "productDescription");

            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "Orders",
                newName: "productPrice");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Orders",
                newName: "productName");
        }
    }
}
