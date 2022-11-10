using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;
using UserDbContext.Infrastructure;
using Usuarios.API.Repository;

namespace Usuarios.API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected ResponseDto _response;
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new ResponseDto();
        }

        //Get all
        [HttpGet]
        [Route("all")]
        public async Task<object> GetAll()
        {
            IEnumerable<UsuarioDTO> usuarioDtos = null;
            try
            {
                usuarioDtos = await _userRepository.GetUsers();
                _response.Success = true;
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(usuarioDtos);
        }

        //Get active
        [HttpGet]
        [Route("active")]
        public async Task<object> GetActiveUsers()
        {
            IEnumerable<UsuarioDTO> usuarioDtos = null;
            try
            {
                usuarioDtos = await _userRepository.GetActiveUsers();
                _response.Success = true;
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(usuarioDtos);
        }

        //Get inactive users
        [HttpGet]
        [Route("inactive")]
        public async Task<object> GetInactiveUsers()
        {
            IEnumerable<UsuarioDTO> usuarioDtos = null;
            try
            {
                usuarioDtos = await _userRepository.GetInactiveUsers();
                _response.Success = true;
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(usuarioDtos);
        }

        //Get By Id
        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetUserById(int id)
        {
            UsuarioDTO usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.GetUserById(id);
                if (usuarioDto == null)
                {
                    _response.Message = "Usuario no encontrado";
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Datos del usuario";
                    _response.Result = usuarioDto;
                }
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        //Get By Carnet
        [HttpGet]
        [Route("carnet/{carnet}")]
        public async Task<object> GetUserByCarnet(string carnet)
        {
            UsuarioDTO usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.GetUserByCarnet(carnet);
                if (usuarioDto == null)
                {
                    _response.Message = "Usuario no encontrado";
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Datos del usuario";
                    _response.Result = usuarioDto;
                }
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        //Get By Nombre
        [HttpGet]
        [Route("nombre/{nombre}")]
        public async Task<object> GetUserByNombre(string nombre)
        {
            UsuarioDTO usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.GetUserByNombre(nombre);
                if (usuarioDto == null)
                {
                    _response.Message = "Usuario no encontrado";
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Datos del usuario";
                    _response.Result = usuarioDto;
                }
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        //Get estudents and admins
        [HttpGet]
        [Route("usuarios")]
        public async Task<object> GetUsersRolStBib()
        {
            IEnumerable<UsuarioDTO> usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.GetUsersRolStBib();
                if (usuarioDto == null)
                {
                    _response.Message = "Hubo un error";
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Datos del usuario";
                    _response.Result = usuarioDto;
                }
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        //Get estudents
        [HttpGet]
        [Route("estudiantes")]//falta agregar al gateway
        public async Task<object> GetStudents()
        {
            IEnumerable<UsuarioDTO> usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.GetStudents();
                if (usuarioDto == null)
                {
                    _response.Message = "Hubo un error";
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Datos del usuario";
                    _response.Result = usuarioDto;
                }
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        //Get studiantes para agregado de inscribir estudiantes
        [HttpGet]
        [Route("estudiantesagg")]//falta agregar al gateway
        public async Task<object> GetStudentsAgg()
        {
            IEnumerable<UsuarioDTO> usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.GetStudents();
            }
            catch (Exception ex)
            {
            }
            return Ok(usuarioDto);
        }

        //Get instructores
        [HttpGet]
        [Route("instructores")]
        public async Task<object> GetInstructores()
        {
            IEnumerable<UsuarioInstructorDTO> usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.GetInstructores();
                if (usuarioDto == null)
                {
                    _response.Message = "Hubo un error";
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Datos del Instructor";
                    _response.Result = usuarioDto;
                }
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        //Get instructores by habilidad
        [HttpGet]
        [Route("instructoreshabilidad/{idHabilidad}")]
        public async Task<object> GetInstructoresByHabilidad(int idHabilidad)
        {
            IEnumerable<UsuarioInstructorDTO> usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.GetInstructoresByHabilidad(idHabilidad);
                if (usuarioDto == null)
                {
                    _response.Message = "Hubo un error";
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Instructores con la Habilidad solicitada";
                    _response.Result = usuarioDto;
                }
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        //Get instructores
        [HttpGet]
        [Route("instructor/{id}")]
        public async Task<object> GetInstructor(int Id)
        {
            UsuarioInstructorDTO usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.GetInstructor(Id);
                if (usuarioDto == null)
                {
                    _response.Message = "No se encontró la cuenta del Instructor";
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Datos del instructor";
                    _response.Result = usuarioDto;
                }
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }


        [HttpPost]
        public async Task<object> Post([FromBody] UsuarioPostDTO usuarioPost)
        {
            bool usuario = false;

            try
            {
                usuario = await _userRepository.CreateUser(usuarioPost);
                _response.Success = true;
                _response.Message = "Cuenta de usuario creada";
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPut]
        public async Task<object> Put([FromBody] UsuarioPutDTO usuarioPut)
        {
            UsuarioPutDTO usuario = null;

            try
            {
                usuario = await _userRepository.UpdateUser(usuarioPut);
                _response.Success = true;
                _response.Message = "Los cambios han sido actualizados";
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPut]
        [Route("setactiveinactive/{opt}/{id}")]
        public async Task<object> PutInactiveUser(int opt, int Id)
        {
            bool flag = true;
            try
            {
                flag = await _userRepository.ChangeState(opt, Id);
                if (flag)
                {
                    if (opt == 1)
                    {
                        _response.Success = true;
                        _response.Message = "La cuenta de usuario ha sido Activada";
                    }
                    else
                    {
                        _response.Success = true;
                        _response.Message = "La cuenta de usuario ha sido Desactivada";
                    }
                }
                else
                {
                    _response.Message = "Opción o Id inválido";
                }
            }
            catch (Exception ex)
            {
                _response.Message = "Opción o Id inválido";
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPut]
        [Route("cambiarrol")]
        public async Task<object> ChangeRol([FromBody] UsuarioRolDTO usuarioRol)
        {
            bool b = false;
            string msg = String.Empty;

            if (usuarioRol.IdRol==2)
            {
                msg = "Estudiante";
            }
            else
            {
                msg = "Bibliotecario";
            }

            try
            {
                b = await _userRepository.ChangeRol(usuarioRol);
                if (b)
                {
                    _response.Success = true;
                    _response.Message = "Se ha cambiado el rol de usuario a: " + msg;
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
