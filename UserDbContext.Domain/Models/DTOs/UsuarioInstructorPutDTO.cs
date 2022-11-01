using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.DTOs
{
    public class UsuarioInstructorPutDTO
    {
        public int Idusuario { get; set; }
        public string Clave { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int Edad { get; set; }
        public int IdGradoAcademico { get; set; }
        public string Perfil { get; set; }

        public List<HabilidadesIdDTO> Habilidades { get; set; }
    }
}
