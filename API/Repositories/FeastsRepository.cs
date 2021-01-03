using Katameros.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public class FeastsRepository
    {
        private readonly DatabaseContext _context;
        private readonly FeastsFactory _feastsFactory;
        public FeastsRepository(DatabaseContext context, FeastsFactory feastsFactory)
        {
            _context = context;
            _feastsFactory = feastsFactory;
        }

        public void Configure(int languageId = 1)
        {
            _context.LanguageId = languageId;
        }

        private async Task<FeastDate> CreateFeastDate(Enums.Feast feast, DateTime date)
        {
            FeastDate feastDate = new FeastDate() { Id = (int)feast, Date = date };
            feastDate.Name = await _feastsFactory.GetFeastTranslation(feast);
            return feastDate;
        }

        public async Task<IEnumerable<FeastDate>> GetFeastsForYear(int year)
        {
            var feasts = _feastsFactory.ComputeFeastsDate(year);
            var feastsTranslations = await GetFeastsTranslations();

            return feasts.Select(x => new FeastDate()
            {
                Id = (int)x.Item1,
                Date = x.Item2,
                Name = feastsTranslations.Where(t => t.FeastId == (int)x.Item1).FirstOrDefault().Text
            });
        }

        private async Task<IEnumerable<Models.FeastsTranslation>> GetFeastsTranslations()
        {
            return await _context.FeastsTranslations.Where(x => x.LanguageId == _context.LanguageId).ToListAsync();
        }
    }
}
