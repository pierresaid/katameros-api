using Katameros.DTOs;
using Katameros.Factories.SpecialCases;
using Katameros.Repositories;
using NodaTime;

namespace Katameros.Factories;

public class SpecialCaseFactory(ReadingsRepository readingsRepository, ReadingsHelper readingsHelper)
{
    public async Task<DayReadings> HasSpecialCase(DateTime gregorianDate, LocalDate copticDate, int easterDaysDiff)
    {
        var specialCases = new List<ISpecialCase>
        {
            new KiahkSpecialCase(gregorianDate, copticDate, easterDaysDiff, readingsRepository),
            new Theophany(gregorianDate, copticDate, easterDaysDiff, readingsRepository, readingsHelper),
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
