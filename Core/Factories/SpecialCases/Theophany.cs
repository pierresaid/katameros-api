using Helpers.Katameros;
using Katameros.DTOs;
using Katameros.Enums;
using Katameros.Repositories;
using NodaTime;

namespace Katameros.Factories.SpecialCases;

public class Theophany : ISpecialCase
{
    private readonly ReadingsRepository _readingsRepository;
    private readonly ReadingsHelper _readingsHelper;


    private DateTime _gregorianDate;
    private LocalDate _copticDate;
    private int _easterDaysDiff;

    public Theophany(DateTime gregorianDate, LocalDate copticDate, int easterDaysDiff, ReadingsRepository readingsRepository, ReadingsHelper readingsHelper)
    {
        _readingsRepository = readingsRepository;
        _readingsHelper = readingsHelper;
        _gregorianDate = gregorianDate;
        _copticDate = copticDate;
        _easterDaysDiff = easterDaysDiff;
    }

    public async Task<DayReadings> Process()
    {
        // If Theophany falls on a Monday Paramoun is Friday, Saturday, and Sunday
        // If Theophany falls on a Sunday Paramoun is Friday and Saturday

        // Only process in Toubah month
        if (_copticDate.Month != CopticMonths.Toubah)
            return null;

        // Create Theophany date (11 Toubah) for this coptic year
        var theophanyDate = new LocalDate(_copticDate.Year, CopticMonths.Toubah, 11, CalendarSystem.Coptic);
        var theophanyDayOfWeek = theophanyDate.DayOfWeek;
        var currentDayOfWeek = _copticDate.DayOfWeek;

        bool isParamounDay = false;

        // If Theophany falls on a Monday, Paramoun is Friday (8), Saturday (9), and Sunday (10)
        // Day 10 (Sunday) is already handled by FeastsFactory, so we only handle Friday (8) and Saturday (9)
        if (theophanyDayOfWeek == IsoDayOfWeek.Monday
            && ((currentDayOfWeek == IsoDayOfWeek.Friday && _copticDate.Day == 8)
                || (currentDayOfWeek == IsoDayOfWeek.Saturday && _copticDate.Day == 9)))
        {
            isParamounDay = true;
        }
        // If Theophany falls on a Sunday, Paramoun is Friday (9) and Saturday (10)
        // Day 10 (Saturday) is already handled by FeastsFactory, so we only handle Friday (9)
        else if (theophanyDayOfWeek == IsoDayOfWeek.Sunday
            && currentDayOfWeek == IsoDayOfWeek.Friday && _copticDate.Day == 9)
        {
            isParamounDay = true;
        }

        if (!isParamounDay)
            return null;

        // Construct Paramoun Theophany readings
        return new DayReadings
        {
            Title = await _readingsHelper.GetFeastTranslation(Feast.TheophanyParamoun),
            Sections = new List<Section>
            {
                await _readingsRepository.MakeVespers("19.42:2,5", "40.4:12-22"),
                await _readingsRepository.MakeMatins("19.42:7,8", "43.3:22-29"),
                await _readingsRepository.MakeLitugy("46.1:1-17", "61.1:12-19", "44.16:25-34", "19.44:3,4", "42.3:1-18")
            }
        };
    }
}
