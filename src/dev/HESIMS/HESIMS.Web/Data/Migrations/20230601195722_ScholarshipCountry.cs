using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HESIMS.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScholarshipCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Scholarships_Name",
                table: "Scholarships");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Scholarships");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Scholarships",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "Scholarships",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scholarships_CountryId",
                table: "Scholarships",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scholarships_Countries_CountryId",
                table: "Scholarships",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scholarships_Countries_CountryId",
                table: "Scholarships");

            migrationBuilder.DropIndex(
                name: "IX_Scholarships_CountryId",
                table: "Scholarships");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Scholarships");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Scholarships",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Scholarships",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Scholarships_Name",
                table: "Scholarships",
                column: "Name",
                unique: true);
        }
    }
}
