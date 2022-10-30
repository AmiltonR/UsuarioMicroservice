using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;

namespace Usuarios.API.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UsuarioDTO>> GetUsers();
        Task<IEnumerable<UsuarioDTO>> GetActiveUsers();

        Task<IEnumerable<UsuarioDTO>> GetInactiveUsers();
        Task<IEnumerable<UsuarioDTO>> GetUsersRolStBib();
        Task<IEnumerable<UsuarioInstructorDTO>> GetInstructores();
        Task<IEnumerable<UsuarioInstructorDTO>> GetInstructoresByHabilidad(int idHabilidad);
        Task<UsuarioInstructorDTO> GetInstructor(int id);
        Task<UsuarioDTO> GetUserById(int UserId);
        Task<UsuarioDTO> GetUserByNombre(string Nombre);
        Task<UsuarioDTO> GetUserByCarnet(string UserCarnet);
        Task<bool> CreateUser(UsuarioPostDTO usuarioPost);
        Task<UsuarioPutDTO> UpdateUser(UsuarioPutDTO usuario);
        Task<bool> ChangeState(int opt, int Id);
        Task<bool> Exists(string carnet);
        Task<bool> ChangeRol(UsuarioRolDTO usuarioRol);
            
    }
}
