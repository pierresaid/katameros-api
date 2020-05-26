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
    }
}
