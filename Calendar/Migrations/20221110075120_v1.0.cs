using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calendar.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_skills_course_CoursesCourseId",
                table: "skills");

            migrationBuilder.DropIndex(
                name: "IX_skills_CoursesCourseId",
                table: "skills");

            migrationBuilder.DropColumn(
                name: "CoursesCourseId",
                table: "skills");

            migrationBuilder.CreateTable(
                name: "CoursesSkill",
                columns: table => new
                {
                    CoursesCourseId = table.Column<int>(type: "int", nullable: false),
                    skillsSkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesSkill", x => new { x.CoursesCourseId, x.skillsSkillId });
                    table.ForeignKey(
                        name: "FK_CoursesSkill_course_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalTable: "course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesSkill_skills_skillsSkillId",
                        column: x => x.skillsSkillId,
                        principalTable: "skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesSkill_skillsSkillId",
                table: "CoursesSkill",
                column: "skillsSkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesSkill");

            migrationBuilder.AddColumn<int>(
                name: "CoursesCourseId",
                table: "skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_skills_CoursesCourseId",
                table: "skills",
                column: "CoursesCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_skills_course_CoursesCourseId",
                table: "skills",
                column: "CoursesCourseId",
                principalTable: "course",
                principalColumn: "CourseId");
        }
    }
}
