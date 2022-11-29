using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calendar.Migrations
{
    public partial class TablesAddedsdfa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesTrainer_Trainer_usersTrainerId",
                table: "CoursesTrainer");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillTrainer_Trainer_UsersTrainerId",
                table: "SkillTrainer");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainer_AspNetUsers_Id",
                table: "Trainer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainer",
                table: "Trainer");

            migrationBuilder.DropIndex(
                name: "IX_Trainer_Id",
                table: "Trainer");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Trainer");

            migrationBuilder.RenameColumn(
                name: "UsersTrainerId",
                table: "SkillTrainer",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_SkillTrainer_UsersTrainerId",
                table: "SkillTrainer",
                newName: "IX_SkillTrainer_UsersId");

            migrationBuilder.RenameColumn(
                name: "usersTrainerId",
                table: "CoursesTrainer",
                newName: "usersId");

            migrationBuilder.RenameIndex(
                name: "IX_CoursesTrainer_usersTrainerId",
                table: "CoursesTrainer",
                newName: "IX_CoursesTrainer_usersId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Trainer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "SkillTrainer",
                newName: "UsersTrainerId");

            migrationBuilder.RenameIndex(
                name: "IX_SkillTrainer_UsersId",
                table: "SkillTrainer",
                newName: "IX_SkillTrainer_UsersTrainerId");

            migrationBuilder.RenameColumn(
                name: "usersId",
                table: "CoursesTrainer",
                newName: "usersTrainerId");

            migrationBuilder.RenameIndex(
                name: "IX_CoursesTrainer_usersId",
                table: "CoursesTrainer",
                newName: "IX_CoursesTrainer_usersTrainerId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Trainer",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "TrainerId",
                table: "Trainer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainer",
                table: "Trainer",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainer_Id",
                table: "Trainer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesTrainer_Trainer_usersTrainerId",
                table: "CoursesTrainer",
                column: "usersTrainerId",
                principalTable: "Trainer",
                principalColumn: "TrainerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillTrainer_Trainer_UsersTrainerId",
                table: "SkillTrainer",
                column: "UsersTrainerId",
                principalTable: "Trainer",
                principalColumn: "TrainerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainer_AspNetUsers_Id",
                table: "Trainer",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
