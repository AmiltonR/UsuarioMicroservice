using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDbContext.Domain.Models.Entities;

namespace UserDbContext.Domain.Models.DTOs
{
    public class UsuarioInstructorDTO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string GradoAcademico { get; set; }
        public string Perfil { get; set; }
        public string Carnet { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; } 
        public string Direccion { get; set; }
        public int Edad { get; set; }
        public string FechaRegistro { get; set; }
        public virtual List<HabilidadesStringDTO> Habilidades { get; set; }
    }
}
