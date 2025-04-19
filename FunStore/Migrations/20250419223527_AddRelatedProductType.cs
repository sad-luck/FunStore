using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunStore.Migrations
{
    /// <inheritdoc />
    public partial class AddRelatedProductType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RelatedProductType",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.Sql("UPDATE [dbo].[Products] SET [RelatedProductType] = 2 WHERE [Type] = 0 AND [Title] = 'Book club membership'");
            migrationBuilder.Sql("UPDATE [dbo].[Products] SET [RelatedProductType] = 1 WHERE [Type] = 0 AND [Title] = 'Video club membership'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedProductType",
                table: "Products");
        }
    }
}
