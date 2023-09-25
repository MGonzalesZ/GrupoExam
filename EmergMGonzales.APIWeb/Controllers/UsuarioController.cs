using EmergMGonzales.Logica.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Globalization;
using EmergMGonzales.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EmergMGonzales.APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService= usuarioService;
        }        

        [HttpGet]
        [Route("listar")]
        public IActionResult ObtenerTodos()
        {
            var Usuarios = _usuarioService.ObtenerTodos();
            return Ok(Usuarios);
        }

        [HttpGet]
        [Route("buscar")]        
        public IActionResult ObtenerPorId(int id)
        {
            var usuario = _usuarioService.ObtenerUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        [Route("agregar")]
        public IActionResult Agregar(string nombre, string contrasenia, string correo)
        {
            var nuevoUsuario = _usuarioService.CrearUsuario(nombre, contrasenia,correo);

            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoUsuario.IdUsuario }, nuevoUsuario);
        }
        

        [HttpPut]
        [Route("actualizar")]
        public IActionResult Actualizar(int id,string nombre, string contrasenia, string correo)
        {
            var usuarioExistente = _usuarioService.ObtenerUsuarioPorId(id);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            usuarioExistente.Nombre = nombre;
            usuarioExistente.Contrasenia = contrasenia;
            usuarioExistente.Correo = correo;


            _usuarioService.Actualizar(usuarioExistente);

            return NoContent();
        }

        [HttpDelete]
        [Route("eliminar")]
        public IActionResult Eliminar(int id)
        {
            var usuario = _usuarioService.ObtenerUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _usuarioService.Eliminar(id);

            return NoContent();
        }

        [HttpPost]
        [Route("LOGIN")]
        public IActionResult Autenticar(string nombreUsuario, string contrasenia)
        {
            var usuario = _usuarioService.VerificarCredenciales(nombreUsuario, contrasenia);

            if (usuario == null)
            {
                return Unauthorized();
            }

            return Ok(usuario);
        }

    }
}
