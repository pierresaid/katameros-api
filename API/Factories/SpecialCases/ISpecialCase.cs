using Katameros.DTOs;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Katameros.Factories.SpecialCases
{
    public interface ISpecialCase
    {
        /// <summary>
        /// Handle the special case and returns the readings of it applies
        /// </summary>
        public Task<DayReadings> Process();
    }
}
