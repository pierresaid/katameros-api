using Helpers.Katameros;
using Katameros.DTOs;
using Katameros.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Katameros.Controllers;

[Route("[controller]")]
[ApiController]
public class FastsController(FastsRepository _fastsRepository) : ControllerBase
{
    [HttpGet]
    [Route("{year}/{languageId}")]
    public async Task<ActionResult<IEnumerable<FastPeriod>>> GetTranslatedFastsForYear(int year, int languageId)
    {
        if (!CopticDateHelper.IsSupportedYear(year))
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Year out of range",
                detail: $"The year must be between {CopticDateHelper.MinSupportedYear} and {CopticDateHelper.MaxSupportedYear}.");

        if (!await _fastsRepository.Configure(languageId))
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Unknown language",
                detail: $"No language has the id {languageId}.");

        return Ok(await _fastsRepository.GetFastsForYear(year));
    }
}
