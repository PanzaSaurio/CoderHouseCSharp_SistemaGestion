using CoderHouse_SistemaGestion.Models;
using CoderHouse_SistemaGestion.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoderHouse_SistemaGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        [Route("GetProducto")]
        public ActionResult TraerProductos([FromQuery] int pIdUsuario)
        {
            var result = ProductoRepository.TraerProductos(pIdUsuario);
            return Ok(result);
        }

        [HttpPost]
        [Route("AgregarProducto")]
        public IActionResult AgregarProducto([FromBody] Producto producto)
        {
            try
            {
                ProductoRepository.AgregarProducto(producto);
                return Ok(new { message = "Producto registrado con exito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        [Route("ModificarProducto")]
        public IActionResult ModificarProducto([FromBody] Producto producto)
        {
            try
            {
                ProductoRepository.ModificarProducto(producto);
                return Ok(new { message = "Producto modificado con exito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("EliminarProducto")]
        public IActionResult EliminarProducto([FromBody] int Idproducto)
        {
            try
            {
                ProductoRepository.EliminarProducto(Idproducto);
                return Ok(new { message = "Producto eliminado con exito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
