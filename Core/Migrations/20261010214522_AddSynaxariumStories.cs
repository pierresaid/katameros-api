using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Katameros.Migrations
{
    /// <inheritdoc />
    public partial class AddSynaxariumStories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SynaxariumStories",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SynaxariumStories", x => x.StoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SynaxariumStories_Slug",
                table: "SynaxariumStories",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SynaxariumStories");
        }
    }
}
