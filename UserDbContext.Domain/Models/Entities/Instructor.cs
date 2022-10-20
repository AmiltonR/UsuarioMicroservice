using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdGradoAcademico { get; set; }
        public string Perfil { get; set; }
    }
}
