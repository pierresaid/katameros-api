﻿using System.Collections.Generic;

namespace Katameros.DTOs
{
    public class DayReadings
    {
        public string Title { get; set; }
        public string PeriodInfo { get; set; }
        public List<Section> Sections { get; set; }
    }
}
