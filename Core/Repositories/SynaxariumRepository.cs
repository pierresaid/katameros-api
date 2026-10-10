using Katameros.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Katameros.Repositories;

public class SynaxariumRepository(DatabaseContext _context)
{
    public Task<bool> LanguageExists(int languageId) =>
        _context.Languages.AnyAsync(l => l.Id == languageId);

    /// <summary>
    /// Finds a story by its slug. StoryExists is false when no story has this slug;
    /// Story is null when the story exists but not in the requested language.
    /// </summary>
    public async Task<(bool StoryExists, SynaxariumStoryDetails? Story)> GetStory(string slug, int languageId)
    {
        var story = await _context.SynaxariumStories.AsNoTracking()
            .FirstOrDefaultAsync(s => s.Slug == slug);
        if (story == null)
            return (false, null);

        var translation = await _context.Synaxarium.AsNoTracking()
            .Where(s => s.StoryId == story.StoryId && s.LanguageId == languageId)
            .Select(s => new { s.Title, s.Text })
            .FirstOrDefaultAsync();
        if (translation == null)
            return (true, null);

        return (true, new SynaxariumStoryDetails
        {
            StoryId = story.StoryId,
            Slug = story.Slug,
            Month = story.Month,
            Day = story.Day,
            Title = translation.Title.Trim(),
            Html = translation.Text,
        });
    }
}
