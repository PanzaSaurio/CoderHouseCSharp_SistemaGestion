using CoderHouse_SistemaGestion.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoderHouse_SistemaGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {

        [HttpGet]
        [Route("GetProductosVendidos")]
        public ActionResult GetProductosVendidos([FromQuery] int pIdUsuario)
        {
            var result = ProductoVendidoRepository.TraerProductosVendidos(pIdUsuario);
            return Ok(result);
        }
    }
}
