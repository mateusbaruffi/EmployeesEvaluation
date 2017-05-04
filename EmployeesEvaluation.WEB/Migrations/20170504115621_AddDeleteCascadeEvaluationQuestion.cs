using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesEvaluation.WEB.Migrations
{
    public partial class AddDeleteCascadeEvaluationQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationQuestion_Evaluations_EvaluationId",
                table: "EvaluationQuestion");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 4, 8, 56, 21, 339, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 804, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 4, 8, 56, 21, 339, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 804, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 4, 8, 56, 21, 334, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 788, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 4, 8, 56, 21, 333, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 788, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationQuestion_Evaluations_EvaluationId",
                table: "EvaluationQuestion",
                column: "EvaluationId",
                principalTable: "Evaluations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationQuestion_Evaluations_EvaluationId",
                table: "EvaluationQuestion");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 804, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 4, 8, 56, 21, 339, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 804, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 4, 8, 56, 21, 339, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 788, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 4, 8, 56, 21, 334, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 788, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 4, 8, 56, 21, 333, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationQuestion_Evaluations_EvaluationId",
                table: "EvaluationQuestion",
                column: "EvaluationId",
                principalTable: "Evaluations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
