using System;
using System.Collections.Generic;

namespace EmergMGonzales.Entidades
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Contrasenia { get; set; }
        public string? Correo { get; set; }
    }
}
