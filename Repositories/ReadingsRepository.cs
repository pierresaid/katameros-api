using Katameros.DTOs;
using Katameros.Enums;
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

        #region Liturgy
        private async Task<SubSection> MakePauline(string paulineRef)
        {
            var subSection = new SubSection();
            Reading reading = await _readingsHelper.MakeReading(paulineRef, ReadingType.Pauline);
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
            // Titus, Philemon, James
            if (new[] { 56, 57, 59 }.Contains(firstPassage.BookId))
            {
                noun = singular;
            }
            var output = input.Substring(0, first) + noun + input.Substring(last + 1, input.Length - 1 - last);
            reading.Introduction = output;
            subSection.Title = (await _context.ReadingsMetadatasTranslations.FindAsync((int)ReadingType.Pauline, (int)ReadingsMetadata.Title, _context.LanguageId)).Text;
            subSection.Readings = new List<Reading>() { reading };
            return subSection;
        }

        private async Task<SubSection> MakeCatholic(string catholicRef)
        {
            var subSection = new SubSection();
            Reading reading = await _readingsHelper.MakeReading(catholicRef, ReadingType.Catholic);
            var author = string.Concat(reading.Passages.First().BookTranslation.Where(char.IsLetter));
            reading.Introduction = reading.Introduction.Replace("$", author);
            subSection.Readings = new List<Reading>() { reading };
            subSection.Title = (await _context.ReadingsMetadatasTranslations.FindAsync((int)ReadingType.Catholic, (int)ReadingsMetadata.Title, _context.LanguageId)).Text;
            return subSection;
        }

        private async Task<SubSection> MakeActs(string actsRef)
        {
            var title = (await _context.ReadingsMetadatasTranslations.FindAsync((int)ReadingType.Acts, (int)ReadingsMetadata.Title, _context.LanguageId)).Text;
            return new SubSection
            {
                Title = title,
                Readings = new List<Reading>() { await _readingsHelper.MakeReading(actsRef, ReadingType.Acts) }
            };
        }

        public async Task<Section> MakeLitugy(string paulineRef, string catholicRef, string actsRef, string psalmRef, string gospelRef)
        {
            var section = new Section();
            var subSections = new List<SubSection>();

            section.Title = (await _context.SectionsMetadatasTranslations.FindAsync((int)SectionType.Liturgy, (int)SectionsMetadata.Title, _context.LanguageId)).Text;

            subSections.Add(await MakePauline(paulineRef));
            subSections.Add(await MakeCatholic(catholicRef));
            subSections.Add(await MakeActs(actsRef));
            if (gospelRef != null)
                subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));

            section.subSections = subSections;

            return section;
        }
        #endregion

        #region Psalm And Gospel
        public async Task<SubSection> MakePsalmAndGospel(string psalmRef, string gospelRef)
        {
            SubSection subSection = new SubSection();
            List<Reading> readings = new List<Reading>();

            subSection.Introduction = (await _context.SubSectionsMetadatasTranslations.FindAsync((int)SubSectionType.PsalmAndGospel, (int)SubSectionsMetadata.Introduction, _context.LanguageId)).Text;
            if (psalmRef != null)
                readings.Add(await _readingsHelper.MakeReading(psalmRef, ReadingType.Psalm));
            Reading gospel = await _readingsHelper.MakeReading(gospelRef, ReadingType.Gospel);
            if (psalmRef == null)
                gospel.Introduction = null;
            readings.Add(gospel);
            var evangelist = string.Concat(gospel.Passages.First().BookTranslation.Where(char.IsLetter));
            subSection.Title = (await _context.SubSectionsMetadatasTranslations.FindAsync((int)SubSectionType.PsalmAndGospel, (int)SubSectionsMetadata.Title, _context.LanguageId)).Text;
            subSection.Introduction = subSection.Introduction.Replace("$", evangelist);
            subSection.Readings = readings;
            return subSection;
        }
        #endregion

        #region Matins
        public async Task<Section> MakeMatins(string psalmRef, string gospelRef, string prophecyRef)
        {
            var section = new Section();
            var subSections = new List<SubSection>();

            section.Title = (await _context.SectionsMetadatasTranslations.FindAsync((int)SectionType.Matins, (int)SectionsMetadata.Title, _context.LanguageId)).Text;

            if (prophecyRef != null)
                subSections.Add(await MakeProphecies(prophecyRef));
            subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));

            section.subSections = subSections;

            return section;
        }
        #endregion

        #region Prohpecies
        private async Task<SubSection> MakeProphecies(string prophecyRef)
        {
            var subSection = new SubSection();
            var readings = new List<Reading>();

            subSection.Title = (await _context.SubSectionsMetadatasTranslations.FindAsync((int)SubSectionType.Prophecy, (int)SubSectionsMetadata.Title, _context.LanguageId)).Text;
            var prophecyConclusion = (await _context.ReadingsMetadatasTranslations.FindAsync((int)ReadingType.Prophecy, (int)ReadingsMetadata.Conclusion, _context.LanguageId)).Text;
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

        #region Vespers
        public async Task<Section> MakeVespers(string psalmRef, string gospelRef)
        {
            var section = new Section();
            var subSections = new List<SubSection>();

            section.Title = (await _context.SectionsMetadatasTranslations.FindAsync((int)SectionType.Vespers, (int)SectionsMetadata.Title, _context.LanguageId)).Text;
            subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));
            section.subSections = subSections;

            return section;
        }
        #endregion
    }
}
