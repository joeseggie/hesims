using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HESIMS.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class InstitutionModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Institution_Countries_CountryId",
                table: "Institution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Institution",
                table: "Institution");

            migrationBuilder.RenameTable(
                name: "Institution",
                newName: "Institutions");

            migrationBuilder.RenameIndex(
                name: "IX_Institution_CountryId",
                table: "Institutions",
                newName: "IX_Institutions_CountryId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Institutions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Institutions",
                table: "Institutions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Institutions_Countries_CountryId",
                table: "Institutions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Institutions_Countries_CountryId",
                table: "Institutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Institutions",
                table: "Institutions");

            migrationBuilder.RenameTable(
                name: "Institutions",
                newName: "Institution");

            migrationBuilder.RenameIndex(
                name: "IX_Institutions_CountryId",
                table: "Institution",
                newName: "IX_Institution_CountryId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Institution",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Institution",
                table: "Institution",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Institution_Countries_CountryId",
                table: "Institution",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
