using UserDbContext.Domain.Models.DTOs;

namespace Usuarios.API.Repository
{
    public interface IHabilidadRepository
    {
        Task<HabilidadGetPutDTO> GetHabilidades();
        Task<bool> CreateHabilidad(HabilidadPostDTO habilidad);
        Task<bool> UpdateHabilidad(HabilidadGetPutDTO habilidad);
        Task<bool> DeleteHabilidad(int id);
    }
}
