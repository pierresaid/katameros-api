using System.Collections.Generic;
using Katameros.Models;

namespace Katameros.DTOs
{
    public class Passage
    {
        public int BookId { get; set; }
        public int Chapter { get; set; }
        public string Ref { get; set; }
        public IEnumerable<Verse> Verses { get; set; }
    }
}
