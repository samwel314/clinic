using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinic.Migrations
{
    /// <inheritdoc />
    public partial class CommentTable : Migration
    {
        /// <inheritdoc />                                   
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Massege = table.Column<string>(type: "varchar", nullable: false),
                    D_Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    P_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comments_patients_P_ID",
                        column: x => x.P_ID,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_P_ID",
                table: "comments",
                column: "P_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");
        }
    }
}
