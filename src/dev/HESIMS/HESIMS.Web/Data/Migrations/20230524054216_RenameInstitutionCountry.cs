using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HESIMS.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameInstitutionCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InsititutionCountry",
                table: "Courses",
                newName: "InstitutionCountry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InstitutionCountry",
                table: "Courses",
                newName: "InsititutionCountry");
        }
    }
}
