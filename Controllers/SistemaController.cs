using CoderHouse_SistemaGestion.Models;
using CoderHouse_SistemaGestion.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace CoderHouse_SistemaGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemaController : ControllerBase
    {
        [HttpGet]
        [Route("GetNombreSistema")]
        public IActionResult GetNombreSistema()
        {
            var result = SistemaRepository.GetNombreSistema();
            if (!result.Any())
            {
                return NotFound("No se encuentra el servicio");
            }
            return Ok(result);
            
        }

    }
}
