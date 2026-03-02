using Helpers.Katameros;
using Katameros.Enums;
using NodaTime;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private readonly DatabaseContext _context;
    private readonly ReadingsRepository _readingsRepository;
    private readonly ReadingsHelper _readingsHelper;
    private readonly List<FeastCalc> _feasts;

    public FeastsFactory(DatabaseContext context, ReadingsRepository readingsRepository, ReadingsHelper readingsHelper)
    {
        _context = context;
        _readingsRepository = readingsRepository;
        _readingsHelper = readingsHelper;
        _feasts =
        [
            new FeastCalc(Feast.Christmas, new DateTime(1, 01, 07), ConstructChristmas),
            new FeastCalc(Feast.NativityParamoun, IsNativityParamoun, GetNativityParamounDate, ConstructNativityParamoun),
            new FeastCalc(Feast.Ascension, 39, null),
            new FeastCalc(Feast.LazarusSaturday, -8, ConstructLazarusSaturday),
            new FeastCalc(Feast.TempleEntrance, new LocalDate(1, 6, CopticMonths.Baramoudah, CalendarSystem.Coptic), ConstructTempleEntrance),
            new FeastCalc(Feast.EgyptEntrance, new LocalDate(1, CopticMonths.Bashans, 24, CalendarSystem.Coptic), ConstructEgyptEntrance),
            new FeastCalc(Feast.Annunciation, new LocalDate(1, CopticMonths.Baramhat, 29, CalendarSystem.Coptic), ConstructAnnunciation),
            new FeastCalc(Feast.Cross, new LocalDate(1, CopticMonths.Baramhat, 10, CalendarSystem.Coptic), ConstructFeastOfTheCross),
            new FeastCalc(Feast.Cross, new LocalDate(1, CopticMonths.Thout, 17, CalendarSystem.Coptic), ConstructFeastOfTheCross),
            new FeastCalc(Feast.Cross, new LocalDate(1, CopticMonths.Thout, 18, CalendarSystem.Coptic), ConstructFeastOfTheCross2),
            new FeastCalc(Feast.Cross, new LocalDate(1, CopticMonths.Thout, 19, CalendarSystem.Coptic), ConstructFeastOfTheCross3),
            new FeastCalc(Feast.FastOfNinevah, -69, ConstructFastOfNinevah1),
            new FeastCalc(Feast.FastOfNinevah, -68, ConstructFastOfNinevah2),
            new FeastCalc(Feast.FastOfNinevah, -67, ConstructFastOfNinevah3),
            new FeastCalc(Feast.Jonas, -66, ConstructFeastOfJonas),
            new FeastCalc(Feast.PalmSunday, -7, ConstructPalmSunday),
            new FeastCalc(Feast.PaschaMonday, -6, ConstructPaschaMonday),
            new FeastCalc(Feast.PaschaTuesday, -5, ConstructPaschaTuesday),
            new FeastCalc(Feast.TheophanyParamoun, new LocalDate(1, CopticMonths.Toubah, 10, CalendarSystem.Coptic), ConstructParamounTheophany),
            new FeastCalc(Feast.Theophany, new LocalDate(1, CopticMonths.Toubah, 11, CalendarSystem.Coptic), ConstructTheophany),
            new FeastCalc(Feast.WeddingOfCana, new LocalDate(1, CopticMonths.Toubah, 13, CalendarSystem.Coptic), ConstructWeddingOfCana),
            new FeastCalc(Feast.GreatLentPreparationSaturday, -57, ConstructGreatLentPreparationSaturday),
            new FeastCalc(Feast.GreatLentPreparationSunday, -56, ConstructGreatLentPreparationSunday),
            new FeastCalc(Feast.PaschaWednesday, -4, ConstructPaschaWednesday),
            new FeastCalc(Feast.PaschaThursday, -3, ConstructPaschaThursday),
            new FeastCalc(Feast.PaschaFriday, -2, ConstructPaschaFriday),
            new FeastCalc(Feast.PaschaSaturday, -1, ConstructPaschaSaturday),
            new FeastCalc(Feast.Easter, 0, ConstructEaster),
            new FeastCalc(Feast.Nayrouz, new LocalDate(1, CopticMonths.Thout, 1, CalendarSystem.Coptic), ConstructNayrouz),
        ];
    }
    public FeastCalc GetDayFeast(DateTime gregorianDate, LocalDate copticDate, int easterDaysDiff)
    {

        foreach (FeastCalc feastCalc in this._feasts)
        {
            if (feastCalc.EasterDaysDiff.HasValue && feastCalc.EasterDaysDiff == easterDaysDiff)
                return feastCalc;
            else if (feastCalc.CopticDate.HasValue && feastCalc.CopticDate.Value.Day == copticDate.Day && feastCalc.CopticDate.Value.Month == copticDate.Month)
                return feastCalc;
            else if (feastCalc.GregorianDate.HasValue && feastCalc.GregorianDate.Value.Day == gregorianDate.Day && feastCalc.GregorianDate.Value.Month == gregorianDate.Month)
                return feastCalc;
            else if (feastCalc.Validator != null && feastCalc.Validator(gregorianDate, copticDate))
                return feastCalc;
        }
        return null;
    }

    /// <summary>
    /// Get The date of all the feasts for a year
    /// </summary>
    /// <param name="year"></param>
    public List<(Feast, DateTime)> ComputeFeastsDate(int year)
    {
        List<(Feast, DateTime)> feasts = new List<(Feast, DateTime)>();

        var date = new DateTime(year, 1, 1);
        var copticDate = date.ToCopticDate();
        CopticDateHelper copticDateHelper = new CopticDateHelper(date);
        var easterDate = copticDateHelper.GetEasterDate();

        foreach (var feastCalc in _feasts)
        {
            if (feastCalc.EasterDaysDiff.HasValue)
                feasts.Add(ValueTuple.Create(feastCalc.Feast, easterDate.AddDays(feastCalc.EasterDaysDiff.Value)));
            else if (feastCalc.CopticDate.HasValue)
                feasts.Add(ValueTuple.Create(
                    feastCalc.Feast,
                    CopticDateHelper.ToGregorianDate(new LocalDate(copticDate.Year, feastCalc.CopticDate.Value.Month, feastCalc.CopticDate.Value.Day, CalendarSystem.Coptic))));
            else if (feastCalc.GregorianDate.HasValue)
                feasts.Add(ValueTuple.Create(feastCalc.Feast, new DateTime(year, feastCalc.GregorianDate.Value.Month, feastCalc.GregorianDate.Value.Day)));
            else if (feastCalc.feastDateGetter != null)
                feasts.Add(ValueTuple.Create(feastCalc.Feast, feastCalc.feastDateGetter(date, copticDate)));
        }
        return feasts;
    }

    public async Task<string> GetFeastTranslation(Feast feastId)
    {
        return (await _context.FeastsTranslations.FindAsync((int)feastId, _context.LanguageId))?.Text;
    }
}
