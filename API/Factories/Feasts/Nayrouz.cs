using Katameros.DTOs;
using Katameros.Enums;
using NodaTime;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsFactory
    {
        private async Task<DayReadings> ConstructNayrouz()
        {
            var dayReadings = await _readingsRepository.GetReadingsForAnnual(new LocalDate(1, CopticMonths.Thout, 1, CalendarSystem.Coptic));
            dayReadings.Title = "Nayrouz";
            //DayReadings dayReadings = new DayReadings
            //{
            //    Title = await GetFeastTranslation(Feast.Nayrouz),
            //    Sections = new List<Section>
            //    {
            //        await _readingsRepository.MakeVespers("19.144:5,7", "42.7:36-50"),
            //        await _readingsRepository.MakeMatins("19.72:6-7", "42.11:20-28", null),
            //        await _readingsRepository.MakeLitugy("45.3:1-31", "62.1:1-10*@+62.2:1-6", "44.7:23-34", "19.45:10-11", "42.1:26-38")
            //    }
            //};
            return dayReadings;
        }
    }
}
