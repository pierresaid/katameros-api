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
        private async Task<Section> MakeVespers(string psalmRef, string gospelRef)
        {
            var section = new Section();
            var subSections = new List<SubSection>();

            section.Title = (await _context.SectionsMetadatasTranslations.FindAsync((int)SectionType.Vespers, (int)SectionsMetadata.Title, LanguageId)).Text;
            subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));
            section.subSections = subSections;

            return section;
        }
    }
}
