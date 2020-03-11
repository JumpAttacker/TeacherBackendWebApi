using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherBackend.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "LessonSubject");

            migrationBuilder.AddColumn<int>(
                name: "ClassNumber",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationTime",
                table: "Users",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "LessonSubject",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonSubject_UserModelId",
                table: "LessonSubject",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonSubject_Users_UserModelId",
                table: "LessonSubject",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonSubject_Users_UserModelId",
                table: "LessonSubject");

            migrationBuilder.DropIndex(
                name: "IX_LessonSubject_UserModelId",
                table: "LessonSubject");

            migrationBuilder.DropColumn(
                name: "ClassNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegistrationTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "LessonSubject");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "LessonSubject",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
