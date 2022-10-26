using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.Entities
{
    public class Instructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [Required]
        [ForeignKey("Grado")]
        public int IdGradoAcademico { get; set; }
        [Required]
        public string Perfil { get; set; }

        //Llaves foráneas
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }
        [ForeignKey("IdGradoAcademico")]
        public virtual Grado Grado { get; set; }

    }
}
