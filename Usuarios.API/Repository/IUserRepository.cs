using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;

namespace Usuarios.API.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UsuarioDTO>> GetUsers();
        Task<UsuarioDTO> GetUserById(int UserId);
        Task<UsuarioDTO> GetUserByCarnet(string UserCarnet);
        Task<UsuarioPostDTO> CreateUser(UsuarioPostDTO usuarioPost);
        Task<UsuarioPutDTO> UpdateUser(UsuarioPutDTO usuario);
        Task<UsuarioDeleteDTO> SetInactive(int Id);
        Task<UsuarioDeleteDTO> SetActive(int Id);
        Task<bool> Exists(string carnet);
            
    }
}
