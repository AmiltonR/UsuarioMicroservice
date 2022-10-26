using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.DTOs
{
    public class InstructorCambioRolInstructor
    {
        public int IdUsuario { get; set; }
        public int IdGradoAcademico { get; set; }
        public string Perfil { get; set; }
        public List<HabilidadesIdDTO> Habilidades { get; set; }
    }
}
