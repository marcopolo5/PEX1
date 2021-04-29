using Microsoft.EntityFrameworkCore.Migrations;

namespace Elearning.Database.Migrations
{
    public partial class fixedQuizes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizs_QuizId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizs_Courses_CourseId",
                table: "Quizs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quizs",
                table: "Quizs");

            migrationBuilder.RenameTable(
                name: "Quizs",
                newName: "Quizes");

            migrationBuilder.RenameIndex(
                name: "IX_Quizs_CourseId",
                table: "Quizes",
                newName: "IX_Quizes_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quizes",
                table: "Quizes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizes_QuizId",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_Courses_CourseId",
                table: "Quizes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizes_QuizId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_Courses_CourseId",
                table: "Quizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quizes",
                table: "Quizes");

            migrationBuilder.RenameTable(
                name: "Quizes",
                newName: "Quizs");

            migrationBuilder.RenameIndex(
                name: "IX_Quizes_CourseId",
                table: "Quizs",
                newName: "IX_Quizs_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quizs",
                table: "Quizs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizs_QuizId",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizs_Courses_CourseId",
                table: "Quizs",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
