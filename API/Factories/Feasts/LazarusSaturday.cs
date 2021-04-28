using Katameros.DTOs;
using Katameros.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsFactory
    {
        private async Task<DayReadings> ConstructLazarusSaturday()
        {
            DayReadings dayReadings = new DayReadings
            {
                Title = await GetFeastTranslation(Feast.LazarusSaturday),
                Sections = new List<Section>
                {
                    await _readingsRepository.MakeMatins("19.30:3,11", "42.18:35-43", "1.49:1-28*@+23.40:9-31*@+36.3:14-20*@+38.9:9-15"),
                    await _readingsRepository.MakeLitugy("46.2:1-8", "60.1:25*@+60.2:1-6", "44.27:38-44*@+44.28:1-10", "19.129:8,2", "43.11:1-45")
                }
            };
            return dayReadings;
        }
    }
}
