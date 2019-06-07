using api.domain.Entity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace api.web.Controllers
{
    [Route("api/utils")]
    [ApiController]
    public class UtilsController : ControllerBase
    {

        [HttpGet("datahora")]
        public IActionResult GetDataHora()
        {           
            return Ok(DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"));
        }
        
        [HttpGet("orgaoExpeditor")]
        public IActionResult GetOrgaoExpeditor()
        {
            var orgaoExpeditor = new OrgaoExpeditor();
            return Ok(orgaoExpeditor.GetAll());
        }

        [HttpGet("tipoConta")]
        public IActionResult GetTipoConta()
        {
            var tipoConta = new TipoConta();
            return Ok(tipoConta.GetAll());
        }

    }
}