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
    }
}
