using Helpers.Katameros;
using Katameros.DTOs;
using Katameros.Repositories;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTime.Text;

namespace Katameros.Controllers;

[Route("[controller]")]
[ApiController]
public class ReadingsController(LectionaryRepository _lectionaryRepository) : ControllerBase
{
    [HttpGet]
    [Route("gregorian/{date}")]
    public async Task<DayReadings> GetFromGregorianDate(string date, int languageId = -1, int bibleId = -1)
    {
        if (languageId == 4 && bibleId == -1)
            bibleId = 4;

        if (languageId == 3 && bibleId == -1)
            bibleId = 11;

        DateTime parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

        await _lectionaryRepository.Configure(languageId, bibleId);
        var res = await _lectionaryRepository.GetForDay(parsedDate);

        return res;
    }

    [HttpGet]
    [Route("coptic/{date}")]
    public async Task<DayReadings> GetFromCopticDate(string date, int languageId = -1, int bibleId = -1)
    {
        var parsedCopticDate = LocalDatePattern.Create("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture).WithCalendar(CalendarSystem.Coptic).Parse(date).Value;
        CopticDateHelper copticDateHelper = new CopticDateHelper(parsedCopticDate.Day, parsedCopticDate.Month, parsedCopticDate.Year);

        if (languageId == 4 && bibleId == -1)
            bibleId = 4;

        if (languageId == 3 && bibleId == -1)
            bibleId = 11;

        await _lectionaryRepository.Configure(languageId, bibleId);
        var res = await _lectionaryRepository.GetForDay(copticDateHelper.Date);

        return res;
    }
}