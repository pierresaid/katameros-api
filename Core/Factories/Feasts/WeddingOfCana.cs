using Katameros.DTOs;
using Katameros.Enums;
using NodaTime;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructWeddingOfCana()
    {
        var dayReadings = await _readingsRepository.GetReadingsForAnnual(new LocalDate(1, CopticMonths.Toubah, 13, CalendarSystem.Coptic), Feast.WeddingOfCana);
        dayReadings.Title = await GetFeastTranslation(Feast.WeddingOfCana);
        return dayReadings;
    }
}
