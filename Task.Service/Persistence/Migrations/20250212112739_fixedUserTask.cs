using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fixedUserTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_TaskEvaluations_TaskEvaluationId",
                table: "UserTasks");

            migrationBuilder.DropIndex(
                name: "IX_UserTasks_TaskEvaluationId",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "TaskEvaluationId",
                table: "UserTasks");

            migrationBuilder.AddColumn<Guid>(
                name: "UserTaskId",
                table: "TaskEvaluations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvaluations_UserTaskId",
                table: "TaskEvaluations",
                column: "UserTaskId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskEvaluations_UserTasks_UserTaskId",
                table: "TaskEvaluations",
                column: "UserTaskId",
                principalTable: "UserTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskEvaluations_UserTasks_UserTaskId",
                table: "TaskEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_TaskEvaluations_UserTaskId",
                table: "TaskEvaluations");

            migrationBuilder.DropColumn(
                name: "UserTaskId",
                table: "TaskEvaluations");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskEvaluationId",
                table: "UserTasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_TaskEvaluationId",
                table: "UserTasks",
                column: "TaskEvaluationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_TaskEvaluations_TaskEvaluationId",
                table: "UserTasks",
                column: "TaskEvaluationId",
                principalTable: "TaskEvaluations",
                principalColumn: "Id");
        }
    }
}
