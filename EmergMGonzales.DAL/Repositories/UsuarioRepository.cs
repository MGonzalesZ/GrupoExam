using EmergMGonzales.DAL.DataContext;
using EmergMGonzales.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace EmergMGonzales.DAL.Repositories
{
    public class UsuarioRepository 
    {        
        private readonly EmergGrupalContext _dbContext;
        public UsuarioRepository(EmergGrupalContext context)
        {
            _dbContext= context;
        }
        public void Actualizar(Usuario modelo)
        {
            modelo.Contrasenia = BCrypt.Net.BCrypt.HashPassword(modelo.Contrasenia);
            _dbContext.Entry(modelo).State= EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var usuario = _dbContext.Usuarios.FirstOrDefault(p => p.IdUsuario == id);
            if (usuario != null)
            {
                _dbContext.Usuarios.Remove(usuario);
                _dbContext.SaveChanges();
            }
        }

        public Usuario Agregar(string nombre, string contrasenia, string correo)
        {
            var modelo = new Usuario()
            {
                Nombre = nombre,
                Contrasenia = BCrypt.Net.BCrypt.HashPassword(contrasenia),
                Correo = correo
            };
            //modelo.Contrasenia = BCrypt.Net.BCrypt.HashPassword(modelo.Contrasenia);
            _dbContext.Usuarios.Add(modelo);
            _dbContext.SaveChanges();
            return modelo;
        }

        public List<Usuario> ObtenerTodos()
        {
            //IQueryable<Usuario> queryUsuarioSQL = _dbContext.Usuarios;

            return _dbContext.Usuarios.ToList();
        }
        public Usuario ObtenerPorId(int id)
        {
            return _dbContext.Usuarios.FirstOrDefault(p => p.IdUsuario == id);
        }

        public bool VerificarCredenciales(string nombreUsuario, string contraseña)
        {
            var usuario = _dbContext.Usuarios.FirstOrDefault(u => u.Nombre == nombreUsuario);

            if (usuario == null)
            {
                return false; // Usuario no encontrado
            }

            // Verifica la contraseña utilizando BCrypt
            return BCrypt.Net.BCrypt.Verify(contraseña, usuario.Contrasenia);
        }
    }
}
