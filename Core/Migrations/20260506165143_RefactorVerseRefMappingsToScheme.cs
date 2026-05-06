using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Katameros.Migrations
{
    /// <inheritdoc />
    public partial class RefactorVerseRefMappingsToScheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Old rows are keyed by BibleId; after the column rename their values
            // become invalid (they were 7 / 10, but VersificationSchemeId values
            // must now reference VersificationSchemes.Id which we'll seed at 1).
            // Clearing here and re-seeding from the script is simpler than a
            // remap UPDATE.
            migrationBuilder.Sql("DELETE FROM VerseRefMappings");

            migrationBuilder.RenameColumn(
                name: "BibleId",
                table: "VerseRefMappings",
                newName: "VersificationSchemeId");

            migrationBuilder.AddColumn<int>(
                name: "VersificationSchemeId",
                table: "Bibles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VersificationSchemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersificationSchemes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VersificationSchemes");

            migrationBuilder.DropColumn(
                name: "VersificationSchemeId",
                table: "Bibles");

            migrationBuilder.RenameColumn(
                name: "VersificationSchemeId",
                table: "VerseRefMappings",
                newName: "BibleId");
        }
    }
}
