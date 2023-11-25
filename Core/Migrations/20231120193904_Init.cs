using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Katameros.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnualReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Month_Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Month_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    Other = table.Column<string>(type: "TEXT", nullable: true),
                    Day_Tune = table.Column<string>(type: "TEXT", nullable: true),
                    DayName = table.Column<string>(type: "TEXT", nullable: true),
                    Season = table.Column<string>(type: "TEXT", nullable: true),
                    V_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    V_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    M_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    M_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    P_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    C_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    X_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    L_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    L_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualReadings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bibles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bibles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BooksTranslations",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksTranslations", x => new { x.BookId, x.LanguageId });
                });

            migrationBuilder.CreateTable(
                name: "Feasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feasts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeastsTranslations",
                columns: table => new
                {
                    FeastId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeastsTranslations", x => new { x.FeastId, x.LanguageId });
                });

            migrationBuilder.CreateTable(
                name: "GreatLentReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Week = table.Column<int>(type: "INTEGER", nullable: false),
                    DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    DayName = table.Column<string>(type: "TEXT", nullable: true),
                    Seasonal_Tune = table.Column<string>(type: "TEXT", nullable: true),
                    Weather_Prayers = table.Column<string>(type: "TEXT", nullable: true),
                    V_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: true),
                    V_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: true),
                    M_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    M_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    P_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    C_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    X_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    L_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    L_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Prophecy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreatLentReadings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PentecostReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Week = table.Column<int>(type: "INTEGER", nullable: false),
                    DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    DayName = table.Column<string>(type: "TEXT", nullable: true),
                    V_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: true),
                    V_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: true),
                    M_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    M_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    P_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    C_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    X_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    L_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    L_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PentecostReadings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReadingsMetadatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingsMetadatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReadingsMetadatasTranslations",
                columns: table => new
                {
                    ReadingId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReadingsMetadatasId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingsMetadatasTranslations", x => new { x.ReadingId, x.ReadingsMetadatasId, x.LanguageId });
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectionsMetadatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionsMetadatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectionsMetadatasTranslations",
                columns: table => new
                {
                    SectionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    SectionsMetadatasId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionsMetadatasTranslations", x => new { x.SectionsId, x.SectionsMetadatasId, x.LanguageId });
                });

            migrationBuilder.CreateTable(
                name: "Sentences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentencesTranslations",
                columns: table => new
                {
                    SentenceId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentencesTranslations", x => new { x.SentenceId, x.LanguageId });
                });

            migrationBuilder.CreateTable(
                name: "SubSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubSectionsMetadatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSectionsMetadatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubSectionsMetadatasTranslations",
                columns: table => new
                {
                    SubSectionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubSectionsMetadatasId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSectionsMetadatasTranslations", x => new { x.SubSectionsId, x.SubSectionsMetadatasId, x.LanguageId });
                });

            migrationBuilder.CreateTable(
                name: "SundayReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Month_Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Month_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    Other = table.Column<string>(type: "TEXT", nullable: true),
                    Day_Tune = table.Column<string>(type: "TEXT", nullable: true),
                    DayName = table.Column<string>(type: "TEXT", nullable: true),
                    Season = table.Column<string>(type: "TEXT", nullable: true),
                    V_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    V_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    M_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    M_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    P_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    C_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    X_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    L_Psalm_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    L_Gospel_Ref = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SundayReadings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Synaxarium",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synaxarium", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Verses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BibleId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    Chapter = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualReadings");

            migrationBuilder.DropTable(
                name: "Bibles");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "BooksTranslations");

            migrationBuilder.DropTable(
                name: "Feasts");

            migrationBuilder.DropTable(
                name: "FeastsTranslations");

            migrationBuilder.DropTable(
                name: "GreatLentReadings");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "PentecostReadings");

            migrationBuilder.DropTable(
                name: "Readings");

            migrationBuilder.DropTable(
                name: "ReadingsMetadatas");

            migrationBuilder.DropTable(
                name: "ReadingsMetadatasTranslations");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "SectionsMetadatas");

            migrationBuilder.DropTable(
                name: "SectionsMetadatasTranslations");

            migrationBuilder.DropTable(
                name: "Sentences");

            migrationBuilder.DropTable(
                name: "SentencesTranslations");

            migrationBuilder.DropTable(
                name: "SubSections");

            migrationBuilder.DropTable(
                name: "SubSectionsMetadatas");

            migrationBuilder.DropTable(
                name: "SubSectionsMetadatasTranslations");

            migrationBuilder.DropTable(
                name: "SundayReadings");

            migrationBuilder.DropTable(
                name: "Synaxarium");

            migrationBuilder.DropTable(
                name: "Verses");
        }
    }
}
