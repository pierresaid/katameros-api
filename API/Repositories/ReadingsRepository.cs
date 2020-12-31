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
    public class ReadingsRepository
    {
        private readonly DatabaseContext _context;
        private readonly ReadingsHelper _readingsHelper;

        public ReadingsRepository(DatabaseContext context, ReadingsHelper readingsHelper)
        {
            _context = context;
            _readingsHelper = readingsHelper;
        }

        #region PeriodsReadingsRef
        public async Task<Models.IReadingRefs> GePentecostReadingsRef(DateTime date, int easterDaysDiff)
        {
            Models.IReadingRefs readingRefs;

            var dayNumber = date.DayOfWeek;
            var weekNumber = (easterDaysDiff / 7) + 1 - (dayNumber == DayOfWeek.Sunday ? 1 : 0);

            readingRefs = await _context.PentecostReadings.Where(ar => ar.Week == weekNumber && ar.DayOfWeek == (int)dayNumber).FirstAsync();

            return readingRefs;
        }

        public async Task<Models.IReadingRefs> GetGreatLentReadingsRef(DateTime date, DateTime lentBeginning)
        {
            Models.IReadingRefs readingRefs;
            int weekNumber = ((date - lentBeginning).Days / 7) + 1;
            int dayNumber = (int)date.DayOfWeek;

            readingRefs = await _context.GreatLentReadings.Where(ar => ar.Week == weekNumber && ar.DayOfWeek == dayNumber).FirstAsync();

            return readingRefs;
        }

        public async Task<Models.IReadingRefs> GetSundayReadingsRef(LocalDate copticDate)
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

        public async Task<Models.IReadingRefs> GetAnnualReadingsRef(LocalDate copticDate)
        {
            return await _context.AnnualReadings.Where(ar => ar.Month_Number == copticDate.Month && ar.Day == copticDate.Day).FirstAsync();
        }
        #endregion

        public async Task<DayReadings> GetFromRef(Models.IReadingRefs readingRefs)
        {
            var sections = new List<Section>();
            if (readingRefs.V_Psalm_Ref != null)
                sections.Add(await MakeVespers(readingRefs.V_Psalm_Ref, readingRefs.V_Gospel_Ref));
            sections.Add(await MakeMatins(readingRefs.M_Psalm_Ref, readingRefs.M_Gospel_Ref, readingRefs.Prophecy));
            sections.Add(await MakeLitugy(readingRefs.P_Gospel_Ref, readingRefs.C_Gospel_Ref, readingRefs.X_Gospel_Ref, readingRefs.L_Psalm_Ref, readingRefs.L_Gospel_Ref));
            return new DayReadings
            {
                Sections = sections
            };
        }

        #region Period DayReadings

        public async Task<DayReadings> GetReadingsForAnnual(LocalDate copticDate)
        {
            var refs = await GetAnnualReadingsRef(copticDate);
            return await GetFromRef(refs);
        }

        #endregion

        #region Liturgy
        private async Task<SubSection> MakePauline(string paulineRef)
        {
            var subSection = new SubSection(SubSectionType.Pauline);
            Reading reading = await _readingsHelper.MakeReading(paulineRef, ReadingType.Pauline);
            var firstPassage = reading.Passages.First();
            var recipient = string.Concat(firstPassage.BookTranslation.Where(char.IsLetter));

            if (reading.Introduction != null)
            {
                reading.Introduction = reading.Introduction.Replace("$", recipient);

                reading.Introduction = NumeriseEpistle(reading.Introduction, firstPassage, reading.Introduction.IndexOf("] "));

                var first = reading.Introduction.IndexOf('[');
                var last = reading.Introduction.LastIndexOf(']');

                if (first != -1 && last != -1)
                {
                    Regex regex = new Regex("(?<=\\[).*?(?=\\])");
                    var matches = regex.Matches(reading.Introduction);

                    var singular = matches[0].Value;
                    var plural = matches[1].Value;

                    var input = reading.Introduction;
                    var noun = plural;
                    // Titus, Philemon, James, Timothy
                    if (new[] { 56, 57, 59, 55 }.Contains(firstPassage.BookId))
                    {
                        noun = singular;
                    }
                    var output = input.Substring(0, first) + noun + input.Substring(last + 1, input.Length - 1 - last);
                    reading.Introduction = output;
                }
            }
            subSection.Title = await _readingsHelper.GetReadingMeta(ReadingType.Pauline, ReadingsMetadata.Title);
            subSection.Readings = new List<Reading>() { reading };
            return subSection;
        }

        private async Task<SubSection> MakeCatholic(string catholicRef)
        {
            var subSection = new SubSection(SubSectionType.Catholic);
            Reading reading = await _readingsHelper.MakeReading(catholicRef, ReadingType.Catholic);
            var firstPassage = reading.Passages.First();
            var author = string.Concat(firstPassage.BookTranslation.Where(char.IsLetter));
            reading.Introduction = reading.Introduction?.Replace("$", author);
            reading.Introduction = NumeriseEpistle(reading.Introduction, firstPassage);

            subSection.Readings = new List<Reading>() { reading };
            subSection.Title = await _readingsHelper.GetReadingMeta(ReadingType.Catholic, ReadingsMetadata.Title);
            return subSection;
        }

        private string NumeriseEpistle(string Introduction, Passage firstPassage, int lastIndex = -1)
        {
            var first = Introduction.IndexOf('[');
            var last = lastIndex != -1 ? lastIndex : Introduction.LastIndexOf(']');
            if (first == -1 || last == -1)
            {
                return Introduction;
            }

            Regex regex = new Regex("(?<=\\[).*?(?=\\])");
            var matches = regex.Matches(Introduction);

            int matchesCount = matches.Count;

            var firstEpistle = matchesCount >= 1 ? matches[0]?.Value : null;
            var secondEpistle = matchesCount >= 2 ? matches[1]?.Value : null;
            var thirdEpistle = matchesCount >= 3 ? matches[2]?.Value : null;

            var input = Introduction;
            var epistleNumber = "";
            // First Corinthians, First Thessalonians, First Timothy, First Peter, First John
            if (new[] { 46, 52, 54, 60, 62 }.Contains(firstPassage.BookId))
            {
                epistleNumber = firstEpistle;
            }
            else if (new[] { 47, 53, 55, 61, 63 }.Contains(firstPassage.BookId)) // Second Corinthians, Second Thessalonians, Second Timothy, Second Peter, Second John
            {
                epistleNumber = secondEpistle;
            }
            else if (firstPassage.BookId == 64) // Third John
            {
                epistleNumber = thirdEpistle;
            }
            var output = input.Substring(0, first) + epistleNumber + input.Substring(last + 1, input.Length - 1 - last);
            return output;
        }

        private async Task<SubSection> MakeActs(string actsRef)
        {
            return new SubSection(SubSectionType.Acts)
            {
                Title = await _readingsHelper.GetReadingMeta(ReadingType.Acts, ReadingsMetadata.Title),
                Readings = new List<Reading>() { await _readingsHelper.MakeReading(actsRef, ReadingType.Acts) }
            };
        }

        public async Task<Section> MakeLitugy(string paulineRef, string catholicRef, string actsRef, string psalmRef, string gospelRef)
        {
            var section = new Section(SectionType.Liturgy);
            var subSections = new List<SubSection>();

            section.Title = await _readingsHelper.GetSectionMeta(SectionType.Liturgy, SectionsMetadata.Title);

            subSections.Add(await MakePauline(paulineRef));
            subSections.Add(await MakeCatholic(catholicRef));
            subSections.Add(await MakeActs(actsRef));
            if (gospelRef != null)
                subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));

            section.SubSections = subSections;

            return section;
        }
        #endregion

        #region Psalm And Gospel
        public async Task<SubSection> MakePsalmAndGospel(string psalmRef, string gospelRef)
        {
            SubSection subSection = new SubSection(SubSectionType.PsalmAndGospel);
            List<Reading> readings = new List<Reading>();

            subSection.Introduction = await _readingsHelper.GetSubSectionMeta(SubSectionType.PsalmAndGospel, SubSectionsMetadata.Introduction);
            if (psalmRef != null)
                readings.Add(await _readingsHelper.MakeReading(psalmRef, ReadingType.Psalm));
            Reading gospel = await _readingsHelper.MakeReading(gospelRef, ReadingType.Gospel);
            var evangelist = string.Concat(gospel.Passages.First().BookTranslation.Where(char.IsLetter));
            if (psalmRef == null)
                gospel.Introduction = null;
            else if (gospel.Introduction.Contains("$"))
            {
                gospel.Introduction = gospel.Introduction.Replace("$", evangelist);
            }
            readings.Add(gospel);
            subSection.Title = await _readingsHelper.GetSubSectionMeta(SubSectionType.PsalmAndGospel, SubSectionsMetadata.Title);
            subSection.Introduction = subSection.Introduction?.Replace("$", evangelist);
            subSection.Readings = readings;
            return subSection;
        }
        #endregion

        #region Matins
        public async Task<Section> MakeMatins(string psalmRef, string gospelRef, string prophecyRef = null)
        {
            var section = new Section(SectionType.Matins);
            var subSections = new List<SubSection>();

            section.Title = (await _context.SectionsMetadatasTranslations.FindAsync((int)SectionType.Matins, (int)SectionsMetadata.Title, _context.LanguageId))?.Text;

            if (prophecyRef != null)
                subSections.Add(await MakeProphecies(prophecyRef));
            subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));

            section.SubSections = subSections;

            return section;
        }
        #endregion

        #region Prophecies
        public async Task<SubSection> MakeProphecies(string prophecyRef)
        {
            var subSection = new SubSection(SubSectionType.Prophecy);
            var readings = new List<Reading>();

            subSection.Title = (await _context.SubSectionsMetadatasTranslations.FindAsync((int)SubSectionType.Prophecy, (int)SubSectionsMetadata.Title, _context.LanguageId))?.Text;
            var prophecyConclusion = (await _context.ReadingsMetadatasTranslations.FindAsync((int)ReadingType.Prophecy, (int)ReadingsMetadata.Conclusion, _context.LanguageId))?.Text;
            var refs = _readingsHelper.GetRefs(prophecyRef);

            foreach (var readingRef in refs)
            {
                Reading reading = await _readingsHelper.MakeReading(readingRef, ReadingType.Prophecy);
                reading.Conclusion = prophecyConclusion;
                readings.Add(reading);
            }

            subSection.Readings = readings;

            return subSection;
        }
        #endregion

        #region OldTestament
        public async Task<SubSection> MakeOldTestament(string passageRef)
        {
            var subSection = new SubSection(SubSectionType.Prophecy);
            var readings = new List<Reading>();

            Reading reading = await _readingsHelper.MakeReading(passageRef, ReadingType.Prophecy);
            reading.Conclusion = await _readingsHelper.GetReadingMeta(ReadingType.Prophecy, ReadingsMetadata.Conclusion);
            readings.Add(reading);

            subSection.Readings = readings;
            return subSection;
        }
        #endregion

        #region Vespers
        public async Task<Section> MakeVespers(string psalmRef, string gospelRef, string prophecyRef = null)
        {
            var section = new Section(SectionType.Vespers);
            var subSections = new List<SubSection>();

            section.Title = (await _context.SectionsMetadatasTranslations.FindAsync((int)SectionType.Vespers, (int)SectionsMetadata.Title, _context.LanguageId))?.Text;

            if (prophecyRef != null)
                subSections.Add(await MakeProphecies(prophecyRef));

            subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));
            section.SubSections = subSections;

            return section;
        }
        #endregion
    }
}
