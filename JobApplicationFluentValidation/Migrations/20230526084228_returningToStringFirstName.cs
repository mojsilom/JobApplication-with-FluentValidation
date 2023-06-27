using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplicationFluentValidation.Migrations
{
    /// <inheritdoc />
    public partial class returningToStringFirstName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName_Value",
                table: "Applicants",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Applicants",
                newName: "FirstName_Value");
        }
    }
}
