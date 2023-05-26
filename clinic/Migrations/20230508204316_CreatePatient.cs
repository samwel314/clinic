using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinic.Migrations
{
    /// <inheritdoc />
    public partial class CreatePatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fname = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    Lname = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<string>(type: "varchar(6)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patients");
        }
    }
}
