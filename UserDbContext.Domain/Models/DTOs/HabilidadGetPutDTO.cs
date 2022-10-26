using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.DTOs
{
    public class HabilidadGetPutDTO
    {
        public int Id { get; set; }
        public string NombreHabilidad { get; set; }
        public string DescripcionHabilidad { get; set; }
    }
}
