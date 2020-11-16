using Katameros.DTOs;
using Katameros.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsFactory
    {
        private async Task<DayReadings> ConstructPalmSunday()
        {
            DayReadings dayReadings = new DayReadings
            {
                Title = await GetFeastTranslation(Feast.PalmSunday),
                Sections = new List<Section>
                {
                    await _readingsRepository.MakeVespers("19.118:26-27", "43.12:1-11"),
                    await _readingsRepository.MakeMatins("19.68:19,35", "42.19:1-10", null),
                }
            };
            var liturgy = await _readingsRepository.MakeLitugy("58.9:11-28", "60.4:1-11", "44.28:11-31", null, null);
            liturgy.subSections.Add(await _readingsRepository.MakePsalmAndGospel("19.81:3,1,2", "40.21:1-17"));
            liturgy.subSections.Add(await _readingsRepository.MakePsalmAndGospel(null, "41.11:1-11"));
            liturgy.subSections.Add(await _readingsRepository.MakePsalmAndGospel(null, "42.19:29-48"));
            liturgy.subSections.Add(await _readingsRepository.MakePsalmAndGospel("19.65:1-2", "43.12:12-19"));
            dayReadings.Sections.Add(liturgy);
            return dayReadings;
        }
    }
}
