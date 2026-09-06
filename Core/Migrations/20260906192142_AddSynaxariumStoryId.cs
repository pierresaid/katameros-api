using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Katameros.Migrations
{
    /// <inheritdoc />
    public partial class AddSynaxariumStoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoryId",
                table: "Synaxarium",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FeastsTranslations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Fasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fasts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FastsTranslations",
                columns: table => new
                {
                    FastId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FastsTranslations", x => new { x.FastId, x.LanguageId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fasts");

            migrationBuilder.DropTable(
                name: "FastsTranslations");

            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "Synaxarium");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FeastsTranslations");
        }
    }
}
