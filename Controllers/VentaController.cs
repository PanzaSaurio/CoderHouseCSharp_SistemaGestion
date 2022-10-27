using CoderHouse_SistemaGestion.Models;
using CoderHouse_SistemaGestion.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoderHouse_SistemaGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        [HttpGet]
        [Route("GetVentas")]
        public ActionResult GetVentas([FromQuery] int pIdUsuario)
        {
            var result = VentaRepository.TraerVentas(pIdUsuario);
            return Ok(result);
        }

        [HttpPost]
        [Route("CargarVenta")]
        public IActionResult CargarVenta([FromBody] List<Producto> productos, int pIdUsuario)
        {
            try
            {
                VentaRepository.CargarVenta(productos, pIdUsuario);
                return Ok(new { message = "Venta registrada con exito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}
