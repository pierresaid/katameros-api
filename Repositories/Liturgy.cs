using Katameros.DTOs;
using Katameros.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class LectionaryRepository : ILectionaryRepository
    {
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
            // Titus, Philemon, James
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
    }
}
