using AutoMapper;
using GestionRepuestosAPI.Modelos;
using GestionRepuestosAPI.Modelos.Dtos;
using GestionRepuestosAPI.Repository.Interfaces;
using GestionRespuestosAPI.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionRepuestosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolRepository _rolRepository;
        private readonly IMapper _mapper;

        public RolesController(IRolRepository rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _rolRepository.ObtenerRoles();
            if (roles == null || !roles.Any())
            {
                return NotFound();
            }
            var dto = _mapper.Map<IEnumerable<RolReadDto>>(roles);
            return Ok(dto);
        }

        [HttpGet("{id:guid}", Name = "ObtenerRol")]
        public IActionResult GetRol(Guid id)
        {
            if (!_rolRepository.ExisteRol(id))
            {
                return NotFound();
            }
            var rol = _rolRepository.ObtenerRol(id);
            var dto = _mapper.Map<RolReadDto>(rol);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CrearRol([FromBody] RolCreateDto rolCreateDto)
        {
            if (rolCreateDto == null)
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<Rol>(rolCreateDto);
            if (!_rolRepository.CrearRol(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var dto = _mapper.Map<RolReadDto>(entidad);
            return CreatedAtRoute("ObtenerRol", new { id = dto.idRol }, dto);
        }

        [HttpPut]
        public IActionResult ActualizarRol([FromBody] RolReadDto rolUpdateDto)
        {
            if (rolUpdateDto == null || !_rolRepository.ExisteRol(rolUpdateDto.idRol))
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<Rol>(rolUpdateDto);
            if (!_rolRepository.ActualizarRol(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("Rol actualizado correctamente.");
        }

        [HttpDelete("{id:guid}")]
        public IActionResult EliminarRol(Guid id)
        {
            if (!_rolRepository.ExisteRol(id))
            {
                return NotFound();
            }
            if (!_rolRepository.EliminarRol(id))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("Rol eliminado correctamente.");
        }

        [HttpGet("exists/{id:guid}")]
        public IActionResult ExisteRol(Guid id)
        {
            var existe = _rolRepository.ExisteRol(id);
            if (!existe)
            {
                return NotFound();
            }
            return Ok(new { existe });
        }
    }
}