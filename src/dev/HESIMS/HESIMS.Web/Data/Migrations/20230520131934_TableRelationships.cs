using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HESIMS.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationCycle_Scholarship_ScholarshipId",
                table: "ApplicationCycle");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationCycleCourse_ApplicationCycle_ApplicationCycleId",
                table: "ApplicationCycleCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationCycleCourse_Course_CourseId",
                table: "ApplicationCycleCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseApplication_Applicant_ApplicantId",
                table: "CourseApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseApplication_ApplicationCycleCourse_ApplicationCycleCourseId",
                table: "CourseApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scholarship",
                table: "Scholarship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseApplication",
                table: "CourseApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationCycleCourse",
                table: "ApplicationCycleCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationCycle",
                table: "ApplicationCycle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applicant",
                table: "Applicant");

            migrationBuilder.RenameTable(
                name: "Scholarship",
                newName: "Scholarships");

            migrationBuilder.RenameTable(
                name: "CourseApplication",
                newName: "CourseApplications");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "ApplicationCycleCourse",
                newName: "ApplicationCycleCourses");

            migrationBuilder.RenameTable(
                name: "ApplicationCycle",
                newName: "ApplicationCycles");

            migrationBuilder.RenameTable(
                name: "Applicant",
                newName: "Applicants");

            migrationBuilder.RenameIndex(
                name: "IX_Scholarship_Country",
                table: "Scholarships",
                newName: "IX_Scholarships_Country");

            migrationBuilder.RenameIndex(
                name: "IX_CourseApplication_ApplicationCycleCourseId",
                table: "CourseApplications",
                newName: "IX_CourseApplications_ApplicationCycleCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseApplication_ApplicantId",
                table: "CourseApplications",
                newName: "IX_CourseApplications_ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_Name",
                table: "Courses",
                newName: "IX_Courses_Name");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationCycleCourse_CourseId",
                table: "ApplicationCycleCourses",
                newName: "IX_ApplicationCycleCourses_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationCycleCourse_ApplicationCycleId",
                table: "ApplicationCycleCourses",
                newName: "IX_ApplicationCycleCourses_ApplicationCycleId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationCycle_ScholarshipId",
                table: "ApplicationCycles",
                newName: "IX_ApplicationCycles_ScholarshipId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scholarships",
                table: "Scholarships",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseApplications",
                table: "CourseApplications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationCycleCourses",
                table: "ApplicationCycleCourses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationCycles",
                table: "ApplicationCycles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applicants",
                table: "Applicants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationCycleCourses_ApplicationCycles_ApplicationCycleId",
                table: "ApplicationCycleCourses",
                column: "ApplicationCycleId",
                principalTable: "ApplicationCycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationCycleCourses_Courses_CourseId",
                table: "ApplicationCycleCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationCycles_Scholarships_ScholarshipId",
                table: "ApplicationCycles",
                column: "ScholarshipId",
                principalTable: "Scholarships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseApplications_Applicants_ApplicantId",
                table: "CourseApplications",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseApplications_ApplicationCycleCourses_ApplicationCycleCourseId",
                table: "CourseApplications",
                column: "ApplicationCycleCourseId",
                principalTable: "ApplicationCycleCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationCycleCourses_ApplicationCycles_ApplicationCycleId",
                table: "ApplicationCycleCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationCycleCourses_Courses_CourseId",
                table: "ApplicationCycleCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationCycles_Scholarships_ScholarshipId",
                table: "ApplicationCycles");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseApplications_Applicants_ApplicantId",
                table: "CourseApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseApplications_ApplicationCycleCourses_ApplicationCycleCourseId",
                table: "CourseApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scholarships",
                table: "Scholarships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseApplications",
                table: "CourseApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationCycles",
                table: "ApplicationCycles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationCycleCourses",
                table: "ApplicationCycleCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applicants",
                table: "Applicants");

            migrationBuilder.RenameTable(
                name: "Scholarships",
                newName: "Scholarship");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "CourseApplications",
                newName: "CourseApplication");

            migrationBuilder.RenameTable(
                name: "ApplicationCycles",
                newName: "ApplicationCycle");

            migrationBuilder.RenameTable(
                name: "ApplicationCycleCourses",
                newName: "ApplicationCycleCourse");

            migrationBuilder.RenameTable(
                name: "Applicants",
                newName: "Applicant");

            migrationBuilder.RenameIndex(
                name: "IX_Scholarships_Country",
                table: "Scholarship",
                newName: "IX_Scholarship_Country");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_Name",
                table: "Course",
                newName: "IX_Course_Name");

            migrationBuilder.RenameIndex(
                name: "IX_CourseApplications_ApplicationCycleCourseId",
                table: "CourseApplication",
                newName: "IX_CourseApplication_ApplicationCycleCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseApplications_ApplicantId",
                table: "CourseApplication",
                newName: "IX_CourseApplication_ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationCycles_ScholarshipId",
                table: "ApplicationCycle",
                newName: "IX_ApplicationCycle_ScholarshipId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationCycleCourses_CourseId",
                table: "ApplicationCycleCourse",
                newName: "IX_ApplicationCycleCourse_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationCycleCourses_ApplicationCycleId",
                table: "ApplicationCycleCourse",
                newName: "IX_ApplicationCycleCourse_ApplicationCycleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scholarship",
                table: "Scholarship",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseApplication",
                table: "CourseApplication",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationCycle",
                table: "ApplicationCycle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationCycleCourse",
                table: "ApplicationCycleCourse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applicant",
                table: "Applicant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationCycle_Scholarship_ScholarshipId",
                table: "ApplicationCycle",
                column: "ScholarshipId",
                principalTable: "Scholarship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationCycleCourse_ApplicationCycle_ApplicationCycleId",
                table: "ApplicationCycleCourse",
                column: "ApplicationCycleId",
                principalTable: "ApplicationCycle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationCycleCourse_Course_CourseId",
                table: "ApplicationCycleCourse",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseApplication_Applicant_ApplicantId",
                table: "CourseApplication",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseApplication_ApplicationCycleCourse_ApplicationCycleCourseId",
                table: "CourseApplication",
                column: "ApplicationCycleCourseId",
                principalTable: "ApplicationCycleCourse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
