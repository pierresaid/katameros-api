using Helpers.Katameros;
using Katameros.DTOs;
using Katameros.Enums;
using NodaTime;
using System;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsFactory
    {
        private DateTime GetParamounDate(DateTime gregorianDate, LocalDate _)
        {
            return new DateTime(gregorianDate.Year, 1, 6);
        }

        private bool IsParamoun(DateTime gregorianDate, LocalDate copticDate)
        {
            // If Paramoun fall on a Sunday the Paramoun last 3 days, 2 days on a Saturday or just one day
            var paramounDay = new DateTime(gregorianDate.Year, 1, 6);
            return
                copticDate.Month == CopticMonths.Kiahk && (
                paramounDay.DayOfWeek == DayOfWeek.Sunday && gregorianDate >= paramounDay.AddDays(-2) && gregorianDate < paramounDay
                || paramounDay.DayOfWeek == DayOfWeek.Saturday && gregorianDate >= paramounDay.AddDays(-1) && gregorianDate < paramounDay
                || gregorianDate == paramounDay);
        }

        private async Task<DayReadings> ConstructParamoun()
        {
            DayReadings paramoun = await _readingsRepository.GetReadingsForAnnual(CopticDateHelper.CreateCopticDate(28, CopticMonths.Kiahk), Feast.Paramoun);
            return paramoun;
        }
    }
}
