using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace clinic.Migrations
{
    /// <inheritdoc />
    public partial class CreateDoctorsContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fname = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    Lname = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Message = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    D_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contacts_doctors_D_id",
                        column: x => x.D_id,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "doctors",
                columns: new[] { "Id", "Email", "Fname", "Lname", "Password", "Phone" },
                values: new object[,]
                {
                    { 1, "DrSamwel@gmail.com", "Samwel", "Marzouk", "Sa147258", "01005415303" },
                    { 2, "DrMina@gmail.com", "Mina", "Marzouk", "Ma147258", "01005415404" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_contacts_D_id",
                table: "contacts",
                column: "D_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "doctors");
        }
    }
}
