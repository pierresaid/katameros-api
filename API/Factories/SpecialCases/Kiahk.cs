using System;
using System.Threading.Tasks;
using Helpers.Katameros;
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
                return await _readingsRepository.GetReadingsForAnnual(CopticDateHelper.CreateCopticDate(30, CopticMonths.Kiahk));
            }
            // If Christmas fall on 28 kiahk we repeat the readings for 29 Kiahk also
            if (_copticDate.Month == CopticMonths.Kiahk && _copticDate.Day == 29 
                && new DateTime(_gregorianDate.Year, 1, 7).ToCopticDate().Day == 28)
            {
                return await _readingsRepository.GetReadingsForAnnual(CopticDateHelper.CreateCopticDate(29, CopticMonths.Kiahk));
            }
            return null;
        }
    }
}
