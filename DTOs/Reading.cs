using System.Collections.Generic;

namespace Katameros.DTOs
{
    public class Reading
    {
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string Conclusion { get; set; }
        public List<Passage> Passages { get; set; }
    }
}