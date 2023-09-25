using EmergMGonzales.Logica.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Globalization;
using EmergMGonzales.Entidades;

namespace EmergMGonzales.APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService= usuarioService;
        }        

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var Usuarios = _usuarioService.ObtenerTodos();
            return Ok(Usuarios);
        }

        [HttpGet("{id}")]
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
        public IActionResult Agregar(string nombre, string contrasenia, string correo)
        {
            //Usuario nuevoModelo = new Contacto()
            //{
            //    Nombre = modelo.Nombre,
            //    Telefono = modelo.Telefono,
            //    FechaNacimiento = DateTime.ParseExact(modelo.FechaNacimiento, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("es-PE"))
            //};
            var nuevoUsuario = _usuarioService.CrearUsuario(nombre, contrasenia,correo);

            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoUsuario.IdUsuario }, nuevoUsuario);
        }

        //[HttpPost]
        //public IActionResult Agregar([FromBody] Usuario modelo)
        //{
        //    //Usuario nuevoModelo = new Contacto()
        //    //{
        //    //    Nombre = modelo.Nombre,
        //    //    Telefono = modelo.Telefono,
        //    //    FechaNacimiento = DateTime.ParseExact(modelo.FechaNacimiento, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("es-PE"))
        //    //};
        //    var nuevoUsuario = _usuarioService.CrearUsuario(modelo);

        //    return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoUsuario.IdUsuario },nuevoUsuario);
        //}

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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

        [HttpPost("autenticar")]
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
