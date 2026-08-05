using Helpers.Katameros;
using Katameros.Enums;

namespace Katameros.Repositories;

public class FastsFactory
{
    /// <summary>
    /// Get the start and end dates of the fasting periods for a year.
    /// A fast belongs to the year its start falls in: the Nativity fast starts
    /// in November of the requested year and ends the following January 6.
    /// </summary>
    public List<(Fast Fast, DateTime Start, DateTime End)> ComputeFastPeriods(int year)
    {
        var easterDate = new CopticDateHelper(new DateTime(year, 1, 1)).GetEasterDate();

        return
        [
            (Fast.NinevehFast, easterDate.AddDays(-69), easterDate.AddDays(-67)),
            // The Great Fast proper; Holy Week continues as the Pascha feast days
            (Fast.GreatFast, easterDate.AddDays(-55), easterDate.AddDays(-9)),
            // Starts the day after Pentecost, ends on the eve of the Apostles' Feast (Abib 5)
            (Fast.ApostlesFast, easterDate.AddDays(50), CopticDateHelper.ResolveInGregorianYear(CopticMonths.Abib, 4, year)),
            (Fast.StMaryFast, CopticDateHelper.ResolveInGregorianYear(CopticMonths.Misra, 1, year), CopticDateHelper.ResolveInGregorianYear(CopticMonths.Misra, 15, year)),
            // Coptic start (shifts a day with the Coptic leap cycle) but fixed
            // Gregorian end: the church anchors the Nativity to January 7
            (Fast.NativityFast, CopticDateHelper.ResolveInGregorianYear(CopticMonths.Hatour, 16, year), new DateTime(year + 1, 1, 6)),
        ];
    }
}
