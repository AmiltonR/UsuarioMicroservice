using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserDbContext.Domain.Models.Entities
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Carnet { get; set; }
        [Required]
        public string Clave { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string ApellidoUsuario { get; set; }
        [Required]
        public string Correo { get; set; }
        public string Telefono { get; set; } = String.Empty;
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; }
        [Required]
        [ForeignKey("rol")]
        public int IdRol { get; set; }
        [Required]
        public int estado { get; set; }
        [ForeignKey("IdRol")]
        public virtual Rol rol { get; set; }

        public virtual ICollection<HabilidadInstructor> HabilidadInstructores { get; set; }
        public virtual ICollection<Instructor> Instructores { get; set; }
    }
}
