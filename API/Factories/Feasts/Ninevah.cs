using Katameros.DTOs;
using Katameros.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsFactory
    {
        private async Task<DayReadings> ConstructFastOfNinevah1()
        {
            DayReadings dayReadings = new DayReadings
            {
                Title = await GetFeastTranslation(Feast.FastOfNinevah),
                Sections = new List<Section>
                {
                    await _readingsRepository.MakeVespers("19.95:1-2", "42.13:1-5", "32.1:1-17"),
                    await _readingsRepository.MakeMatins("19.103:1,8", "40.7:6-12"),
                    await _readingsRepository.MakeLitugy("45.6:17-23", "65.1:1-13", "44.2:38-47", "19.130:3-4", "40.12:35-45")
                }
            };
            return dayReadings;
        }

        private async Task<DayReadings> ConstructFastOfNinevah2()
        {
            DayReadings dayReadings = new DayReadings
            {
                Title = await GetFeastTranslation(Feast.FastOfNinevah),
                Sections = new List<Section>
                {
                    await _readingsRepository.MakeMatins("19.103:14,15,9,10", "42.13:6-9", "32.2:2-10"),
                    await _readingsRepository.MakeLitugy("51.1:21-29", "60.4:3-11", "44.17:30-34", "19.85:2-3", "42.11:29-36")
                }
            };
            return dayReadings;
        }

        private async Task<DayReadings> ConstructFastOfNinevah3()
        {
            DayReadings dayReadings = new DayReadings
            {
                Title = await GetFeastTranslation(Feast.FastOfNinevah),
                Sections = new List<Section>
                {
                    await _readingsRepository.MakeMatins("19.103:13,12", "40.11:25-30", "32.3:1-10*@+32.4:1-11"),
                    await _readingsRepository.MakeLitugy("49.2:1-23", "62.2:12-17", "44.15:12-20", "19.32:1,5", "40.15:32-39*@+40.16:1-4")
                }
            };
            return dayReadings;
        }
    }
}
