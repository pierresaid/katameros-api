using System.Collections.Generic;

namespace Katameros.DTOs
{
    public class Section
    {
        public string Title { get; set; }
        public IEnumerable<SubSection> subSections { get; set; }
    }
}
