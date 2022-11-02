using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.DTOs
{
    public class ResponseNuevaCuentaDto
    {
        public int r { get; set; }
        public int id { get; set; } = 0;
        public string carnet { get; set; }
    }
}
