using Katameros.DTOs;
using Katameros.Factories.SpecialCases;
using Katameros.Repositories;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Factories
{
    public class SpecialCaseFactory
    {
        private readonly DatabaseContext _context;
        private readonly ReadingsRepository _readingsRepository;
        private readonly ReadingsHelper _readingsHelper;

        public SpecialCaseFactory(DatabaseContext context, ReadingsRepository readingsRepository, ReadingsHelper readingsHelper)
        {
            _context = context;
            _readingsRepository = readingsRepository;
            _readingsHelper = readingsHelper;
        }
        public async Task<DayReadings> HasSpecialCase(DateTime gregorianDate, LocalDate copticDate, int easterDaysDiff)
        {
            var specialCases = new List<ISpecialCase>
            {
                new KiahkSpecialCase(gregorianDate, copticDate, easterDaysDiff, _readingsRepository),
            };

            foreach (var specialCase in specialCases)
            {
                var readings = await specialCase.Process();
                if (readings != null)
                {
                    return readings;
                }
            }

            return null;
        }
    }
}
