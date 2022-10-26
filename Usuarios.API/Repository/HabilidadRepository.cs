using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;
using UserDbContext.Infrastructure;

namespace Usuarios.API.Repository
{
    public class HabilidadRepository : IHabilidadRepository
    {
        private readonly UserContext _db;
        private IMapper _mapper;

        public HabilidadRepository(UserContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public Task<bool> CreateHabilidad(HabilidadPostDTO habilidad)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteHabilidad(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HabilidadGetPutDTO>> GetHabilidades()
        {
            List<Habilidad> habilidades = await _db.Habilidades.ToListAsync();
            return _mapper.Map<List<HabilidadGetPutDTO>>(habilidades);
        }

        public Task<bool> UpdateHabilidad(HabilidadGetPutDTO habilidad)
        {
            throw new NotImplementedException();
        }
    }
}
