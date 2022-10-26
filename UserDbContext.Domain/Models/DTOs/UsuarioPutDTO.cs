﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.DTOs
{
    public class UsuarioPutDTO
    {
        public int id { get; set; }
        public string Carnet { get; set; }
        public string Clave { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int Edad { get; set; }
        public int IdRol { get; set; }
    }
}
