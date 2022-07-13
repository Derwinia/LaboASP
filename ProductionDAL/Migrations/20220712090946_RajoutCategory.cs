using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductionDAL.Migrations
{
    public partial class RajoutCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_NAME_MIN",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.CheckConstraint("CK_NAME_MIN", "len(NAME) > 2");
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_NAME_MIN1",
                table: "Products",
                sql: "len(NAME) >= 4");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropCheckConstraint(
                name: "CK_NAME_MIN1",
                table: "Products");

            migrationBuilder.AddCheckConstraint(
                name: "CK_NAME_MIN",
                table: "Products",
                sql: "len(NAME) >= 4");
        }
    }
}
