using UserDbContext.Domain.Models.DTOs;

namespace Usuarios.API.Repository
{
    public interface IInstructorRepository
    {
        Task<bool> CreateNuevaCuentaInstructor(InstructorCuentaNuevaPostDTO cuentaInstructor);
        Task<int> CambiarUsuarioAInstructor(InstructorCambioRolInstructor cambioInstructor);
        Task<bool> ActualizarInstructor(UsuarioInstructorPutDTO updateInstructor);
        Task<bool> Exists(string carnet);
    }
}
