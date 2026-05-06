using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Katameros.Migrations
{
    /// <inheritdoc />
    public partial class AddVerseRefMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VerseRefMappings",
                columns: table => new
                {
                    BibleId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    Chapter = table.Column<int>(type: "INTEGER", nullable: false),
                    Offset = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerseRefMappings", x => new { x.BibleId, x.BookId, x.Chapter });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerseRefMappings");
        }
    }
}
