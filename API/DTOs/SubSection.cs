using Katameros.Enums;
using System.Collections.Generic;

namespace Katameros.DTOs
{
    public class SubSection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public List<Reading> Readings { get; set; }
        public SubSection(SubSectionType subSectionType)
        {
            Id = (int)subSectionType;
        }
    }
}
