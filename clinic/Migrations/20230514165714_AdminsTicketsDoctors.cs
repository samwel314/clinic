using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinic.Migrations
{
    /// <inheritdoc />
    public partial class AdminsTicketsDoctors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_admins_doctors_D_id",
                        column: x => x.D_id,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    D_ofTic = table.Column<DateTime>(type: "datetime2", nullable: false),
                    P_id = table.Column<int>(type: "int", nullable: false),
                    A_id = table.Column<int>(type: "int", nullable: true),
                    D_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tickets_admins_A_id",
                        column: x => x.A_id,
                        principalTable: "admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tickets_doctors_D_id",
                        column: x => x.D_id,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_patients_P_id",
                        column: x => x.P_id,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_admins_D_id",
                table: "admins",
                column: "D_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_A_id",
                table: "tickets",
                column: "A_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_D_id",
                table: "tickets",
                column: "D_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_P_id",
                table: "tickets",
                column: "P_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "admins");
        }
    }
}
