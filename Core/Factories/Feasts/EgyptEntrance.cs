using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructEgyptEntrance()
    {
        DayReadings dayReadings = new DayReadings
        {
            Title = await GetFeastTranslation(Feast.EgyptEntrance),
            Sections = new List<Section>
            {
                await _readingsRepository.MakeVespers("19.105:23,27", "40.4:12-17"),
                await _readingsRepository.MakeMatins("19.106:21,22,4", "40.12:15-23", null),
                await _readingsRepository.MakeLitugy("49.2:1-22", "62.4:7-19", "44.7:20-34", "19.105:36,38", "40.2:13-23")
            }
        };
        return dayReadings;
    }
}
