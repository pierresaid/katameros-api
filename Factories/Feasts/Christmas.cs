using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructChristmas()
    {
        DayReadings dayReadings = new DayReadings
        {
            Title = await GetFeastTranslation(Feast.Christmas),
            Sections = new List<Section>
            {
                await _readingsRepository.MakeVespers("19.72:10", "42.3:23-38"),
                await _readingsRepository.MakeMatins("19.72:15", "43.1:14-17", null),
                await _readingsRepository.MakeLitugy("58.1:1-14*@+58.2:1-4", "61.1:12-17", "44.13:26-33", "19.2:7-8", "40.2:1-12")
            }
        };
        return dayReadings;
    }
}
