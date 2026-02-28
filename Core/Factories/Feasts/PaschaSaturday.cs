using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructPaschaSaturday()
    {
        DayReadings dayReadings = new DayReadings
        {
            Title = await GetFeastTranslation(Feast.PaschaSaturday),
            Sections = new List<Section>
            {
                await _readingsRepository.MakeLitugy("46.15:1-22", "60.1:1-9", "44.3:12-21", "19.3:3,5*@+19.81:7", "40.28:1-20")
            }
        };
        return dayReadings;
    }
}
