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

    private async Task<DayReadings> ConstructFeastOfTheCross2()
    {
        DayReadings dayReadings = new DayReadings
        {
            Title = await GetFeastTranslation(Feast.Cross),
            Sections = new List<Section>
            {
                await _readingsRepository.MakeVespers("19.99:9,5", "43.4:19-24"),
                await _readingsRepository.MakeMatins("19.118:28,16", "43.3:14-21", null),
                await _readingsRepository.MakeLitugy("48.6:11-18", "60.1:3-12", "44.4:8-18", "19.145:1-2", "43.6:35-46")
            }
        };
        return dayReadings;
    }

    private async Task<DayReadings> ConstructFeastOfTheCross3()
    {
        DayReadings dayReadings = new DayReadings
        {
            Title = await GetFeastTranslation(Feast.Cross),
            Sections = new List<Section>
            {
                await _readingsRepository.MakeVespers("19.45:6,17", "40.16:21-26"),
                await _readingsRepository.MakeMatins("19.74:2,12", "41.8:34-38*@+41.9:1", null),
                await _readingsRepository.MakeLitugy("51.2:6-19", "62.5:13-21", "44.3:12-21", "19.61:2,3,5", "42.14:25-35")
            }
        };
        return dayReadings;
    }
}
