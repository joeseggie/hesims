using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HESIMS.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherNames = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scholarship",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scholarship", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationCycle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScholarshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCycle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationCycle_Scholarship_ScholarshipId",
                        column: x => x.ScholarshipId,
                        principalTable: "Scholarship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationCycleCourse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationCycleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCycleCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationCycleCourse_ApplicationCycle_ApplicationCycleId",
                        column: x => x.ApplicationCycleId,
                        principalTable: "ApplicationCycle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationCycleCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseApplication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationCycleCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseApplication_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseApplication_ApplicationCycleCourse_ApplicationCycleCourseId",
                        column: x => x.ApplicationCycleCourseId,
                        principalTable: "ApplicationCycleCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCycle_ScholarshipId",
                table: "ApplicationCycle",
                column: "ScholarshipId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCycleCourse_ApplicationCycleId",
                table: "ApplicationCycleCourse",
                column: "ApplicationCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCycleCourse_CourseId",
                table: "ApplicationCycleCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Name",
                table: "Course",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseApplication_ApplicantId",
                table: "CourseApplication",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseApplication_ApplicationCycleCourseId",
                table: "CourseApplication",
                column: "ApplicationCycleCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Scholarship_Country",
                table: "Scholarship",
                column: "Country",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseApplication");

            migrationBuilder.DropTable(
                name: "Applicant");

            migrationBuilder.DropTable(
                name: "ApplicationCycleCourse");

            migrationBuilder.DropTable(
                name: "ApplicationCycle");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Scholarship");
        }
    }
}
