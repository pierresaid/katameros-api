using Katameros.DTOs;
using Katameros.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Katameros.Controllers;

[Route("[controller]")]
[ApiController]
public class FeastsController(FeastsRepository _feastsRepository) : ControllerBase
{
    [HttpGet]
    [Route("{year}/{languageId}")]
    public async Task<IEnumerable<FeastDate>> GetTranslatedFeastsForYear(int year, int languageId)
    {
        _feastsRepository.Configure(languageId);
        return await _feastsRepository.GetFeastsForYear(year);
    }
}
