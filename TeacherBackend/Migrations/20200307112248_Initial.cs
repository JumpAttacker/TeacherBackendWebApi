using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "LessonSubject",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_LessonSubject", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Age = table.Column<int>(),
                    UserType = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "Lessons",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(),
                    Minutes = table.Column<int>(),
                    Price = table.Column<int>(),
                    LessonSubjectId = table.Column<int>(nullable: true),
                    TeacherId = table.Column<int>(nullable: true),
                    StudentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        "FK_Lessons_LessonSubject_LessonSubjectId",
                        x => x.LessonSubjectId,
                        "LessonSubject",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Lessons_Users_StudentId",
                        x => x.StudentId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Lessons_Users_TeacherId",
                        x => x.TeacherId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Lessons_LessonSubjectId",
                "Lessons",
                "LessonSubjectId");

            migrationBuilder.CreateIndex(
                "IX_Lessons_StudentId",
                "Lessons",
                "StudentId");

            migrationBuilder.CreateIndex(
                "IX_Lessons_TeacherId",
                "Lessons",
                "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Lessons");

            migrationBuilder.DropTable(
                "LessonSubject");

            migrationBuilder.DropTable(
                "Users");
        }
    }
}