using Microsoft.EntityFrameworkCore.Migrations;

namespace Elearning.Database.Migrations
{
    public partial class ChangedLessonResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Lessons_LessonId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_LessonId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Resources");

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ResourceId",
                table: "Lessons",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Resources_ResourceId",
                table: "Lessons",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Resources_ResourceId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ResourceId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Lessons");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_LessonId",
                table: "Resources",
                column: "LessonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Lessons_LessonId",
                table: "Resources",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
