using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserDbContext.Domain.Models.DTOs;
using Usuarios.API.Repository;

namespace Usuarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        protected ResponseDto _response;
        private IInstructorRepository _instructorRepository;

        public InstructorController(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
            _response = new ResponseDto();
        }

        [HttpPost]
        [Route("nuevacuenta")]
        public async Task<object> Post([FromBody] InstructorCuentaNuevaPostDTO instructorPost)
        {
            bool usuario = false;

            try
            {
                usuario = await _instructorRepository.CreateNuevaCuentaInstructor(instructorPost);
                if (usuario)
                {
                    _response.Success = true;
                    _response.Message = "Cuenta de Instructor creada";
                }
                else
                {
                    _response.Message = "Ocurrió un error! Conexión a base de datos fallida o Carnet ya existe";
                }
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }
    }
}
