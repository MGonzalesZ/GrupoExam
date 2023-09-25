using EmergMGonzales.DAL.Repositories;
using EmergMGonzales.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergMGonzales.Logica.Service
{
    public class UsuarioService 
    {
        private UsuarioRepository _usuarioRepository;
        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public void Actualizar(Usuario modelo)
        {
            _usuarioRepository.Actualizar(modelo);
        }

        public void Eliminar(int id)
        {
            _usuarioRepository.Eliminar(id);
        }

        public Usuario CrearUsuario(string nombre, string contrasenia, string correo)
        {
            return _usuarioRepository.Agregar(nombre, contrasenia,correo);
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            return _usuarioRepository.ObtenerPorId(id);
        }


        public List<Usuario> ObtenerTodos()
        {
            return _usuarioRepository.ObtenerTodos();
        }

        public bool VerificarCredenciales(string nombreUsuario, string contraseña)
        {
            return _usuarioRepository.VerificarCredenciales(nombreUsuario, contraseña);
        }

    }
}
