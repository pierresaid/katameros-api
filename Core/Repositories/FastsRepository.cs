using Katameros.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Katameros.Repositories;

public class FastsRepository(DatabaseContext _context, FastsFactory _fastsFactory)
{

    public void Configure(int languageId = 1)
    {
        _context.LanguageId = languageId;
    }

    public async Task<IEnumerable<FastPeriod>> GetFastsForYear(int year)
    {
        var fasts = _fastsFactory.ComputeFastPeriods(year);
        var fastsTranslations = await GetFastsTranslations();

        return fasts.Select(x => new FastPeriod()
        {
            Id = (int)x.Fast,
            Start = x.Start,
            End = x.End,
            Name = fastsTranslations.Where(t => t.FastId == (int)x.Fast).FirstOrDefault()?.Text
        });
    }

    private async Task<IEnumerable<Models.FastsTranslation>> GetFastsTranslations()
    {
        return await _context.FastsTranslations.Where(x => x.LanguageId == _context.LanguageId).ToListAsync();
    }
}
