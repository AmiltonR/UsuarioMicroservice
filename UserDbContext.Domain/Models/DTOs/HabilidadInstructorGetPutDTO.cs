using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDbContext.Domain.Models.Entities;

namespace UserDbContext.Domain.Models.DTOs
{
    public class HabilidadInstructorGetPutDTO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdHabilidad { get; set; }
    }
}
