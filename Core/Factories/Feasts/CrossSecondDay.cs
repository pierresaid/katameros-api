using Helpers.Katameros;
using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructFeastOfTheCrossDay2()
    {
        var reading = await _readingsRepository.GetReadingsForAnnual(CopticDateHelper.CreateCopticDate(18, CopticMonths.Thout));
        reading.Title = await GetFeastTranslation(Feast.Cross);
        return reading;
    }
}
