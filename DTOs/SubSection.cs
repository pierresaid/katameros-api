using System.Collections.Generic;

namespace Katameros.DTOs
{
    public class SubSection
    {
        public string Title { get; set; }
        public string Introduction { get; set; }
        public IEnumerable<Reading> Readings { get; set; }
    }
}
