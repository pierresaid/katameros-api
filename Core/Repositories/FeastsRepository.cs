using Katameros.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Katameros.Repositories;

public class FeastsRepository(DatabaseContext _context, FeastsFactory _feastsFactory)
{

    public async Task<bool> Configure(int languageId = 1)
    {
        if (!await _context.Languages.AnyAsync(l => l.Id == languageId))
            return false;
        _context.LanguageId = languageId;
        return true;
    }

    public async Task<IEnumerable<FeastDate>> GetFeastsForYear(int year)
    {
        var feasts = _feastsFactory.ComputeFeastsDate(year);
        var feastsTranslations = await GetFeastsTranslations();

        return feasts.Select(x =>
        {
            var translation = feastsTranslations.Where(t => t.FeastId == (int)x.Item1).FirstOrDefault();
            return new FeastDate()
            {
                Id = (int)x.Item1,
                Date = x.Item2,
                Name = translation?.Text,
                Description = translation?.Description
            };
        });
    }

    private async Task<IEnumerable<Models.FeastsTranslation>> GetFeastsTranslations()
    {
        return await _context.FeastsTranslations.Where(x => x.LanguageId == _context.LanguageId).ToListAsync();
    }
}
