using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDbContext.Domain.Models.Entities;

namespace UserDbContext.Domain.Models.DTOs
{
    public class InstructorCuentaNuevaPostDTO
    {
        public string Perfil { get; set; }
        public string Carnet { get; set; }
        public string Clave { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int Edad { get; set; }
        public int IdGradoAcademico { get; set; }
        public List<HabilidadesIdDTO> Habilidades { get; set; }
        public DateTime FechaRegistro { get; set; }
        //Id, IdRol, IdUsuario, estado serán definidos en tiempo de ejecución
    }
}
