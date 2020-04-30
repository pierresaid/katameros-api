using System;
using System.Threading.Tasks;
using Katameros.DTOs;
using Katameros.Repositories;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTime.Text;

namespace Katameros.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReadingsController : ControllerBase
    {
        private readonly ILectionaryRepository _lectionaryRepository;
        public ReadingsController(ILectionaryRepository lectionaryRepository)
        {
            _lectionaryRepository = lectionaryRepository;
        }

        [HttpGet]
        [Route("gregorian/{date}")]
        public async Task<DayReadings> GetFromGregorianDate(string date)
        {
            DateTime parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

            await _lectionaryRepository.Configure();
            var res = await _lectionaryRepository.GetForDay(parsedDate);

            return res;
        }

        [HttpGet]
        [Route("coptic/{date}")]
        public async Task<DayReadings> GetFromCopticDate(string date)
        {
            var parsedCopticDate = LocalDatePattern.Create("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture).WithCalendar(CalendarSystem.Coptic).Parse(date).Value;
            CopticDateHelper copticDateHelper = new CopticDateHelper(parsedCopticDate.Day, parsedCopticDate.Month, parsedCopticDate.Year);

            await _lectionaryRepository.Configure();
            var res = await _lectionaryRepository.GetForDay(copticDateHelper.Date);

            return res;
        }
    }
}