using Katameros.Enums;

namespace Katameros.DTOs;

public class Reading
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Introduction { get; set; }
    public string Conclusion { get; set; }
    public List<Passage> Passages { get; set; }
    public string Html { get; set; }
    /// <summary>
    /// For synaxarium readings: language-independent id shared by all translations of the same story.
    /// </summary>
    public int? StoryId { get; set; }
    public Reading(ReadingType readingType)
    {
        Id = (int)readingType;
    }
}