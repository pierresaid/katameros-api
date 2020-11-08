using Katameros.DTOs;
using Katameros.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsRepository
    {
        private async Task<DayReadings> ConstructTempleEntrance()
        {
            DayReadings dayReadings = new DayReadings
            {
                Title = await GetFeastTranslation(Feast.TempleEntrance),
                Sections = new List<Section>
                {
                    await _readingsRepository.MakeVespers("19.116:16-19", "42.2:15-20"),
                    await _readingsRepository.MakeMatins("19.66:13-15", "42.2:40-52", null),
                    await _readingsRepository.MakeLitugy("50.3:1-12", "61.1:12-21", "44.15:13-21", "19.50:14,23", "42.2:21-39")
                }
            };
            return dayReadings;
        }
    }
}
