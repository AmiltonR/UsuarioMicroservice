using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;
using UserDbContext.Infrastructure;

namespace Usuarios.API.Repository
{
    public class GradoRepository : IGradoRepository
    {
        private readonly UserContext _db;
        private IMapper _mapper;

        public GradoRepository(UserContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<GradoGetPutDTO>> GetGrados()
        {
            List<Grado> grados = await _db.Grados.ToListAsync();
            return _mapper.Map<List<GradoGetPutDTO>>(grados);
        }
    }
}
