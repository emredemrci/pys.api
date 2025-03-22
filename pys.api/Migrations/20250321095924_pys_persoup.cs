using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pys.api.Migrations
{
    /// <inheritdoc />
    public partial class pys_persoup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Personnel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Personnel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Personnel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                table: "Personnel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PersonnelSalary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelSalary", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonnelSalary");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "MotherName",
                table: "Personnel");
        }
    }
}
