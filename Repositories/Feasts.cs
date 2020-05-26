using Katameros.DTOs;
using Katameros.Enums;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class LectionaryRepository : ILectionaryRepository
    {
        FeastCalc GetDayFeast(DateTime gregorianDate, LocalDate copticDate, int easterDaysDiff)
        {
            var feastCalcs = new List<FeastCalc>
            {
                new FeastCalc(Feast.Christmas, new DateTime(1, 01, 07), ConstructChristmas)
            };
            foreach (FeastCalc feastCalc in feastCalcs)
            {
                if (feastCalc.EasterDaysDiff.HasValue && feastCalc.EasterDaysDiff == easterDaysDiff)
                    return feastCalc;
                else if (feastCalc.CopticDate.HasValue && feastCalc.CopticDate.Value.Day == copticDate.Day && feastCalc.CopticDate.Value.Month == copticDate.Month)
                    return feastCalc;
                else if (feastCalc.GregorianDate.HasValue && feastCalc.GregorianDate.Value.Day == gregorianDate.Day && feastCalc.GregorianDate.Value.Month == gregorianDate.Month)
                    return feastCalc;
            }
            return null;
        }
        private async Task<DayReadings> ConstructChristmas()
        {
            DayReadings dayReadings = new DayReadings
            {
                Title = await GetFeastTranslation((int)Feast.Christmas),
                Sections = new List<Section>
            {
                await MakeVespers("19.72:10", "42.3:23-38"),
                await MakeMatins("19.72:15", "43.1:14-17", null),
                await MakeLitugy("58.1:1-14*@+58.2:1-4", "61.1:12-17", "44.13:26-33", "19.2:7-8", "40.2:1-12")
            }
            };
            return dayReadings;
        }

        private async Task<string> GetFeastTranslation(int feastId)
        {
            return (await _context.FeastsTranslations.FindAsync(feastId, LanguageId)).Text;
        }
    }
}
