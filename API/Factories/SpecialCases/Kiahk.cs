using System;
using System.Threading.Tasks;
using Katameros.DTOs;
using Katameros.Enums;
using Katameros.Repositories;
using NodaTime;

namespace Katameros.Factories.SpecialCases
{
    public class KiahkSpecialCase : ISpecialCase
    {
        private readonly ReadingsRepository _readingsRepository;


        private DateTime _gregorianDate;
        private LocalDate _copticDate;
        private int _easterDaysDiff;

        public KiahkSpecialCase(DateTime gregorianDate, LocalDate copticDate, int easterDaysDiff, ReadingsRepository readingsRepository)
        {
            _readingsRepository = readingsRepository;
            _gregorianDate = gregorianDate;
            _copticDate = copticDate;
            _easterDaysDiff = easterDaysDiff;
        }

        public async Task<DayReadings> Process()
        {
            // If the day which follows Christmas is on a sunday we read the 30 kiahk annual reading, and not the lessons of the fifth Sunday
            if (_copticDate.Month == CopticMonths.Kiahk && _copticDate.DayOfWeek == IsoDayOfWeek.Sunday && _copticDate.Day == 30)
            {
                var copticDate = CreateCopticDate(30, CopticMonths.Kiahk);
                return await _readingsRepository.GetReadingsForAnnual(copticDate);
            }
            return null;
        }

        private static LocalDate CreateCopticDate(int copticDay, int copticMonth)
        {
            var currentYear = LocalDate.FromDateTime(DateTime.Now, CalendarSystem.Gregorian)
                                                 .WithCalendar(CalendarSystem.Coptic).Year;
            var copticDate = new LocalDate(currentYear, copticMonth, copticDay);
            return copticDate;
        }
    }
}
