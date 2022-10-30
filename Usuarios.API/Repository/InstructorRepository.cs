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

        public async Task<bool> ActualizarInstructor(UsuarioInstructorPutDTO updateInstructor)
        {
            bool b = false;
            Usuario usuario = null;
            Instructor instructor = null;
            HabilidadInstructor instructorHabilidad = null;
            using var transaction = _db.Database.BeginTransaction();

            try
            {
               string carnet = updateInstructor.Carnet;
                //el carnet le pertenezca al usuario actual; sino, ya existe carnet
                bool carnetusuario = await CarnetUsuario(carnet, updateInstructor.Idusuario);
                //Existe el id
                bool existe = await verifyId(updateInstructor.Idusuario);
                int rol = await VerifyRol(updateInstructor.Idusuario);

                //Si no existe el carnet y el rol es instructor
                if (existe && rol == 3 && carnetusuario)
                {
                    usuario = new Usuario
                    {
                        Id = updateInstructor.Idusuario,
                        Carnet = updateInstructor.Carnet,
                        Clave = updateInstructor.Clave,
                        NombreUsuario = updateInstructor.NombreUsuario,
                        ApellidoUsuario = updateInstructor.ApellidoUsuario,
                        Correo = updateInstructor.Correo,
                        Telefono = updateInstructor.Telefono,
                        Direccion = updateInstructor.Direccion,
                        Edad = updateInstructor.Edad,
                    };

                    //save usuario
                    _db.Usuarios.Attach(usuario);
                    _db.Entry(usuario).Property(x => x.Carnet).IsModified = true;
                    _db.Entry(usuario).Property(x => x.NombreUsuario).IsModified = true;
                    _db.Entry(usuario).Property(x => x.ApellidoUsuario).IsModified = true;
                    _db.Entry(usuario).Property(x => x.Correo).IsModified = true;
                    _db.Entry(usuario).Property(x => x.Direccion).IsModified = true;
                    _db.Entry(usuario).Property(x => x.Edad).IsModified = true;
                    _db.Entry(usuario).Property(x => x.Telefono).IsModified = true;
                    await _db.SaveChangesAsync();

                    instructor = new Instructor
                    {
                        IdUsuario = updateInstructor.Idusuario,
                        IdGradoAcademico = updateInstructor.IdGradoAcademico,
                        Perfil = updateInstructor.Perfil,
                    };

                    //save instructor
                    _db.Attach(instructor);
                    _db.Entry(instructor).Property(i => i.IdGradoAcademico).IsModified = true;
                    _db.Entry(instructor).Property(i => i.Perfil).IsModified = true;
                    await _db.SaveChangesAsync();

                    foreach (var item in updateInstructor.Habilidades)
                    {
                        instructorHabilidad = new HabilidadInstructor
                        {
                            IdUsuario = updateInstructor.Idusuario,
                            IdHabilidad = item.IdHabilidad,
                        };
                        _db.HabilidadesInstructores.Add(instructorHabilidad);
                        await _db.SaveChangesAsync();
                    }

                    //commit o rollback
                    transaction.Commit();
                    b = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return b;
        }

        public async Task<bool> CambiarUsuarioAInstructor(InstructorCambioRolInstructor cambioInstructor)
        {
            //Variables
            bool b = false;
            Instructor instructor = null;
            UsuarioRolDTO usuario = null;
            HabilidadInstructor habilidades = null;
            using var transaction = _db.Database.BeginTransaction();

            try
            {
                //Guardar datos en Instructor
                instructor = new Instructor
                {
                    IdUsuario = cambioInstructor.IdUsuario,
                    IdGradoAcademico = cambioInstructor.IdGradoAcademico,
                    Perfil = cambioInstructor.Perfil
                };

                //Guardando en base de datos
                _db.Instructores.Add(instructor);
                await _db.SaveChangesAsync();

                //Modificando el rol en la cuenta de usuario
                usuario = new UsuarioRolDTO
                {
                    Id = cambioInstructor.IdUsuario,
                    IdRol = 3
                };

                Usuario u = _mapper.Map<UsuarioRolDTO, Usuario>(usuario);
                _db.Usuarios.Attach(u);
                _db.Entry(u).Property(u => u.IdRol).IsModified = true;
                await _db.SaveChangesAsync();

                //Guardar las Habilidades
                foreach (var item in cambioInstructor.Habilidades)
                {
                    habilidades = new HabilidadInstructor
                    {
                        IdUsuario = usuario.Id,
                        IdHabilidad = item.IdHabilidad
                    };
                    _db.HabilidadesInstructores.Add(habilidades);
                    await _db.SaveChangesAsync();
                }

                transaction.Commit();

                b = true;
            }
            catch (Exception)
            {
                throw;
            }

            return b;
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

        //This class
        public async Task<int> VerifyRol(int id)
        {
            Usuario? u = await _db.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            int rol = u.IdRol;
            if (u != null)
            {
                _db.Entry(u).State = EntityState.Detached;
            }
            return rol;
        }

        public async Task<bool> verifyId(int Id)
        {
            bool b = false;
            Usuario? u = await _db.Usuarios.FirstOrDefaultAsync(u => u.Id == Id);

            if (u != null)
            {
                b = true;
                _db.Entry(u).State = EntityState.Detached;
            }

            return b;
        }

        //Verificar que el carnet exista
        public async Task<bool> CarnetUsuario(string carnet, int id)
        {
            bool b = false;
            Usuario? u = await _db.Usuarios.FirstOrDefaultAsync(u => u.Carnet == carnet);

            if (u != null)
            {
                if (u.Id == id)
                {
                    b = true;
                }
                _db.Entry(u).State = EntityState.Detached;
            }

            return b;
        }
    }
}
