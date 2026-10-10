using Katameros.DTOs;
using Katameros.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Katameros.Controllers;

[Route("[controller]")]
[ApiController]
public class SynaxariumController(SynaxariumRepository _synaxariumRepository) : ControllerBase
{
    /// <summary>
    /// One synaxarium story by its permanent English slug, e.g. GET /synaxarium/christmas?languageId=1.
    /// The story does not depend on the lectionary, so it is available on every day of the year,
    /// including Holy Week when the readings leave the synaxarium out.
    /// </summary>
    [HttpGet]
    [Route("{slug}")]
    public async Task<ActionResult<SynaxariumStoryDetails>> GetStory(string slug, int languageId = -1)
    {
        if (!await _synaxariumRepository.LanguageExists(languageId))
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Unknown language",
                detail: $"No language has the id {languageId}.");

        var (storyExists, story) = await _synaxariumRepository.GetStory(slug.Trim().ToLowerInvariant(), languageId);
        if (!storyExists)
            return NotFoundProblem("unknown-story", "Unknown story",
                $"No synaxarium story has the slug '{slug}'.");
        if (story == null)
            return NotFoundProblem("not-in-language", "Story not available in this language",
                $"The story '{slug}' has no version in language {languageId}.");

        return story;
    }

    // The code tells the two 404 cases apart without parsing the title.
    private ObjectResult NotFoundProblem(string code, string title, string detail)
    {
        var problem = ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status404NotFound, title: title, detail: detail);
        problem.Extensions["code"] = code;
        return new ObjectResult(problem) { StatusCode = StatusCodes.Status404NotFound, ContentTypes = { "application/problem+json" } };
    }
}
