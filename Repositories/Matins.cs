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
    }
}
