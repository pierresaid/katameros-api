using Microsoft.AspNetCore.Mvc;

namespace Katameros.Controllers;

[Route("")]
[ApiController]
public class RootController : ControllerBase
{
    [HttpGet]
    public object Get()
    {
        return new { name = "Ⲁⲛⲁⲗⲟⲅⲓⲟⲛ", heartbeat = DateTime.Now };
    }
}