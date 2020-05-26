using System.Collections.Generic;

namespace Katameros.DTOs
{
    public class DayReadings
    {
        public string Title { get; set; }
        public IEnumerable<Section> Sections { get; set; }
    }
}
