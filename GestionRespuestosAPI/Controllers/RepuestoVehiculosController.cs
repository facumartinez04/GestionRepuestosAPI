using AutoMapper;
using GestionRepuestosAPI.Modelos.Dtos;
using GestionRepuestosAPI.Repository.Interfaces;
using GestionRespuestosAPI.Modelos;
using GestionRespuestosAPI.Modelos.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionRepuestosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepuestoVehiculosController : ControllerBase
    {
        private readonly IRepuestoVehiculoRepository _repuestoVehiculoRepository;
        private readonly IMapper _mapper;

        public RepuestoVehiculosController(IRepuestoVehiculoRepository repuestoVehiculoRepository, IMapper mapper)
        {
            _repuestoVehiculoRepository = repuestoVehiculoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetRepuestosVehiculos()
        {
            var lista = _repuestoVehiculoRepository.ObtenerRepuestosVehiculos();
            if (lista == null || !lista.Any())
            {
                return NotFound();
            }
            var dto = _mapper.Map<IEnumerable<RepuestoVehiculoReadDto>>(lista);
            return Ok(dto);
        }

        [HttpGet("{repuestoId:int}/{vehiculoId:int}", Name = "ObtenerRepuestoVehiculo")]
        public IActionResult GetRepuestoVehiculo(int repuestoId, int vehiculoId)
        {
            if (!_repuestoVehiculoRepository.ExisteRepuestoVehiculo(repuestoId, vehiculoId))
            {
                return NotFound();
            }
            var entidad = _repuestoVehiculoRepository.ObtenerRepuestoVehiculo(repuestoId, vehiculoId);
            var dto = _mapper.Map<RepuestoVehiculoReadDto>(entidad);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CrearRepuestoVehiculo([FromBody] RepuestoVehiculoCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<RepuestoVehiculo>(createDto);
            if (!_repuestoVehiculoRepository.CrearRepuestoVehiculo(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var dto = _mapper.Map<RepuestoVehiculoReadDto>(entidad);
            return CreatedAtRoute("ObtenerRepuestoVehiculo", new { repuestoId = dto.RepuestoId, vehiculoId = dto.VehiculoId }, dto);
        }

        [HttpPut]
        public IActionResult ActualizarRepuestoVehiculo([FromBody] RepuestoVehiculoReadDto updateDto)
        {
            if (updateDto == null || !_repuestoVehiculoRepository.ExisteRepuestoVehiculo(updateDto.RepuestoId, updateDto.VehiculoId))
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<RepuestoVehiculo>(updateDto);
            if (!_repuestoVehiculoRepository.ActualizarRepuestoVehiculo(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("RepuestoVehiculo actualizado correctamente.");
        }

        [HttpDelete("{repuestoId:int}/{vehiculoId:int}")]
        public IActionResult EliminarRepuestoVehiculo(int repuestoId, int vehiculoId)
        {
            if (!_repuestoVehiculoRepository.ExisteRepuestoVehiculo(repuestoId, vehiculoId))
            {
                return NotFound();
            }
            if (!_repuestoVehiculoRepository.EliminarRepuestoVehiculo(repuestoId, vehiculoId))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("RepuestoVehiculo eliminado correctamente.");
        }

        [HttpGet("exists/{repuestoId:int}/{vehiculoId:int}")]
        public IActionResult ExisteRepuestoVehiculo(int repuestoId, int vehiculoId)
        {
            var existe = _repuestoVehiculoRepository.ExisteRepuestoVehiculo(repuestoId, vehiculoId);
            if (!existe)
            {
                return NotFound();
            }
            return Ok(new { existe });
        }
    }
}