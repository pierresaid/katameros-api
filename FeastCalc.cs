using Katameros.DTOs;
using Katameros.Enums;
using NodaTime;
using System;
using System.Threading.Tasks;

namespace Katameros
{
    class FeastCalc
    {
        public DateTime? GregorianDate { get; set; }
        public LocalDate? CopticDate { get; set; }
        public int? EasterDaysDiff { get; set; }
        public Feast Feast { get; set; }
        public Func<Task<DayReadings>> FeastConstructor { get; set; }
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
}
