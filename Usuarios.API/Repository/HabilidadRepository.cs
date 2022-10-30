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
        public async Task<bool> CreateHabilidad(HabilidadPostDTO habilidad)
        {
            bool flag = false;
            try
            {
                Habilidad hab = _mapper.Map<HabilidadPostDTO, Habilidad>(habilidad);
                _db.Habilidades.Add(hab);
                await _db.SaveChangesAsync();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
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

        public async Task<bool> UpdateHabilidad(HabilidadGetPutDTO habilidad)
        {
            bool b = false;
            try
            {
                Habilidad h = _mapper.Map<HabilidadGetPutDTO, Habilidad>(habilidad);
                _db.Habilidades.Update(h);
                await _db.SaveChangesAsync();
                b = true;
            }
            catch (Exception)
            {

                throw;
            }
            return b;
        }
    }
}
