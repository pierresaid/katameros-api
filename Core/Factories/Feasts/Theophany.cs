using Katameros.DTOs;
using Katameros.Enums;
using NodaTime;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructTheophany()
    {
        var dayReadings = await _readingsRepository.GetReadingsForAnnual(new LocalDate(1, CopticMonths.Toubah, 11, CalendarSystem.Coptic), Feast.Theophany);
        dayReadings.Title = await GetFeastTranslation(Feast.Theophany);
        return dayReadings;
    }
}
