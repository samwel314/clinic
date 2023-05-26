using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinic.Migrations
{
    /// <inheritdoc />
    public partial class Dr_picture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dr_picture",
                table: "doctors",
                type: "VARCHAR",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "doctors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Dr_picture",
                value: null);

            migrationBuilder.UpdateData(
                table: "doctors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Dr_picture",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dr_picture",
                table: "doctors");
        }
    }
}
