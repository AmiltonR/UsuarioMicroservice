using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;
using UserDbContext.Infrastructure;
using Usuarios.API.Utilities;

namespace Usuarios.API.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly UserContext _db;
        private readonly GenerarCarnet _generarCarnet;
        private IMapper _mapper;
        protected ResponseNuevaCuentaDto _response;

        public InstructorRepository(UserContext db, IMapper mapper, GenerarCarnet generarCarnet)
        {
            _db = db;
            _mapper = mapper;
            _generarCarnet = generarCarnet;
            _response = new ResponseNuevaCuentaDto();
        }

        public async Task<int> ActualizarInstructor(UsuarioInstructorPutDTO updateInstructor)
        {
            int response = 0;
            //Para actualizar el rol en la tabla de usuarios
            Usuario usuario = null;
            //Para actualizar la data en la tabla de instructores
            Instructor instructor = null;
            //Para guardar las habilidades en la tabla de habilidadesInstructores
            HabilidadInstructor instructorHabilidad = null;
            using var transaction = _db.Database.BeginTransaction();

            try
            {
                //Verificar que el id del usuario existe
                bool idExiste = await verifyId(updateInstructor.Idusuario);
                //es de rol instructor
                

                if (idExiste)//si existe el usuario
                {
                    int rol = await VerifyRol(updateInstructor.Idusuario);
                    if (rol == 3)//Si el usuario es instructor (idRol = 3)
                    {
                        usuario = new Usuario
                        {
                            Id = updateInstructor.Idusuario,
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
                        _db.Entry(usuario).Property(x => x.NombreUsuario).IsModified = true;
                        _db.Entry(usuario).Property(x => x.ApellidoUsuario).IsModified = true;
                        _db.Entry(usuario).Property(x => x.Correo).IsModified = true;
                        _db.Entry(usuario).Property(x => x.Direccion).IsModified = true;
                        _db.Entry(usuario).Property(x => x.Edad).IsModified = true;
                        _db.Entry(usuario).Property(x => x.Telefono).IsModified = true;
                        await _db.SaveChangesAsync();

                        Instructor? ins = await _db.Instructores.Where(i => i.IdUsuario == updateInstructor.Idusuario).FirstOrDefaultAsync();

                        //instructor = new Instructor
                        //{
                        //    IdUsuario = updateInstructor.Idusuario,
                        //    IdGradoAcademico = updateInstructor.IdGradoAcademico,
                        //    Perfil = updateInstructor.Perfil,
                        //};
                        ins.IdGradoAcademico = updateInstructor.IdGradoAcademico;
                        ins.Perfil = updateInstructor.Perfil;

                        _db.Entry(ins).State = EntityState.Detached;

                        //Attach instructor
                        _db.Attach(ins);
                        _db.Entry(ins).Property(i => i.IdGradoAcademico).IsModified = true;
                        _db.Entry(ins).Property(i => i.Perfil).IsModified = true;
                        await _db.SaveChangesAsync();

                        //Eliminamos las habilidades que tenía para sustituir por las nuevas
                        List<HabilidadInstructor> habilidadesGuardadas = await _db.HabilidadesInstructores.Where(h => h.IdUsuario == updateInstructor.Idusuario).ToListAsync();

                        foreach (var item in habilidadesGuardadas)
                        {
                            //Eliminamos las habilidades que estaban guardadas anteriormente
                            _db.HabilidadesInstructores.Remove(item);
                            await _db.SaveChangesAsync();
                        }

                        //Guardamos las nuevas habilidades
                        foreach (var item in updateInstructor.Habilidades)
                        {
                            instructorHabilidad = new HabilidadInstructor
                            {
                                IdUsuario = updateInstructor.Idusuario,
                                IdHabilidad = item.IdHabilidad,
                            };

                            //Guardamos una nueva habilidad
                            _db.HabilidadesInstructores.Add(instructorHabilidad);
                            await _db.SaveChangesAsync();
                        }

                        //commit o rollback
                        transaction.Commit();
                        response = 1;//cuando la tarea se completa con éxito
                    }
                    else
                    {
                        response = 2;//Cuando el rol es diferente de instructor
                    }
                }
                else
                {
                    response = 3;//El usuario no existe
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<int> CambiarUsuarioAInstructor(InstructorCambioRolInstructor cambioInstructor)
        {
            //Variables
            int response = 0;
            Instructor instructor = null;
            UsuarioRolDTO usuario = null;
            HabilidadInstructor habilidades = null;
            using var transaction = _db.Database.BeginTransaction();

            //Verificar que no exista en la tabla instructores
            Instructor? ins =  await _db.Instructores.Where(i => i.IdUsuario == cambioInstructor.IdUsuario).FirstOrDefaultAsync();

            if (ins == null)//Si ins == null significa que no se encontró el idUsuario en la tabla instructores
            {
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

                    response = 1;//Cuando la tarea se completa con exito
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                response = 2;//Cuando el usuario ya está categorizado como instructor   
            }
            return response;
        }

        public async Task<ResponseNuevaCuentaDto> CreateNuevaCuentaInstructor(InstructorCuentaNuevaPostDTO cuentaInstructor)
        {
            Usuario? usuario = null;
            Instructor instructor = null;
            HabilidadInstructor habilidades = null;
            using var transaction = _db.Database.BeginTransaction();
            int b = 0;

            string carnet = _generarCarnet.Generar(cuentaInstructor.NombreUsuario, cuentaInstructor.ApellidoUsuario, cuentaInstructor.Edad);


            bool ex = await Exists(carnet);

            if (!ex)
            {
                try
                {
                    //Crear objeto usuario
                    usuario = new Usuario
                    {
                        Carnet = carnet,
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

                    b = 1;//Cuando la tarea se completa con exito


                    //Crear objeto  para devolver id
                    int idRespuesta;
                    try
                    {
                        Usuario? usuarioRespuesta = await _db.Usuarios.Where(u => u.Carnet == carnet).FirstOrDefaultAsync();
                        idRespuesta = usuarioRespuesta.Id;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    _response.id = idRespuesta;
                    _response.carnet = carnet;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                b = 2;//Cuando ya existe el carnet
            }

            _response.r = b;
            
            return _response;
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
                b = true;//El usuario existe
                _db.Entry(u).State = EntityState.Detached;
            }

            return b;
        }

        //Verificar que el carnet exista
        public async Task<int> CarnetUsuario(string carnet, int id)
        {
            //Devuelve 1 si se el carnet de parámetro pertenece al id 
            int response= 0;
            Usuario? u = await _db.Usuarios.FirstOrDefaultAsync(u => u.Carnet == carnet);

            //si es diferente de null significa que se encontró el carnet
            if (u != null)
            {
                if (u.Id == id)//se verifica que el carnet pertenezca al usuario en cuestión; sino, el carnet existe pero es de otro usuario
                {
                   response = 1;//El carnet existe y es del usuario
                }
                else
                {
                    response = 2;//El carnet existe pero no es del usuario
                }
                _db.Entry(u).State = EntityState.Detached;
            }

            return response;
        }
    }
}
