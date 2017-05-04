using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesEvaluation.WEB.Migrations
{
    public partial class AddDMAndSeasonToEvaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 804, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 8, 57, 51, 65, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 804, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 8, 57, 51, 65, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "DepartmentManagerId",
                table: "Evaluations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeasonId",
                table: "Evaluations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Evaluations",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 788, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 8, 57, 51, 60, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 788, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 8, 57, 51, 58, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_DepartmentManagerId",
                table: "Evaluations",
                column: "DepartmentManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_SeasonId",
                table: "Evaluations",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_AspNetUsers_DepartmentManagerId",
                table: "Evaluations",
                column: "DepartmentManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Seasons_SeasonId",
                table: "Evaluations",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_AspNetUsers_DepartmentManagerId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Seasons_SeasonId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_DepartmentManagerId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_SeasonId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DepartmentManagerId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Evaluations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 8, 57, 51, 65, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 804, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 8, 57, 51, 65, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 804, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 8, 57, 51, 60, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 788, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(2017, 5, 3, 8, 57, 51, 58, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 5, 3, 14, 10, 25, 788, DateTimeKind.Local));
        }
    }
}
