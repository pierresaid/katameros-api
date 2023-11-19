using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructGreatLentPreparationSaturday()
    {
        DayReadings dayReadings = new DayReadings
        {
            Title = await GetFeastTranslation(Feast.GreatLentPreparationSaturday),
            Sections = new List<Section>
            {
                await _readingsRepository.MakeVespers("19.17:1-2", "42.17:3-6"),
                await _readingsRepository.MakeMatins("19.119:49,52", "41.13:33-37", null),
                await _readingsRepository.MakeLitugy("47.6:14-18*@+47.7:1-16", "60.1:1-12", "44.21:1-14", "19.95:1-2", "42.13:1-5")
            }
        };
        return dayReadings;
    }
}
