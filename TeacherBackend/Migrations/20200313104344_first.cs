using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherBackend.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SomeRandomInfo",
                table: "Subject",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SomeRandomInfo",
                table: "Subject");
        }
    }
}
