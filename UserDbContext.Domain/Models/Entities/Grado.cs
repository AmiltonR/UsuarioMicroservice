using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.Entities
{
    public class Grado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string NombreGrado { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public ICollection<Instructor>  Instructores { get; set; }
    }
}
