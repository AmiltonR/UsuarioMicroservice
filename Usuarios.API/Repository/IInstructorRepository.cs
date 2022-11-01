using UserDbContext.Domain.Models.DTOs;

namespace Usuarios.API.Repository
{
    public interface IInstructorRepository
    {
        Task<ResponseNuevaCuentaDto> CreateNuevaCuentaInstructor(InstructorCuentaNuevaPostDTO cuentaInstructor);
        Task<int> CambiarUsuarioAInstructor(InstructorCambioRolInstructor cambioInstructor);
        Task<int> ActualizarInstructor(UsuarioInstructorPutDTO updateInstructor);
        Task<bool> Exists(string carnet);
    }
}
