using AutoMapper;
using GestionRespuestosAPI.Modelos;
using GestionRespuestosAPI.Modelos.Dtos;
using GestionRespuestosAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionRespuestosAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RepuestosController : ControllerBase
    {
        private readonly IRepuestoRepository _repuestoRepository;
        private readonly IMapper _mapper;

        public RepuestosController(IRepuestoRepository repuestoRepository, IMapper mapper)
        {
            _repuestoRepository = repuestoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetRepuestos()
        {
            var repuestos = _repuestoRepository.ObtenerRepuestos();
            if (repuestos == null || !repuestos.Any())
            {
                return NotFound("No se encontraron repuestos.");
            }

            var repuestosDto = _mapper.Map<IEnumerable<RepuestoReadDto>>(repuestos);
            return Ok(repuestosDto);
        }

        [HttpGet("{id:int}", Name = "ObtenerRepuesto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetRepuesto(int id)
        {
            if (!_repuestoRepository.ExisteRepuesto(id))
            {
                return NotFound($"No se encontró el repuesto con ID {id}.");
            }

            var repuesto = _repuestoRepository.ObtenerRepuesto(id);
            var repuestoDto = _mapper.Map<RepuestoReadDto>(repuesto);
            return Ok(repuestoDto);
        }


        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CrearRepuesto([FromBody] RepuestoCreateDto repuestoCreateDto)
        {
            if (repuestoCreateDto == null)
            {
                return BadRequest("Los datos del repuesto son inválidos.");
            }

            var repuesto = _mapper.Map<Repuesto>(repuestoCreateDto);
            if (!_repuestoRepository.CrearRepuesto(repuesto))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el repuesto.");
            }

            var repuestoReadDto = _mapper.Map<RepuestoReadDto>(repuesto);
            return CreatedAtRoute("ObtenerRepuesto", new { id = repuestoReadDto.Id }, repuestoReadDto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ActualizarRepuesto([FromBody] RepuestoReadDto repuestoUpdateDto)
        {
            if (repuestoUpdateDto == null || repuestoUpdateDto.Id <= 0)
            {
                return BadRequest("Los datos del repuesto son inválidos.");
            }

            if (!_repuestoRepository.ExisteRepuesto(repuestoUpdateDto.Id))
            {
                return NotFound($"No se encontró el repuesto con ID {repuestoUpdateDto.Id}.");
            }

            var repuesto = _mapper.Map<Repuesto>(repuestoUpdateDto);
            if (!_repuestoRepository.ActualizarRepuesto(repuesto))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el repuesto.");
            }

            return Ok("Repuesto actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EliminarRepuesto(int id)
        {
            if (!_repuestoRepository.ExisteRepuesto(id))
            {
                return NotFound($"No se encontró el repuesto con ID {id}.");
            }

            if (!_repuestoRepository.EliminarRepuesto(id))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el repuesto.");
            }

            return Ok("Repuesto eliminado correctamente.");
        }

        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ExisteRepuesto(int id)
        {
            var existe = _repuestoRepository.ExisteRepuesto(id);
            if (!existe)
            {
                return NotFound($"No se encontró el repuesto con ID {id}.");
            }

            return Ok(new { existe });
        }
    }
}
