using Katameros.Models;
using System.Collections.Generic;

namespace Katameros.DTOs
{
    public class DayReadings
    {
        public string Title { get; set; }
        public string PeriodInfo { get; set; }
        public Bible Bible { get; set; }
        public IEnumerable<Bible> Bibles { get; set; }
        public List<Section> Sections { get; set; }
    }
}
