using Katameros.DTOs;
using Katameros.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class LectionaryRepository : ILectionaryRepository
    {
        private async Task<DayReadings> ConstructChristmas()
        {
            DayReadings dayReadings = new DayReadings
            {
                Title = await GetFeastTranslation(Feast.Christmas),
                Sections = new List<Section>
                {
                    await MakeVespers("19.72:10", "42.3:23-38"),
                    await MakeMatins("19.72:15", "43.1:14-17", null),
                    await MakeLitugy("58.1:1-14*@+58.2:1-4", "61.1:12-17", "44.13:26-33", "19.2:7-8", "40.2:1-12")
                }
            };
            return dayReadings;
        }
    }
}
