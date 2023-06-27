using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplicationFluentValidation.Migrations
{
    /// <inheritdoc />
    public partial class updateFirstNameValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Applicants",
                newName: "FirstName_Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName_Value",
                table: "Applicants",
                newName: "FirstName");
        }
    }
}
