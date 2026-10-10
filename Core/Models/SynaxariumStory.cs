namespace Katameros.Models;

/// <summary>
/// One row per synaxarium story, shared by all its translations (the Synaxarium rows
/// with the same StoryId). Holds the language-independent facts about the story.
/// </summary>
public class SynaxariumStory
{
    /// <summary>
    /// Same value as Synaxarium.StoryId: CopticMonth * 10000 + CopticDay * 100 + sequence.
    /// </summary>
    public int StoryId { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    /// <summary>
    /// Permanent English URL slug used for every language. Never regenerate it from the
    /// title: shared links depend on it.
    /// </summary>
    public string Slug { get; set; } = "";
}
