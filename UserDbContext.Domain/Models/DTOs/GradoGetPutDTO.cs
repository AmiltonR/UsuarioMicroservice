using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.DTOs
{
    public class GradoGetPutDTO
    {
        public int Id { get; set; }
        public string NombreGrado { get; set; }
        public string Descripcion { get; set; }
    }
}
