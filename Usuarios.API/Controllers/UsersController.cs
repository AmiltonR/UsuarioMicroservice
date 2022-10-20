using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<object> Get()
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

        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetUserById(int id)
        {
            UsuarioDTO usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.GetUserById(id);
                _response.Success = true;
                _response.Message = "Datos del usuario";
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(usuarioDto);
        }

        [HttpGet]
        [Route("carnet/{carnet}")]
        public async Task<object> GetUserByCarnet(string carnet)
        {
            UsuarioDTO usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.GetUserByCarnet(carnet);
                _response.Success = true;
                _response.Message = "Datos del usuario";
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<object> Post([FromBody] UsuarioPostDTO usuarioPost)
        {
            UsuarioPostDTO usuario = null;

            try
            {
                bool exists = await _userRepository.Exists(usuarioPost.Carnet);
                if (!exists)
                {
                    usuario = await _userRepository.CreateUser(usuarioPost);
                    _response.Success = true;
                    _response.Message = "Cuenta de usuario creada";
                }
                else
                {
                    _response.Message = "El carnet ya existe";
                } 
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
        [Route("{id}")]
        public async Task<object> PutRol(int Id)
        {
            UsuarioDeleteDTO usuarioDto = null;
            try
            {
                usuarioDto = await _userRepository.SetInactive(Id);
                _response.Success = true;
                _response.Message = "La cuenta de usuario ha sido desactivada";
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPut]
        [Route("activate/{id}")]
        public async Task<object> PutActivateUser(int Id)
        {
            UsuarioDeleteDTO usuarioDto = null; // Se está usando UsuarioDeleteDTO porque mapea los campos necesarios(ID, ESTADO) para modificar el estado
            try
            {
                usuarioDto = await _userRepository.SetActive(Id);
                _response.Success = true;
                _response.Message = "La cuenta de usuario ha sido activada";
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }
    }
}
