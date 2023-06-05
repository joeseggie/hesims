using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HESIMS.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class CourseModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseLevel",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Institution",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstitutionCountry",
                table: "Courses");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseLevelId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InstitutionId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLevels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseLevelId",
                table: "Courses",
                column: "CourseLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstitutionId",
                table: "Courses",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseLevels_CourseLevelId",
                table: "Courses",
                column: "CourseLevelId",
                principalTable: "CourseLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Institutions_InstitutionId",
                table: "Courses",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseLevels_CourseLevelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Institutions_InstitutionId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseLevels");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseLevelId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstitutionId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseLevelId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "CourseLevel",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Institution",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstitutionCountry",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
