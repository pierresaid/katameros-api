using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructFeastOfTheCross()
    {
        DayReadings dayReadings = new DayReadings
        {
            Title = await GetFeastTranslation(Feast.Cross),
            Sections = new List<Section>
            {
                await _readingsRepository.MakeVespers("19.4:6-8", "43.8:28-42"),
                await _readingsRepository.MakeMatins("19.60:4-5", "43.12:26-36", null),
                await _readingsRepository.MakeLitugy("46.1:17-31", "60.2:11-25", "44.10:34-43", "19.65:1-2", "43.10:22-38")
            }
        };
        return dayReadings;
    }
}
