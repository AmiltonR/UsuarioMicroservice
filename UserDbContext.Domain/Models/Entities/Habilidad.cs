using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.Entities
{
    public class Habilidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string NombreHabilidad { get; set; }
        [Required]
        public string DescripcionHabilidad { get; set; }

       // public ICollection<HabilidadInstructor> HabilidadInstructores { get; set; }
    }
}
