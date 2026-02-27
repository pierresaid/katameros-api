using Katameros.DTOs;

namespace Katameros.Factories.SpecialCases;

public interface ISpecialCase
{
    /// <summary>
    /// Handle the special case and returns the readings of it applies
    /// </summary>
    public Task<DayReadings?> Process();
}
