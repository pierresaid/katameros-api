using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructParamounTheophany()
    {
        DayReadings dayReadings = new DayReadings
        {
            Title = await GetFeastTranslation(Feast.TheophanyParamoun),
            Sections = new List<Section>
            {
                await _readingsRepository.MakeVespers("19.42:2,5", "40.4:12-22"),
                await _readingsRepository.MakeMatins("19.42:7,8", "43.3:22-29"),
                await _readingsRepository.MakeLitugy("46.1:1-17", "61.1:12-19", "44.16:25-34", "19.45:2", "42.3:1-18")
            }
        };
        return dayReadings;
    }
}
