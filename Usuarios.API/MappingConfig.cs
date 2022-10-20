using AutoMapper;
using System.Net.Http.Headers;
using UserDbContext.Domain.Models.DTOs;
using UserDbContext.Domain.Models.Entities;

namespace Usuarios.API
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Usuario, UsuarioDTO>();
                config.CreateMap<UsuarioDTO, Usuario>();

                config.CreateMap<Usuario, UsuarioPostDTO>();
                config.CreateMap<UsuarioPostDTO, Usuario>();

                config.CreateMap<Usuario, UsuarioPutDTO>();
                config.CreateMap<UsuarioPutDTO, Usuario>();

                config.CreateMap<Usuario, UsuarioDeleteDTO>();
                config.CreateMap<UsuarioDeleteDTO, Usuario>();

            });
            return mappingConfig;
        }
    }
}
