using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDbContext.Domain.Models.Entities;

namespace UserDbContext.Infrastructure
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Instructor> Instructores { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<Habilidad> Habilidades { get; set; }
        public DbSet<HabilidadInstructor> HabilidadesInstructores { get; set; }
    }
}
