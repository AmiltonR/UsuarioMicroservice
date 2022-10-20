using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;
using UserDbContext.Infrastructure;

namespace Usuarios.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _db;
        private IMapper _mapper;

        public UserRepository(UserContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<UsuarioPostDTO> CreateUser(UsuarioPostDTO usuariopost)
        {
            Usuario usuario = _mapper.Map<UsuarioPostDTO, Usuario>(usuariopost);
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            return _mapper.Map<Usuario, UsuarioPostDTO>(usuario);
        }

        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                Usuario? usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.Id == userId);
                if (usuario == null)
                {
                    return false;
                }

                _db.Usuarios.Remove(usuario);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //List all users
        public async Task<IEnumerable<UsuarioDTO>> GetUsers()
        {
            List<Usuario> usuariosList = await _db.Usuarios.ToListAsync();
            return _mapper.Map<List<UsuarioDTO>>(usuariosList);
        }


        //Get User By ID
        public async Task<UsuarioDTO> GetUserById(int UserId)
        {
            Usuario? usuario = await _db.Usuarios.Where(x => x.Id == UserId).FirstOrDefaultAsync();
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        //Get User By Carnet
        public async Task<UsuarioDTO> GetUserByCarnet(string UserCarnet)
        {
            Usuario? usuario = await _db.Usuarios.Where(x => x.Carnet == UserCarnet).FirstOrDefaultAsync();
            return _mapper.Map<UsuarioDTO>(usuario);
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

        //Set User inactive state
        public async Task<UsuarioDeleteDTO> SetInactive(int Id)
        {
            UsuarioDeleteDTO userDTO = new UsuarioDeleteDTO
            {
                Id = Id,
                estado = 0
            };
            Usuario user = _mapper.Map<UsuarioDeleteDTO, Usuario>(userDTO);
            _db.Usuarios.Attach(user);
            _db.Entry(user).Property(x => x.estado).IsModified = true;
            _db.SaveChanges();
            return userDTO;
        }

        //Set User active state
        public async Task<UsuarioDeleteDTO> SetActive(int Id)
        {
            UsuarioDeleteDTO userDTO = new UsuarioDeleteDTO
            {
                Id = Id,
                estado = 1
            };
            Usuario user = _mapper.Map<UsuarioDeleteDTO, Usuario>(userDTO);
            _db.Usuarios.Attach(user);
            _db.Entry(user).Property(x => x.estado).IsModified = true;
            _db.SaveChanges();
            return userDTO;
        }
    }
}
