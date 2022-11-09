using CoderHouse_SistemaGestion.Models;
using CoderHouse_SistemaGestion.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoderHouse_SistemaGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {


        [HttpGet]
        [Route("GetUsuario")]
        public ActionResult GetUsuario([FromQuery] string pNombreUsuario)
        {
            var result = UsuarioRepository.GetUsuario(pNombreUsuario);
            return Ok(result);
        }

        [HttpGet]
        [Route("InicioSesion")]
        public ActionResult InicioSesion([FromQuery] string pNombreUsuario, string pContrasena)
        {
            var result = UsuarioRepository.InicioSesion(pNombreUsuario, pContrasena);
            return Ok(result);
        }

        [HttpPost]
        [Route("AgregarUsuario")]
        public IActionResult AgregarUsuario([FromBody] Usuario usuario) 
        {
            try
            {
                UsuarioRepository.AgregarUsuario(usuario);
                return Ok(new { message = "Usuario registrado con exito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpPut]
        [Route("ModificarUsuario")]
        public IActionResult ModificarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                UsuarioRepository.ModificarUsuario(usuario);
                return Ok(new { message = "Usuario modificado con exito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("EliminarUsuario")]
        public IActionResult EliminarUsuario([FromBody] int IdUsuario)
        {
            try
            {
                UsuarioRepository.EliminarUsuario(IdUsuario);
                return Ok(new { message = "Usuario eliminado con exito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
