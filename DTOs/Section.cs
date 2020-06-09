using System.Collections.Generic;

namespace Katameros.DTOs
{
    public class Section
    {
        public string Title { get; set; }
        public List<SubSection> subSections { get; set; }
    }
}
