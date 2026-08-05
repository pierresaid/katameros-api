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
    public async Task<IEnumerable<FastPeriod>> GetTranslatedFastsForYear(int year, int languageId)
    {
        _fastsRepository.Configure(languageId);
        return await _fastsRepository.GetFastsForYear(year);
    }
}
