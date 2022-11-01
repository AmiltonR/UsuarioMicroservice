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
        protected ResponseNuevaCuentaDto _resp;
        private IInstructorRepository _instructorRepository;

        public InstructorController(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
            _response = new ResponseDto();
            _resp = new ResponseNuevaCuentaDto();
        }

        [HttpPost]
        [Route("nuevacuenta")]
        public async Task<object> Post([FromBody] InstructorCuentaNuevaPostDTO instructorPost)
        {

            try
            {
                _resp = await _instructorRepository.CreateNuevaCuentaInstructor(instructorPost);
                if (_resp.r == 1)
                {
                    _response.Success = true;
                    _response.Message = "Cuenta de Instructor creada";
                }
                else if (_resp.r == 2)
                {
                    _response.Message = "El carnet ya existe, intente de nuevo para generar un nuevo carnet";
                }
                else
                {
                    _response.Message = "Ocurrió un error!";
                }
                _response.Result = _resp.id;
            }
            catch (Exception ex)
            {
                _response.Message = "Error!";
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPost]
        [Route("cambiorol")]
        public async Task<object> PostInstructor([FromBody] InstructorCambioRolInstructor instructorPost)
        {
            int usuario = 0;

            try
            {
                usuario = await _instructorRepository.CambiarUsuarioAInstructor(instructorPost);
                if (usuario== 1)
                {
                    _response.Success = true;
                    _response.Message = "Cambio de cuenta de estudiante a instructor exitoso";
                }
                else if(usuario == 2)
                {
                    _response.Message = "Ya existe el instructor. Intente actualizar su perfil";
                }
                else
                {
                    _response.Message = "Error no controlado :(";
                }
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPut]
        public async Task<object> PutInstructor([FromBody] UsuarioInstructorPutDTO instructorPut)
        {
            int usuario = 0;

            try
            {
                usuario = await _instructorRepository.ActualizarInstructor(instructorPut);
                if (usuario==1)
                {
                    _response.Success = true;
                    _response.Message = "Se han guardado los cambios en la cuenta de inctructor";
                }
                else if(usuario==2)
                {
                    _response.Message = "El usuario no es un instructor";
                }
                else if (usuario == 3)
                {
                    _response.Message = "El usuario no existe";
                }
                else
                {
                    _response.Message = "Ocurrió un error :(";
                }
            }
            catch (Exception ex)
            {
                _response.Message = "Algo salió mal";
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }
    }
}
