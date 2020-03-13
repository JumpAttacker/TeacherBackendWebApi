using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherBackend.Migrations
{
    public partial class first2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SomeRandomInfo",
                table: "Subject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SomeRandomInfo",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
