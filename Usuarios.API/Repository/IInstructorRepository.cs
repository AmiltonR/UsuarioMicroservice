using UserDbContext.Domain.Models.DTOs;

namespace Usuarios.API.Repository
{
    public interface IInstructorRepository
    {
        Task<bool> CreateNuevaCuentaInstructor(InstructorCuentaNuevaPostDTO cuentaInstructor);
        Task<bool> CambiarUsuarioAInstructor();//falta crear DTO

        Task<bool> Exists(string carnet);
    }
}
