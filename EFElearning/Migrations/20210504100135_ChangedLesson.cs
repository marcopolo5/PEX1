using Microsoft.EntityFrameworkCore.Migrations;

namespace Elearning.Database.Migrations
{
    public partial class ChangedLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resources_LessonId",
                table: "Resources");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_LessonId",
                table: "Resources",
                column: "LessonId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resources_LessonId",
                table: "Resources");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_LessonId",
                table: "Resources",
                column: "LessonId");
        }
    }
}
