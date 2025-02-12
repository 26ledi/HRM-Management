using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fixedFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvaluationId",
                table: "UserTasks");

            migrationBuilder.RenameColumn(
                name: "assignementStatus",
                table: "TaskAssignments",
                newName: "AssignementStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignementStatus",
                table: "TaskAssignments",
                newName: "assignementStatus");

            migrationBuilder.AddColumn<Guid>(
                name: "EvaluationId",
                table: "UserTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
