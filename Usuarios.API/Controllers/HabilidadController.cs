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

        //Get by id
        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetById(int Id)
        {
            HabilidadGetPutDTO habilidad = null;
            try
            {
                habilidad = await _habilidadRepository.GetHabilidadById(Id);
                _response.Success = true;
                _response.Result = habilidad;
                _response.Message = "Habilidad";
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPost]
        public async Task<object> Post([FromBody] HabilidadPostDTO habilidad)
        {
            bool usuario = false;

            try
            {
                usuario = await _habilidadRepository.CreateHabilidad(habilidad);
                if (usuario)
                {
                    _response.Success = true;
                    _response.Message = "Nuevo registro de habilida guardada";
                }
                else
                {
                    _response.Message = "Ocurrió un error!";
                }
            }
            catch (Exception ex)
            {
                _response.Message = "Ocurrió un error!";
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPut]
        public async Task<object> Put([FromBody] HabilidadGetPutDTO habilidad)
        {
            bool usuario = false;

            try
            {
                usuario = await _habilidadRepository.UpdateHabilidad(habilidad);
                if (usuario)
                {
                    _response.Success = true;
                    _response.Message = "Registro actualizado";
                }
                else
                {
                    _response.Message = "Ocurrió un error!";
                }
            }
            catch (Exception ex)
            {
                _response.Message = "Ocurrió un error!";
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }
    }
}
