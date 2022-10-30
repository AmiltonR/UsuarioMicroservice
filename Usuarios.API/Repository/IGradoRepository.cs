using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;

namespace Usuarios.API.Repository
{
    public interface IGradoRepository
    {
        Task<List<GradoGetPutDTO>> GetGrados();
        Task<bool> CreateGrado(GradoPostDTO grado);
        Task<bool> UpdateGrado(GradoGetPutDTO grado);
    }
}
