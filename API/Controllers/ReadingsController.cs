using System.Globalization;
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
    private static readonly string[] GregorianFormats = ["d-M-yyyy", "yyyy-M-d"];

    private static readonly LocalDatePattern CopticPattern =
        LocalDatePattern.Create("d-M-yyyy", CultureInfo.InvariantCulture).WithCalendar(CalendarSystem.Coptic);

    [HttpGet]
    [Route("gregorian/{date}")]
    public async Task<ActionResult<DayReadings>> GetFromGregorianDate(string date, int languageId = -1, int bibleId = -1)
    {
        if (languageId == 4 && bibleId == -1)
            bibleId = 4;

        if (languageId == 3 && bibleId == -1)
            bibleId = 11;

        if (!DateTime.TryParseExact(ToAsciiDigits(date), GregorianFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            return InvalidDate(date, "dd-MM-yyyy, for example 24-12-2026");

        return await GetReadings(parsedDate, languageId, bibleId);
    }

    [HttpGet]
    [Route("coptic/{date}")]
    public async Task<ActionResult<DayReadings>> GetFromCopticDate(string date, int languageId = -1, int bibleId = -1)
    {
        var parseResult = CopticPattern.Parse(ToAsciiDigits(date));
        if (!parseResult.Success)
            return InvalidDate(date, "dd-MM-yyyy in the Coptic calendar, for example 13-04-1743");

        var parsedCopticDate = parseResult.Value;
        CopticDateHelper copticDateHelper = new CopticDateHelper(parsedCopticDate.Day, parsedCopticDate.Month, parsedCopticDate.Year);

        if (languageId == 4 && bibleId == -1)
            bibleId = 4;

        if (languageId == 3 && bibleId == -1)
            bibleId = 11;

        return await GetReadings(copticDateHelper.Date, languageId, bibleId);
    }

    private async Task<ActionResult<DayReadings>> GetReadings(DateTime gregorianDate, int languageId, int bibleId)
    {
        if (!CopticDateHelper.IsSupportedYear(gregorianDate.Year))
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Date out of range",
                detail: $"Dates must fall between the years {CopticDateHelper.MinSupportedYear} and {CopticDateHelper.MaxSupportedYear}.");

        if (!await _lectionaryRepository.Configure(languageId, bibleId))
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Unknown language or bible",
                detail: $"No bible matches languageId={languageId} and bibleId={bibleId}.");

        return await _lectionaryRepository.GetForDay(gregorianDate);
    }

    private ObjectResult InvalidDate(string date, string expectedFormat) =>
        Problem(statusCode: StatusCodes.Status400BadRequest, title: "Invalid date",
            detail: $"'{date}' is not a valid date. Use {expectedFormat}.");

    private static string ToAsciiDigits(string value) =>
        string.Concat(value.Select(c => char.IsDigit(c) ? (char)('0' + (int)char.GetNumericValue(c)) : c));
}
