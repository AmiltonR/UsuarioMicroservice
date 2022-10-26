using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;
using UserDbContext.Infrastructure;

namespace Usuarios.API.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly UserContext _db;
        private IMapper _mapper;

        public InstructorRepository(UserContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public Task<bool> CambiarUsuarioAInstructor()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateNuevaCuentaInstructor(InstructorCuentaNuevaPostDTO cuentaInstructor)
        {
            Usuario? usuario = null;
            Instructor instructor = null;
            HabilidadInstructor habilidades = null;
            using var transaction = _db.Database.BeginTransaction();
            bool b = false;

            bool ex = await Exists(cuentaInstructor.Carnet);

            if (!ex)
            {
                try
                {
                    //Crear objeto usuario
                    usuario = new Usuario
                    {
                        Carnet = cuentaInstructor.Carnet,
                        Clave = cuentaInstructor.Clave,
                        NombreUsuario = cuentaInstructor.NombreUsuario,
                        ApellidoUsuario = cuentaInstructor.ApellidoUsuario,
                        Correo = cuentaInstructor.Correo,
                        Telefono = cuentaInstructor.Telefono,
                        Direccion = cuentaInstructor.Direccion,
                        Edad = cuentaInstructor.Edad,
                        FechaRegistro = cuentaInstructor.FechaRegistro,
                        IdRol = 3,
                        estado = 1,
                    };

                    //Guardar objeto usuario
                    _db.Usuarios.Add(usuario);
                    _db.SaveChanges();

                    //Recuperar Id
                    usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.Carnet == usuario.Carnet);

                    //Crear Objeto instructor
                    instructor = new Instructor
                    {
                        IdUsuario = usuario.Id,
                        IdGradoAcademico = cuentaInstructor.IdGradoAcademico,
                        Perfil = cuentaInstructor.Perfil,
                    };

                    //Guardar objeto instructor
                    _db.Instructores.Add(instructor);
                    _db.SaveChanges();


                    foreach (var item in cuentaInstructor.Habilidades)
                    {
                        habilidades = new HabilidadInstructor
                        {
                            IdUsuario = usuario.Id,
                            IdHabilidad = item.IdHabilidad
                        };
                        _db.HabilidadesInstructores.Add(habilidades);
                        _db.SaveChanges();
                    }

                    //Commit transacction or rollback
                    transaction.Commit();

                    b = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
            return b;
        }
        public async Task<bool> Exists(string carnet)
        {
            bool exists = await _db.Usuarios.AnyAsync(c => c.Carnet == carnet);
            return exists;
        }
    }
}
