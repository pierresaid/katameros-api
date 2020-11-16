using Katameros.DTOs;
using Katameros.Enums;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public class LectionaryRepository
    {
        private readonly DatabaseContext _context;
        private readonly ReadingsRepository _readingsRepository;
        private readonly FeastsFactory _feastsRepository;

        public LectionaryRepository(DatabaseContext context, ReadingsRepository readingsRepository, FeastsFactory feastsRepository)
        {
            _context = context;
            _readingsRepository = readingsRepository;
            _feastsRepository = feastsRepository;
        }

        /// <summary>
        /// Configure the Language and/or Bible the repository should use while fetching readings.
        /// <br/>
        /// The repository will configure itself to fetch the corresponding bible if the language alone is set and vice-versa.
        /// </summary>
        /// <param name="LanguageId">Id of the language</param>
        /// <param name="BibleId">Id of the bible</param>
        /// <returns>true if the language and/or the bible are found</returns>
        public async Task<bool> Configure(int languageId = -1, int bibleId = -1)
        {
            if (languageId == -1 && bibleId == -1)
            {
                var bible = await _context.Bibles.FirstOrDefaultAsync();
                if (bible == null)
                    return false;
                _context.BibleId = bible.Id;
                _context.LanguageId = bible.LanguageId;
            }
            else if (languageId == -1)
            {
                var bible = await _context.Bibles.FindAsync(bibleId);
                if (bible == null)
                    return false;
                _context.BibleId = bible.Id;
                _context.LanguageId = bible.LanguageId;
            }
            else if (bibleId == -1)
            {
                if (!await _context.Languages.AnyAsync(e => e.Id == languageId))
                    return false;
                var bible = await _context.Bibles.Where(b => b.LanguageId == languageId).FirstOrDefaultAsync();
                if (bible == null)
                    return false;
                _context.BibleId = bible.Id;
                _context.LanguageId = bible.LanguageId;
            }
            else
            {
                var language = await _context.Languages.FindAsync(languageId);
                var bible = await _context.Bibles.FindAsync(bibleId);
                if (bible == null || language == null)
                    return false;
                _context.BibleId = bibleId;
                _context.LanguageId = languageId;
            }
            return true;
        }

        private async Task<Models.IReadingRefs> GetForPentecost(DateTime date, int easterDaysDiff)
        {
            Models.IReadingRefs readingRefs;

            var dayNumber = date.DayOfWeek;
            var weekNumber = (easterDaysDiff / 7) + 1 - (dayNumber == DayOfWeek.Sunday ? 1 : 0);

            readingRefs = await _context.PentecostReadings.Where(ar => ar.Week == weekNumber && ar.DayOfWeek == (int)dayNumber).FirstAsync();

            return readingRefs;
        }

        private async Task<Models.IReadingRefs> GetForGreatLent(DateTime date, DateTime lentBeginning)
        {
            Models.IReadingRefs readingRefs;
            int weekNumber = ((date - lentBeginning).Days / 7) + 1;
            int dayNumber = (int)date.DayOfWeek;

            readingRefs = await _context.GreatLentReadings.Where(ar => ar.Week == weekNumber && ar.DayOfWeek == dayNumber).FirstAsync();

            return readingRefs;
        }

        private async Task<Models.IReadingRefs> GetForSunday(LocalDate copticDate)
        {
            Models.IReadingRefs readingRefs;
            int i = 0;
            int nbSunday = 0;

            while (i < copticDate.Day)
            {
                if (copticDate.PlusDays(i).DayOfWeek == IsoDayOfWeek.Sunday)
                    nbSunday += 1;
                ++i;
            }

            readingRefs = await _context.SundayReadings.Where(ar => ar.Month_Number == copticDate.Month && ar.Day == nbSunday).FirstOrDefaultAsync();

            return readingRefs;
        }

        private async Task<Models.IReadingRefs> GetForAnnual(LocalDate copticDate)
        {
            return await _context.AnnualReadings.Where(ar => ar.Month_Number == copticDate.Month && ar.Day == copticDate.Day).FirstAsync();
        }

        /// <summary>
        /// Returns the readings for the given date
        /// </summary>
        /// <param name="date">The date</param>
        public async Task<DayReadings> GetForDay(DateTime date)
        {
            DayReadings dayReadings = new DayReadings();
            var sections = new List<Section>();
            Models.IReadingRefs readingRefs;
            CopticDateHelper copticDateHelper = new CopticDateHelper(date);

            var copticDate = copticDateHelper.ToCopticDate();
            var (lentBeginning, lentEnding) = copticDateHelper.GetGreatLentPeriod();
            var easterDate = copticDateHelper.GetEasterDate();
            var easterDaysDiff = (date - easterDate).Days;

            var dayFeast = _feastsRepository.GetDayFeast(date, copticDate, easterDaysDiff);
            if (dayFeast != null)
            {
                if (dayFeast.FeastConstructor != null)
                {
                    return await dayFeast.FeastConstructor();
                }
                dayReadings.Title = await _feastsRepository.GetFeastTranslation(dayFeast.Feast);
            }

            if (lentBeginning.Ticks <= date.Ticks && date.Ticks <= lentEnding.Ticks)
            {
                readingRefs = await GetForGreatLent(date, lentBeginning);
            }
            else if (easterDaysDiff > 0 && easterDaysDiff <= 49)
            {
                readingRefs = await GetForPentecost(date, easterDaysDiff);
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                readingRefs = await GetForSunday(copticDate);
            }
            else
            {
                readingRefs = await GetForAnnual(copticDate);
            }
            if (readingRefs == null)
                return null;

            if (readingRefs.V_Psalm_Ref != null)
                sections.Add(await _readingsRepository.MakeVespers(readingRefs.V_Psalm_Ref, readingRefs.V_Gospel_Ref));
            sections.Add(await _readingsRepository.MakeMatins(readingRefs.M_Psalm_Ref, readingRefs.M_Gospel_Ref, readingRefs.Prophecy));
            sections.Add(await _readingsRepository.MakeLitugy(readingRefs.P_Gospel_Ref, readingRefs.C_Gospel_Ref, readingRefs.X_Gospel_Ref, readingRefs.L_Psalm_Ref, readingRefs.L_Gospel_Ref));

            dayReadings.Sections = sections;

            return dayReadings;
        }
    }
}
