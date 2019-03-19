using Microsoft.AspNetCore.Mvc;
using System;

namespace api.web.Controllers
{
    [Route("api/utils")]
    [ApiController]
    public class UtisController : ControllerBase
    {

        [HttpGet("datahora")]
        public IActionResult GetDataHora()
        {           
            return Ok(DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"));
        }
    }
}