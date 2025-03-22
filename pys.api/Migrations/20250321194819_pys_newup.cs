using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pys.api.Migrations
{
    /// <inheritdoc />
    public partial class pys_newup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "PersonnelSalary");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PersonnelSalary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "PersonnelSalary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "PersonnelSalary",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
