using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductionDAL.Migrations
{
    public partial class english : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "prix",
                table: "Products",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "prix");
        }
    }
}
