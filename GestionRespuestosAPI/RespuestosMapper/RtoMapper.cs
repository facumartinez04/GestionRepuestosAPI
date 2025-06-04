using AutoMapper;
using GestionRespuestosAPI.Modelos;
using GestionRespuestosAPI.Modelos.Dtos;

namespace GestionRespuestosAPI.RespuestosMapper
{
    public class RtoMapper : Profile
    {
        public RtoMapper() { 
            CreateMap<CategoriaReadDto, Categoria>()
                .ReverseMap();
            CreateMap<CategoriaCreateDto, Categoria>()
                           .ReverseMap();

            CreateMap<RepuestoReadDto, Repuesto>()

                .ReverseMap();

            CreateMap<RepuestoCreateDto, Repuesto>()
                .ReverseMap();


        }
    }
}
