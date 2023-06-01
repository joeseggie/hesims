using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HESIMS.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseRemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Applicants_ApplicantId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "CourseApplications");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropTable(
                name: "ApplicationCycleCourses");

            migrationBuilder.DropTable(
                name: "ApplicationCycles");

            migrationBuilder.DropIndex(
                name: "IX_Students_ApplicantId",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherNames = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationCycles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScholarshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationCycles_Scholarships_ScholarshipId",
                        column: x => x.ScholarshipId,
                        principalTable: "Scholarships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationCycleCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationCycleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCycleCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationCycleCourses_ApplicationCycles_ApplicationCycleId",
                        column: x => x.ApplicationCycleId,
                        principalTable: "ApplicationCycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationCycleCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationCycleCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseApplications_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseApplications_ApplicationCycleCourses_ApplicationCycleCourseId",
                        column: x => x.ApplicationCycleCourseId,
                        principalTable: "ApplicationCycleCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ApplicantId",
                table: "Students",
                column: "ApplicantId",
                unique: true,
                filter: "[ApplicantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCycleCourses_ApplicationCycleId",
                table: "ApplicationCycleCourses",
                column: "ApplicationCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCycleCourses_CourseId",
                table: "ApplicationCycleCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCycles_ScholarshipId",
                table: "ApplicationCycles",
                column: "ScholarshipId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseApplications_ApplicantId",
                table: "CourseApplications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseApplications_ApplicationCycleCourseId",
                table: "CourseApplications",
                column: "ApplicationCycleCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Applicants_ApplicantId",
                table: "Students",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id");
        }
    }
}
