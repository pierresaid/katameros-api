using Helpers.Katameros;
using Katameros.DTOs;
using Katameros.Enums;
using Katameros.Repositories;
using NodaTime;

namespace Katameros.Factories.SpecialCases;

public class KiahkSpecialCase : ISpecialCase
{
    private readonly ReadingsRepository _readingsRepository;


    private readonly DateTime _gregorianDate;
    private readonly LocalDate _copticDate;

    public KiahkSpecialCase(DateTime gregorianDate, LocalDate copticDate, ReadingsRepository readingsRepository)
    {
        _readingsRepository = readingsRepository;
        _gregorianDate = gregorianDate;
        _copticDate = copticDate;
    }

    public async Task<DayReadings?> Process()
    {
        // If the day which follows Christmas is on a sunday we read the 30 kiahk annual reading, and not the lessons of the fifth Sunday
        if (_copticDate.Month == CopticMonths.Kiahk && _copticDate.DayOfWeek == IsoDayOfWeek.Sunday && _copticDate.Day == 30)
        {
            return await _readingsRepository.GetReadingsForAnnual(CopticDateHelper.CreateCopticDate(30, CopticMonths.Kiahk));
        }
        // If Christmas fall on 28 kiahk we repeat the readings for 29 Kiahk also
        if (_copticDate.Month == CopticMonths.Kiahk && _copticDate.Day == 29 
            && new DateTime(_gregorianDate.Year, 1, 7).ToCopticDate().Day == 28)
        {
            return await _readingsRepository.GetReadingsForAnnual(CopticDateHelper.CreateCopticDate(29, CopticMonths.Kiahk));
        }

        // If the fourth Sunday of Kiyahk falls immediately before the Nativity
        // This means that the month of Kiyahk would be left with only three Sundays
        // The fifth Sunday of Hatour is borrowed and added to the three Sundays of Kiyahk
        var paramounDate = new DateTime(_gregorianDate.Year + 1, 1, 6); // The next (gregorian year's) paramoun
        if (paramounDate.DayOfWeek == DayOfWeek.Sunday && _gregorianDate.DayOfWeek == DayOfWeek.Sunday)
        {
            var nbSundays = CopticDateHelper.NumberOfSundaysElapsed(_copticDate);
            if (_copticDate.Month == CopticMonths.Hatour && CopticDateHelper.IsLastSundayOfMonth(_copticDate))
                return await _readingsRepository.GetReadingsForSunday(CopticMonths.Kiahk, 1);
            else if (_copticDate.Month == CopticMonths.Kiahk) 
                return await _readingsRepository.GetReadingsForSunday(CopticMonths.Kiahk, nbSundays + 1);
        }
        return null;
    }
}
