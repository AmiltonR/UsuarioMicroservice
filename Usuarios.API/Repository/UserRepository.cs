using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;
using UserDbContext.Infrastructure;
using Usuarios.API.Utilities;

namespace Usuarios.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _db;
        private IMapper _mapper;
        private readonly GenerarCarnet _generarCarnet;

        public UserRepository(UserContext db, IMapper mapper, GenerarCarnet generarCarnet)
        {
            _db = db;
            _mapper = mapper;
            _generarCarnet = generarCarnet;
        }
        //Get absolutly all users
        public async Task<IEnumerable<UsuarioDTO>> GetUsers()
        {
            List<Usuario> usuarios = await _db.Usuarios.Include(u => u.rol).ToListAsync();

            List<UsuarioDTO> usuariosList = transformToListDto(usuarios);
            return usuariosList;
        }

        //List all active users 
        public async Task<IEnumerable<UsuarioDTO>> GetActiveUsers()
        {
            List<Usuario> usuarios = await _db.Usuarios.Where(x => x.estado==1).Include(u => u.rol).ToListAsync();

            List<UsuarioDTO> usuariosList = transformToListDto(usuarios);
            return usuariosList;
        }

        //List all inactive users 
        public async Task<IEnumerable<UsuarioDTO>> GetInactiveUsers()
        {
            List<Usuario> usuarios = await _db.Usuarios.Where(x => x.estado == 0).Include(u => u.rol).ToListAsync();
            List<UsuarioDTO> usuariosList = transformToListDto(usuarios);
            return usuariosList;
        }

        //Get users rol = 1 and rol = 2
        public async Task<IEnumerable<UsuarioDTO>> GetUsersRolStBib()
        {
            List<Usuario>? usuarios = await _db.Usuarios.Where(x => x.IdRol != 3 && x.estado == 1).Include(u => u.rol).ToListAsync();
            List<UsuarioDTO> usuariosList = transformToListDto(usuarios);
            return usuariosList;
        }

        //Get students
        //Falta colocarlo en el controlador
        public async Task<IEnumerable<UsuarioDTO>> GetStudents()
        {
            List<Usuario>? usuarios = await _db.Usuarios.Where(x => x.IdRol == 1 && x.estado == 1).Include(u => u.rol).ToListAsync();
            List<UsuarioDTO> usuariosList = transformToListDto(usuarios);
            return usuariosList;
        }

        //Get User By ID
        public async Task<UsuarioDTO> GetUserById(int UserId)
        {
            Usuario? usuario = await _db.Usuarios.Where(x => x.Id == UserId).Include(u => u.rol).FirstOrDefaultAsync();
            UsuarioDTO dto = null;
            if (usuario!=null)
            {
                dto = transformToDto(usuario);
            }
            return dto;
        }

        //Get User By Carnet
        public async Task<UsuarioDTO> GetUserByCarnet(string UserCarnet)
        {
            Usuario? usuario = await _db.Usuarios.Where(x => x.Carnet == UserCarnet).Include(u => u.rol).FirstOrDefaultAsync();
            UsuarioDTO dto = null;
            if (usuario != null)
            {
                dto = transformToDto(usuario);
            }
            return dto;
        }

        //Get User By Nombre
        public async Task<UsuarioDTO> GetUserByNombre(string Nombre)
        {
            Usuario? usuario = await _db.Usuarios.Where(x => x.NombreUsuario == Nombre).Include(u => u.rol).FirstOrDefaultAsync();
            UsuarioDTO dto = null;
            if (usuario != null)
            {
                dto = transformToDto(usuario);
            }
            return dto;
        }

        //Get instructores
        public async Task<IEnumerable<UsuarioInstructorDTO>> GetInstructores()
        {
            //Traer instructores activos
            List<Instructor>? instructores = await _db.Instructores.Include(u=> u.Usuario)
                .Include(u => u.Grado).Where(u => u.Usuario.estado == 1).OrderBy(t => t.Usuario.NombreUsuario).ToListAsync();

            //Variables de retorno
            List<UsuarioInstructorDTO> listaInstructores = new List<UsuarioInstructorDTO>();
            UsuarioInstructorDTO usuarioInstructorDTO = null;

            foreach (var item in instructores)
            {
                List<HabilidadInstructor> habilidadesInstructor = await _db.HabilidadesInstructores
                                                                 .Where(i => i.IdUsuario == item.IdUsuario).Include(h => h.Habilidad).ToListAsync();

                //variables para tratamiento de habilidades por instructor
                List<HabilidadesStringDTO> habilidadesString = new List<HabilidadesStringDTO>();
                HabilidadesStringDTO hs = null;

                //Recorre cada habilidad por instructor y la agrega a la lista
                foreach (var i in habilidadesInstructor)
                {
                    hs = new HabilidadesStringDTO
                    {
                        Habilidad = i.Habilidad.NombreHabilidad
                    };
                    habilidadesString.Add(hs);
                }
                
                usuarioInstructorDTO = new UsuarioInstructorDTO
                {
                    Id = item.Id,
                    IdUsuario = item.IdUsuario,
                    GradoAcademico = item.Grado.NombreGrado,
                    Perfil = item.Perfil,
                    Carnet = item.Usuario.Carnet,
                    NombreUsuario = item.Usuario.NombreUsuario,
                    ApellidoUsuario = item.Usuario.ApellidoUsuario,
                    Correo = item.Usuario.Correo,
                    Telefono = item.Usuario.Telefono,
                    Direccion = item.Usuario.Direccion,
                    Edad = item.Usuario.Edad,
                    FechaRegistro = item.Usuario.FechaRegistro.ToShortDateString(),
                    Habilidades = habilidadesString
                };
                listaInstructores.Add(usuarioInstructorDTO);
            }
            return listaInstructores;
        }
        //Get Instructor by Id
        public async Task<UsuarioInstructorDTO> GetInstructor(int id)
        {
            UsuarioInstructorDTO? usuarioInstructorDTO = null;
            Instructor? instructor = await _db.Instructores.Include(u => u.Usuario)
               .Include(u => u.Grado).FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (instructor!=null)
            {
                List<HabilidadInstructor>? habilidadesInstructor = await _db.HabilidadesInstructores.Include(h => h.Habilidad)
                                                 .Where(i => i.IdUsuario == instructor.IdUsuario).ToListAsync();

                
                List<HabilidadesStringDTO> habilidadesString = new List<HabilidadesStringDTO>();
                HabilidadesStringDTO hs = null;

                foreach (var i in habilidadesInstructor)
                {
                    hs = new HabilidadesStringDTO
                    {
                        Habilidad = i.Habilidad.NombreHabilidad
                    };
                    habilidadesString.Add(hs);
                }

                usuarioInstructorDTO = new UsuarioInstructorDTO
                {
                    Id = instructor.Id,
                    IdUsuario = instructor.IdUsuario,
                    GradoAcademico = instructor.Grado.NombreGrado,
                    Perfil = instructor.Perfil,
                    Carnet = instructor.Usuario.Carnet,
                    NombreUsuario = instructor.Usuario.NombreUsuario,
                    ApellidoUsuario = instructor.Usuario.ApellidoUsuario,
                    Correo = instructor.Usuario.Correo,
                    Telefono = instructor.Usuario.Telefono,
                    Direccion = instructor.Usuario.Direccion,
                    Edad = instructor.Usuario.Edad,
                    FechaRegistro = instructor.Usuario.FechaRegistro.ToShortDateString(),
                    Habilidades = habilidadesString
                };
            }

            return usuarioInstructorDTO;
        }

        public async Task<IEnumerable<UsuarioInstructorDTO>> GetInstructoresByHabilidad(int idHabilidad)
        {
            //Filtramos la entidad Habilidades Instructor donde aparezca la habilidad que deseamos
            List<HabilidadInstructor> habilidades = await _db.HabilidadesInstructores.Where(h => h.IdHabilidad == idHabilidad).ToListAsync();

            List<Instructor> instructores = new List<Instructor>();
            Instructor? instructor = null;

            //Recorremos la lista para llenar otra lista con el id de usuario
            //que tra la lista de habilidades
            foreach (var item in habilidades)
            {
                instructor = await _db.Instructores.Where(i => i.IdUsuario == item.IdUsuario).Include(i => i.Usuario)
                    .Include(i => i.Grado).FirstOrDefaultAsync();
                instructores.Add(instructor);
            }

            //preparamos la lista de retorno
            List<UsuarioInstructorDTO> usuarioInstructorList = new List<UsuarioInstructorDTO>();
            UsuarioInstructorDTO usuarioInstructor = null;

            //recorremos la lista de instructores para llenar nuestra lista de retorno
            foreach (var item in instructores)
            {
              
                usuarioInstructor = new UsuarioInstructorDTO
                {
                    Id = item.Id,
                    IdUsuario = item.IdUsuario,
                    Carnet = item.Usuario.Carnet,
                    NombreUsuario = item.Usuario.NombreUsuario,
                    ApellidoUsuario = item.Usuario.ApellidoUsuario,
                    Correo = item.Usuario.Correo,
                    Telefono = item.Usuario.Telefono,
                    Direccion = item.Usuario.Direccion,
                    Edad = item.Usuario.Edad,
                    GradoAcademico = item.Grado.NombreGrado,
                    Perfil = item.Perfil,
                    FechaRegistro = item.Usuario.FechaRegistro.ToShortDateString(),        
                };

                usuarioInstructorList.Add(usuarioInstructor);
            }

            return usuarioInstructorList;
        }

        public async Task<bool> CreateUser(UsuarioPostDTO usuariopost)
        {
            bool flag = false;
            try
            {
                Usuario usuario = _mapper.Map<UsuarioPostDTO, Usuario>(usuariopost);
                //Generando carnet
                usuario.Carnet = _generarCarnet.Generar(usuariopost.NombreUsuario, usuariopost.ApellidoUsuario, usuariopost.Edad);
                _db.Usuarios.Add(usuario);
                await _db.SaveChangesAsync();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }


        //Update User. All properties
        public async Task<UsuarioPutDTO> UpdateUser(UsuarioPutDTO usuarioput)
        {
            Usuario usuario = _mapper.Map<UsuarioPutDTO, Usuario>(usuarioput);
            _db.Usuarios.Attach(usuario);
            _db.Entry(usuario).Property(x => x.Carnet).IsModified = true;
            _db.Entry(usuario).Property(x => x.NombreUsuario).IsModified = true;
            _db.Entry(usuario).Property(x => x.ApellidoUsuario).IsModified = true;
            _db.Entry(usuario).Property(x => x.Correo).IsModified = true;
            _db.Entry(usuario).Property(x => x.Direccion).IsModified = true;
            _db.Entry(usuario).Property(x => x.Edad).IsModified = true;
            _db.Entry(usuario).Property(x => x.Telefono).IsModified = true;
            _db.Entry(usuario).Property(x => x.IdRol).IsModified = true;
            // _db.Usuarios.Update(usuario);
            await _db.SaveChangesAsync();
            return usuarioput;
        }

        //Verify carnet Existence
        public async Task<bool> Exists(string carnet)
        {
            bool exists = await _db.Usuarios.AnyAsync(c => c.Carnet == carnet);
            return exists;
        }

        //Set User inactive or active
        public async Task<bool> ChangeState(int opt, int Id)
        {

            bool u = await verifyId(Id);

            bool flag = false;
            int _estado = 0;

            if (u)
            {
                if (opt == 1)
                {
                    _estado = 1;
                }
                else if (opt == 0)
                {
                    _estado = 0;
                }
                else
                {
                    return flag;
                }

                UsuarioDeleteDTO userDTO = new UsuarioDeleteDTO
                {
                    Id = Id,
                    estado = _estado
                };

                Usuario user = _mapper.Map<UsuarioDeleteDTO, Usuario>(userDTO);



                _db.Usuarios.Attach(user);
                _db.Entry(user).Property(x => x.estado).IsModified = true;
                await _db.SaveChangesAsync();
                flag = true;
            }

            return flag;
        }

        //Cambiar a rol bibliotecario
        public async Task<bool> ChangeRol(UsuarioRolDTO usuarioRol)
        {
            bool b = false;

            bool verifiId = await verifyId(usuarioRol.Id);

            if (verifiId)
            {
                Usuario u = _mapper.Map<UsuarioRolDTO, Usuario>(usuarioRol);
                _db.Usuarios.Attach(u);
                _db.Entry(u).Property(u => u.IdRol).IsModified = true;
                await _db.SaveChangesAsync();
                b = true;
            }
            return b;
        }

        //Utilities

        public List<UsuarioDTO> transformToListDto(List<Usuario> usuarios)
        {
            List<UsuarioDTO> usuariosList = new List<UsuarioDTO>();
            UsuarioDTO usuarioIterar = null;
            foreach (var item in usuarios)
            {
                usuarioIterar = new UsuarioDTO
                {
                    Id = item.Id,
                    Carnet = item.Carnet,
                    NombreUsuario = item.NombreUsuario,
                    ApellidoUsuario = item.ApellidoUsuario,
                    Correo = item.Correo,
                    Telefono = item.Telefono,
                    Direccion = item.Direccion,
                    Edad = item.Edad,
                    FechaRegistro = item.FechaRegistro.ToShortDateString(),
                    IdRol = item.IdRol,
                    NombreRol = item.rol.NombreRol
                };
                usuariosList.Add(usuarioIterar);
            }
            return usuariosList;
        }

        public UsuarioDTO transformToDto(Usuario usuario)
        {
            UsuarioDTO dto = _mapper.Map<Usuario, UsuarioDTO>(usuario);
            dto.NombreRol = usuario.rol.NombreRol;
            dto.FechaRegistro = usuario.FechaRegistro.ToShortDateString();
            return dto;
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

       
    }
}
