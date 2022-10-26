using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserDbContext.Domain.Models.DTOs;
using Usuarios.API.Repository;

namespace Usuarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabilidadController : ControllerBase
    {
        protected ResponseDto _response;
        private IHabilidadRepository _habilidadRepository;

        public HabilidadController(IHabilidadRepository habilidadRepository)
        {
            _habilidadRepository = habilidadRepository;
            _response = new ResponseDto();
        }

        //Get all
        [HttpGet]
        public async Task<object> GetAll()
        {
            IEnumerable<HabilidadGetPutDTO> habilidades = null;
            try
            {
                habilidades = await _habilidadRepository.GetHabilidades();
                _response.Success = true;
                _response.Result = habilidades;
                _response.Message = "Habilidades";
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }
    }
}
