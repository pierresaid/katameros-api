using Katameros.DTOs;
using Katameros.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeastsController : ControllerBase
    {
        private readonly FeastsRepository _feastsRepository;
        public FeastsController(FeastsRepository feastsRepository)
        {
            _feastsRepository = feastsRepository;
        }

        [HttpGet]
        [Route("{year}/{languageId}")]
        public async Task<IEnumerable<FeastDate>> GetTranslatedFeastsForYear(int year, int languageId)
        {
            _feastsRepository.Configure(languageId);
            return await _feastsRepository.GetFeastsForYear(year);
        }
    }
}
