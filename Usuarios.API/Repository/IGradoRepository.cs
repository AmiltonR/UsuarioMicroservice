using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;

namespace Usuarios.API.Repository
{
    public interface IGradoRepository
    {
        Task<List<GradoGetPutDTO>> GetGrados();
    }
}
