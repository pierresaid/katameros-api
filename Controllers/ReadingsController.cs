using System;
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
        public object GetFromGregorianDate(string date)
        {
            DateTime parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

            _lectionaryRepository.Configure();
            var res = _lectionaryRepository.GetForDay(parsedDate);

            return res;
        }

        [HttpGet]
        [Route("coptic/{date}")]
        public object GetFromCopticDate(string date)
        {
            var parsedCopticDate = LocalDatePattern.Create("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture).WithCalendar(CalendarSystem.Coptic).Parse(date).Value;
            CopticDateHelper copticDateHelper = new CopticDateHelper(parsedCopticDate.Day, parsedCopticDate.Month, parsedCopticDate.Year);

            _lectionaryRepository.Configure();
            var res = _lectionaryRepository.GetForDay(copticDateHelper.Date);

            return res;
        }
    }
}