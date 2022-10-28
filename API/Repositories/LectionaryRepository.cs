using Helpers.Katameros;
using Humanizer;
using Katameros.DTOs;
using Katameros.Enums;
using Katameros.Factories;
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
        private readonly FeastsFactory _feastsFactory;
        private readonly SpecialCaseFactory _specialCaseFactory;

        public LectionaryRepository(DatabaseContext context, ReadingsRepository readingsRepository, FeastsFactory feastsFactory, SpecialCaseFactory specialCaseFactory)
        {
            _context = context;
            _readingsRepository = readingsRepository;
            _feastsFactory = feastsFactory;
            _specialCaseFactory = specialCaseFactory;
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

        /// <summary>
        /// Returns the readings for the given date
        /// </summary>
        /// <param name="gregorianDate">The date</param>
        public async Task<DayReadings> GetForDay(DateTime gregorianDate)
        {
            DayReadings dayReadings;
            Models.IReadingRefs readingRefs;
            CopticDateHelper copticDateHelper = new CopticDateHelper(gregorianDate);

            var copticDate = copticDateHelper.ToCopticDate();
            var (lentBeginning, lentEnding) = copticDateHelper.GetGreatLentPeriod();
            var easterDate = copticDateHelper.GetEasterDate();
            var easterDaysDiff = (gregorianDate - easterDate).Days;
            int nbSundays = -1;
            _context.CopticDate = copticDate;

            var specialCaseReadings = await _specialCaseFactory.HasSpecialCase(gregorianDate, copticDate, easterDaysDiff);
            if (specialCaseReadings != null)
            {
                await AddBibleInfo(specialCaseReadings);
                return specialCaseReadings;
            }

            var dayFeast = _feastsFactory.GetDayFeast(gregorianDate, copticDate, easterDaysDiff);
            if (dayFeast != null)
            {
                if (dayFeast.FeastConstructor != null)
                {
                    dayReadings = await dayFeast.FeastConstructor();
                    await AddBibleInfo(dayReadings);
                    return dayReadings;
                }
            }

            if (lentBeginning.Ticks <= gregorianDate.Ticks && gregorianDate.Ticks <= lentEnding.Ticks)
            {
                readingRefs = await _readingsRepository.GetGreatLentReadingsRef(gregorianDate, lentBeginning);
            }
            else if (easterDaysDiff > 0 && easterDaysDiff <= 49)
            {
                readingRefs = await _readingsRepository.GePentecostReadingsRef(gregorianDate, easterDaysDiff);
            }
            else if (copticDate.Day == 29 && gregorianDate.DayOfWeek == DayOfWeek.Sunday && copticDate.Month != CopticMonths.Amshir && copticDate.Month != CopticMonths.Toubah)
            {
                dayReadings = await _feastsFactory.Construct29OfMonth();
                await AddBibleInfo(dayReadings);
                return dayReadings;
            }
            else if (gregorianDate.DayOfWeek == DayOfWeek.Sunday)
            {
                (readingRefs, nbSundays) = await _readingsRepository.GetSundayReadingsRef(copticDate);
            }
            else
            {
                readingRefs = await _readingsRepository.GetAnnualReadingsRef(copticDate);
            }
            if (readingRefs == null)
                return null;

            dayReadings = await _readingsRepository.GetFromRef(readingRefs);
            if (dayFeast != null)
                dayReadings.Title = await _feastsFactory.GetFeastTranslation(dayFeast.Feast);
            if (nbSundays != -1)
                dayReadings.PeriodInfo = $"{ getOrdinalizeWithLanguage(nbSundays) } { getSundayTranslation() }";

            await AddBibleInfo(dayReadings);
            dayReadings.CopticDate = $"{copticDate.Day}/{copticDate.Month}/{copticDate.Year}";
            return dayReadings;
        }

        private async Task AddBibleInfo(DayReadings dayReadings)
        {
            dayReadings.Bibles = await _context.Bibles.Where(x => x.LanguageId == this._context.LanguageId).ToListAsync();
            dayReadings.Bible = dayReadings.Bibles.FirstOrDefault(x => x.Id == _context.BibleId);
        }

        private  string getOrdinalizeWithLanguage(int nbSundays)
        {
            string langStr = _context.LanguageId switch
            {
                1 => "fr",
                2 => "en",
                3 => "ar",
                4 => "it",
                _ => "",
            };
            return nbSundays.Ordinalize(culture: System.Globalization.CultureInfo.CreateSpecificCulture(langStr));
        }

        private string getSundayTranslation()
        {
            switch (_context.LanguageId)
            {
                case 1:
                    return "Dimanche";
                case 2:
                    return "Sunday";
                case 3:
                    return "الأحد";
                case 4:
                    return "Domenica";
                default:
                    return "";
            }
        }
    }
}
