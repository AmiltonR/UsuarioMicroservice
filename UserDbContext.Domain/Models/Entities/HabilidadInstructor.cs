using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace UserDbContext.Domain.Models.Entities
{
    public class HabilidadInstructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [Required]
        [ForeignKey("Habilidad")]
        public int IdHabilidad { get; set; }

        //Llaves foráneas
        [ForeignKey("IdUsuario")]
        [NotMapped]
        public virtual Usuario Usuario { get; set; }
        [ForeignKey("IdHabilidad")]
        public virtual Habilidad Habilidad { get; set; }
    }
}
