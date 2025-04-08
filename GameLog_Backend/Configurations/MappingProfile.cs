using AutoMapper;
using GameLog_Backend.Entities;
using GameLog.DTOs;

namespace GameLog.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Usuario, UsuarioResponseDTO>();

            
            CreateMap<Avaliacao, AvaliacaoResponseDTO>()
                .ForMember(dest => dest.JogoId, opt => opt.MapFrom(src => src.Jogo.Id));
        }
    }
}