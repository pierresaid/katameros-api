namespace Katameros.DTOs;

/// <summary>
/// One synaxarium story in one language, looked up by its permanent slug.
/// </summary>
public class SynaxariumStoryDetails
{
    /// <summary>Language-independent id shared by every translation of the story.</summary>
    public int StoryId { get; set; }
    /// <summary>Permanent English slug, the same in every language.</summary>
    public string Slug { get; set; } = "";
    /// <summary>Coptic month of the commemoration (1 to 13).</summary>
    public int Month { get; set; }
    /// <summary>Coptic day of the commemoration.</summary>
    public int Day { get; set; }
    public string Title { get; set; } = "";
    public string Html { get; set; } = "";
}
