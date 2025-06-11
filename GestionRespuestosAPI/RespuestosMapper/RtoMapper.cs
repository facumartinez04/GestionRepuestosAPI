using AutoMapper;
using GestionRepuestosAPI.Modelos.Dtos;
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


            CreateMap<UsuarioLoginDto, Usuario>()
                .ReverseMap();

            CreateMap<UsuarioRegisterDto, Usuario>()
                .ReverseMap();

            CreateMap<UsuarioReadDto, Usuario>()
                .ReverseMap();


            CreateMap<UsuarioDatosDto, Usuario>()
        .ReverseMap();

            CreateMap<UsuarioLoginRespuestaDto, Usuario>()
       .ReverseMap();

            CreateMap<VehiculoReadDto, Vehiculo>()
            .ReverseMap();
            CreateMap<VehiculoCreateDto, Vehiculo>()
                .ReverseMap();

            CreateMap<ProveedorReadDto, Proveedor>()
                .ReverseMap();
            CreateMap<ProveedorCreateDto, Proveedor>()
                .ReverseMap();

            CreateMap<RepuestoVehiculoReadDto, RepuestoVehiculo>()
                .ReverseMap();
            CreateMap<RepuestoVehiculoCreateDto, RepuestoVehiculo>()
                .ReverseMap();

        }
    }
}
