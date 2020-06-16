using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Katameros.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TestController(DatabaseContext context) => _context = context;
        [HttpGet]
        public object Get()
        {
            var res = _context.Feasts.ToList();
            return res;
        }
    }
}