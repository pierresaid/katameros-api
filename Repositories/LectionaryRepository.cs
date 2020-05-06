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
    public class LectionaryRepository : ILectionaryRepository
    {
        private int BibleId { get; set; }
        private int LanguageId { get; set; }
        private readonly DatabaseContext _context;
        public LectionaryRepository(DatabaseContext context) => _context = context;

        public async Task<bool> Configure(int languageId = -1, int bibleId = -1)
        {
            if (languageId == -1 && bibleId == -1)
            {
                var bible = await _context.Bibles.FirstOrDefaultAsync();
                if (bible == null)
                    return false;
                BibleId = bible.Id;
                LanguageId = bible.LanguageId;
            }
            else if (languageId == -1)
            {
                var bible = await _context.Bibles.FindAsync(bibleId);
                if (bible == null)
                    return false;
                BibleId = bible.Id;
                LanguageId = bible.LanguageId;
            }
            else if (bibleId == -1)
            {
                if (!await _context.Languages.AnyAsync(e => e.Id == languageId))
                    return false;
                var bible =  await _context.Bibles.Where(b => b.LanguageId == languageId).FirstOrDefaultAsync();
                if (bible == null)
                    return false;
                BibleId = bible.Id;
                LanguageId = bible.LanguageId;
            }
            else
            {
                var language = await _context.Languages.FindAsync(languageId);
                var bible = await _context.Bibles.FindAsync(bibleId);
                if (bible == null || language == null)
                    return false;
                BibleId = bibleId;
                LanguageId = languageId;
            }
            return true;
        }

        private async Task<Passage> MakePassage(string passageRef)
        {
            Passage passage = new Passage();

            var splittedPassageRef = passageRef.Split('.', ':');
            passage.BookId = int.Parse(splittedPassageRef[0]);
            passage.Chapter = int.Parse(splittedPassageRef[1]);

            var query = _context.Verses.Where(v => v.BibleId == this.BibleId && v.BookId == passage.BookId && v.Chapter == passage.Chapter);
            string versesRef = splittedPassageRef[2];
            if (versesRef.Contains('-'))
            {
                var splittedVerses = versesRef.Split('-');
                int from = int.Parse(string.Concat(splittedVerses[0]));
                int to = int.Parse(string.Concat(splittedVerses[1]));
                query = query.Where(v => v.Number >= from && v.Number <= to);
            }
            else if (versesRef.Contains(','))
            {
                var splittedVerses = versesRef.Split(',').Select(s => int.Parse(s)).ToArray();
                query = query.Where(v => splittedVerses.Contains(v.Number));
            }
            else
            {
                query = query.Where(v => v.Number == int.Parse(string.Concat(versesRef)));
            }
            var bookTranslation = (await _context.BooksTranslations.FindAsync(passage.BookId, LanguageId)).Text;
            passage.Ref = $"{passage.Chapter}:{versesRef}";
            passage.BookTranslation = bookTranslation;
            passage.Verses = query;
            return passage;
        }

        private async Task<Reading> MakeReading(string passagesRef, ReadingType readingType)
        {
            Reading reading = new Reading();
            List<Passage> passages = new List<Passage>();
            string[] passageRefs = GetRefs(passagesRef);

            foreach (var passageRef in passageRefs)
            {
                passages.Add(await MakePassage(passageRef));
            }

            reading.Passages = passages;
            reading.Introduction = (await _context.ReadingsMetadatasTranslations.FindAsync((int)readingType, (int)ReadingsMetadata.Introduction, LanguageId))?.Text;
            reading.Conclusion = (await _context.ReadingsMetadatasTranslations.FindAsync((int)readingType, (int)ReadingsMetadata.Conclusion, LanguageId))?.Text;

            return reading;
        }

        private string[] GetRefs(string refs)
        {
            return refs.Split(new string[] { "*@+", "@" }, StringSplitOptions.None);
        }

        private async Task<SubSection> MakePsalmAndGospel(string psalmRef, string gospelRef)
        {
            SubSection subSection = new SubSection();
            List<Reading> readings = new List<Reading>();

            subSection.Introduction = (await _context.SubSectionsMetadatasTranslations.FindAsync((int)SubSectionType.PsalmAndGospel, (int)SubSectionsMetadata.Introduction, LanguageId)).Text;
            if (psalmRef != null)
                readings.Add(await MakeReading(psalmRef, ReadingType.Psalm));
            Reading gospel = await MakeReading(gospelRef, ReadingType.Gospel);
            readings.Add(gospel);
            var evangelist = string.Concat(gospel.Passages.First().BookTranslation.Where(char.IsLetter));
            subSection.Title = (await _context.SubSectionsMetadatasTranslations.FindAsync((int)SubSectionType.PsalmAndGospel, (int)SubSectionsMetadata.Title, LanguageId)).Text;
            subSection.Introduction = subSection.Introduction.Replace("$", evangelist);
            subSection.Readings = readings;
            return subSection;
        }

        private async Task<SubSection> MakePauline(string paulineRef)
        {
            var subSection = new SubSection();
            Reading reading = await MakeReading(paulineRef, ReadingType.Pauline);
            var firstPassage = reading.Passages.First();
            var recipient = string.Concat(firstPassage.BookTranslation.Where(char.IsLetter));
            reading.Introduction = reading.Introduction.Replace("$", recipient);

            var first = reading.Introduction.IndexOf('[');
            var last = reading.Introduction.LastIndexOf(']');

            Regex regex = new Regex("(?<=\\[).*?(?=\\])");
            var matches = regex.Matches(reading.Introduction);

            var singular = matches[0].Value;
            var plural = matches[1].Value;

            var input = reading.Introduction;
            var noun = plural;
            if (new[] { 56, 57, 59 }.Contains(firstPassage.BookId))
            {
                noun = singular;
            }
            var output = input.Substring(0, first) + noun + input.Substring(last + 1, input.Length - 1 - last);
            reading.Introduction = output;
            subSection.Title = (await _context.ReadingsMetadatasTranslations.FindAsync((int)ReadingType.Pauline, (int)ReadingsMetadata.Title, LanguageId)).Text;
            subSection.Readings = new List<Reading>() { reading };
            return subSection;
        }

        private async Task<SubSection> MakeCatholic(string catholicRef)
        {
            var subSection = new SubSection();
            Reading reading = await MakeReading(catholicRef, ReadingType.Catholic);
            var author = string.Concat(reading.Passages.First().BookTranslation.Where(char.IsLetter));
            reading.Introduction = reading.Introduction.Replace("$", author);
            subSection.Readings = new List<Reading>() { reading };
            subSection.Title = (await _context.ReadingsMetadatasTranslations.FindAsync((int)ReadingType.Catholic, (int)ReadingsMetadata.Title, LanguageId)).Text;
            return subSection;
        }

        private async Task<SubSection> MakeActs(string actsRef)
        {
            var title = (await _context.ReadingsMetadatasTranslations.FindAsync((int)ReadingType.Acts, (int)ReadingsMetadata.Title, LanguageId)).Text;
            return new SubSection
            {
                Title = title,
                Readings = new List<Reading>() { await MakeReading(actsRef, ReadingType.Acts) }
            };
        }

        private async Task<Section> MakeLitugy(string paulineRef, string catholicRef, string actsRef, string psalmRef, string gospelRef)
        {
            var section = new Section();
            var subSections = new List<SubSection>();

            section.Title = (await _context.SectionsMetadatasTranslations.FindAsync((int)SectionType.Liturgy, (int)SectionsMetadata.Title, LanguageId)).Text;

            subSections.Add(await MakePauline(paulineRef));
            subSections.Add(await MakeCatholic(catholicRef));
            subSections.Add(await MakeActs(actsRef));
            subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));

            section.subSections = subSections;

            return section;
        }

        private async Task<Section> MakeVespers(string psalmRef, string gospelRef)
        {
            var section = new Section();
            var subSections = new List<SubSection>();

            section.Title = (await _context.SectionsMetadatasTranslations.FindAsync((int)SectionType.Vespers, (int)SectionsMetadata.Title, LanguageId)).Text;
            subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));
            section.subSections = subSections;

            return section;
        }

        private async Task<SubSection> MakeProphecies(string prophecyRef)
        {
            var subSection = new SubSection();
            var readings = new List<Reading>();

            subSection.Title = (await _context.SubSectionsMetadatasTranslations.FindAsync((int)SubSectionType.Prophecy, (int)SubSectionsMetadata.Title, LanguageId)).Text;
            var prophecyConclusion = (await _context.ReadingsMetadatasTranslations.FindAsync((int)ReadingType.Prophecy, (int)ReadingsMetadata.Conclusion, LanguageId)).Text;
            var refs = GetRefs(prophecyRef);

            foreach (var readingRef in refs)
            {
                Reading reading = await MakeReading(readingRef, ReadingType.Prophecy);
                reading.Conclusion = prophecyConclusion;
                readings.Add(reading);
            }

            subSection.Readings = readings;

            return subSection;
        }

        private async Task<Section> MakeMatins(string psalmRef, string gospelRef, string prophecyRef)
        {
            var section = new Section();
            var subSections = new List<SubSection>();

            section.Title = (await _context.SectionsMetadatasTranslations.FindAsync((int)SectionType.Matins, (int)SectionsMetadata.Title, LanguageId)).Text;

            if (prophecyRef != null)
                subSections.Add(await MakeProphecies(prophecyRef));
            subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));

            section.subSections = subSections;

            return section;
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
            var nbSunday = copticDate.Day / 7;

            if (nbSunday == 0)
                nbSunday = 1;

            readingRefs = await _context.SundayReadings.Where(ar => ar.Month_Number == copticDate.Month && ar.Day == nbSunday).FirstOrDefaultAsync();

            return readingRefs;
        }

        private async Task<Models.IReadingRefs> GetForAnnual(LocalDate copticDate)
        {
            return await _context.AnnualReadings.Where(ar => ar.Month_Number == copticDate.Month && ar.Day == copticDate.Day).FirstAsync();
        }

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
                sections.Add(await MakeVespers(readingRefs.V_Psalm_Ref, readingRefs.V_Gospel_Ref));
            sections.Add(await MakeMatins(readingRefs.M_Psalm_Ref, readingRefs.M_Gospel_Ref, readingRefs.Prophecy));
            sections.Add(await MakeLitugy(readingRefs.P_Gospel_Ref, readingRefs.C_Gospel_Ref, readingRefs.X_Gospel_Ref, readingRefs.L_Psalm_Ref, readingRefs.L_Gospel_Ref));

            dayReadings.Sections = sections;

            return dayReadings;
        }
    }
}
