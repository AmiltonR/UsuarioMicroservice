using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserDbContext.Domain.Models.DTOs;
using Usuarios.API.Repository;

namespace Usuarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradoController : ControllerBase
    {
        protected ResponseDto _response;
        private IGradoRepository _gradoRepository;

        public GradoController(IGradoRepository userRepository)
        {
            _gradoRepository = userRepository;
            _response = new ResponseDto();
        }

        //Get all
        [HttpGet]
        public async Task<object> GetAll()
        {
            IEnumerable<GradoGetPutDTO> grados = null;
            try
            {
                grados = await _gradoRepository.GetGrados();
                _response.Success = true;
                _response.Result = grados;
                _response.Message = "Grados";
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }
    }
}
