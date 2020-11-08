using Katameros.DTOs;
using Katameros.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsRepository
    {
        private async Task<DayReadings> ConstructFeastOfJonas()
        {
            DayReadings dayReadings = new DayReadings
            {
                Title = await GetFeastTranslation(Feast.Jonas),
                Sections = new List<Section>
                {
                    await _readingsRepository.MakeMatins("19.30:10,11", "41.8:10-21"),
                    await _readingsRepository.MakeLitugy("45.10:4-18", "60.3:17-22", "44.3:22-26", "19.118:5,18", "43.2:13-25")
                }
            };
            return dayReadings;
        }
    }
}
