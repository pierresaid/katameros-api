using Katameros.DTOs;
using Katameros.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsFactory
    {
        private async Task<DayReadings> ConstructGreatLentPreparationSunday()
        {
            DayReadings dayReadings = new DayReadings
            {
                Title = await GetFeastTranslation(Feast.GreatLentPreparationSunday),
                Sections = new List<Section>
                {
                    await _readingsRepository.MakeVespers("19.46:10", "41.11:22-26"),
                    await _readingsRepository.MakeMatins("19.100:2-3", "42.17:3-10", null),
                    await _readingsRepository.MakeLitugy("47.11:16-28", "61.1:1-11", "44.21:15-26", "19.2:11,10", "40.6:1-18")
                }
            };
            return dayReadings;
        }
    }
}
