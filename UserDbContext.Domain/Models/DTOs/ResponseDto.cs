using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.DTOs
{
    public class ResponseDto
    {
        public bool Success { get; set; } = false;
        public object Result { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
