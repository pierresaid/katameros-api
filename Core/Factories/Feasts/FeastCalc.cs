using Katameros.DTOs;
using Katameros.Enums;
using NodaTime;

namespace Katameros;

/// <summary>
/// A Coptic Feast can be calculated from a Gregorian Date, a Coptic Date or from the days from Easter
/// </summary>
public class FeastCalc
{
    public DateTime? GregorianDate { get; set; }
    public LocalDate? CopticDate { get; set; }
    public Func<DateTime, LocalDate, bool> Validator { get; set; } = null;
    public Func<DateTime, LocalDate, DateTime> feastDateGetter = null;
    public int? EasterDaysDiff { get; set; }
    public Feast Feast { get; set; }

    public Func<Task<DayReadings>> FeastConstructor { get; set; }
    public FeastCalc(Feast Feast, Func<DateTime, LocalDate, bool> validator, Func<DateTime, LocalDate, DateTime> feastDateGetter, Func<Task<DayReadings>> FeastConstructor = null)
    {
        this.Feast = Feast;
        this.Validator = validator;
        this.feastDateGetter = feastDateGetter;
        this.FeastConstructor = FeastConstructor;
    }

    public FeastCalc(Feast Feast, DateTime GregorianDate, Func<Task<DayReadings>> FeastConstructor = null)
    {
        this.Feast = Feast;
        this.GregorianDate = GregorianDate;
        this.FeastConstructor = FeastConstructor;
    }

    public FeastCalc(Feast Feast, LocalDate CopticDate, Func<Task<DayReadings>> FeastConstructor = null)
    {
        this.Feast = Feast;
        this.CopticDate = CopticDate;
        this.FeastConstructor = FeastConstructor;
    }

    public FeastCalc(Feast Feast, int EasterDaysDiff, Func<Task<DayReadings>> FeastConstructor = null)
    {
        this.Feast = Feast;
        this.EasterDaysDiff = EasterDaysDiff;
        this.FeastConstructor = FeastConstructor;
    }
}
