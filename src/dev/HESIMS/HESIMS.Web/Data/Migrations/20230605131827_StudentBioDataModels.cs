using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HESIMS.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentBioDataModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "StudentNumber",
                table: "Students",
                newName: "OtherNames");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NIN",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankSwiftCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchSwiftCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntermediaryBankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntermediaryBankSwiftCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentContacts_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScholarshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompletionYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Scholarships_ScholarshipId",
                        column: x => x.ScholarshipId,
                        principalTable: "Scholarships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicYear = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRegistrations_StudentCourses_StudentCourseId",
                        column: x => x.StudentCourseId,
                        principalTable: "StudentCourses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_StudentId",
                table: "BankAccounts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrations_StudentCourseId",
                table: "CourseRegistrations",
                column: "StudentCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentContacts_StudentId",
                table: "StudentContacts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_ScholarshipId",
                table: "StudentCourses",
                column: "ScholarshipId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentId",
                table: "StudentCourses",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "CourseRegistrations");

            migrationBuilder.DropTable(
                name: "StudentContacts");

            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "NIN",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "OtherNames",
                table: "Students",
                newName: "StudentNumber");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
