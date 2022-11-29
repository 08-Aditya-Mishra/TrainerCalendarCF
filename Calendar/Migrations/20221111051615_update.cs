using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calendar.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesTrainer_Trainer_usersId",
                table: "CoursesTrainer");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillTrainer_Trainer_UsersId",
                table: "SkillTrainer");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainer_AspNetUsers_Id",
                table: "Trainer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainer",
                table: "Trainer");

            migrationBuilder.RenameTable(
                name: "Trainer",
                newName: "trainers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainers",
                table: "trainers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesTrainer_trainers_usersId",
                table: "CoursesTrainer",
                column: "usersId",
                principalTable: "trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillTrainer_trainers_UsersId",
                table: "SkillTrainer",
                column: "UsersId",
                principalTable: "trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trainers_AspNetUsers_Id",
                table: "trainers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesTrainer_trainers_usersId",
                table: "CoursesTrainer");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillTrainer_trainers_UsersId",
                table: "SkillTrainer");

            migrationBuilder.DropForeignKey(
                name: "FK_trainers_AspNetUsers_Id",
                table: "trainers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainers",
                table: "trainers");

            migrationBuilder.RenameTable(
                name: "trainers",
                newName: "Trainer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainer",
                table: "Trainer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesTrainer_Trainer_usersId",
                table: "CoursesTrainer",
                column: "usersId",
                principalTable: "Trainer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillTrainer_Trainer_UsersId",
                table: "SkillTrainer",
                column: "UsersId",
                principalTable: "Trainer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainer_AspNetUsers_Id",
                table: "Trainer",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
