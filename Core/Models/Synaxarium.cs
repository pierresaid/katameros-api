namespace Katameros.Models;

public class Synaxarium
{
    public int Id { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public string Title { get; set; }
    public int Order { get; set; }
    public string Text { get; set; }
    public int LanguageId { get; set; }
    /// <summary>
    /// Language-independent identifier shared by all translations of the same
    /// synaxarium story. Encoded as CopticMonth * 10000 + CopticDay * 100 + sequence.
    /// </summary>
    public int? StoryId { get; set; }
}
