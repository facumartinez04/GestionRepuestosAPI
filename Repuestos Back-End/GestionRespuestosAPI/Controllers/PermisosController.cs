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
    public class PermisosController : ControllerBase
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly IMapper _mapper;

        public PermisosController(IPermisoRepository permisoRepository, IMapper mapper)
        {
            _permisoRepository = permisoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPermisos()
        {
            var permisos = _permisoRepository.ObtenerPermisos();
            if (permisos == null || !permisos.Any())
            {
                return NotFound();
            }
            var dto = _mapper.Map<IEnumerable<PermisoReadDto>>(permisos);
            return Ok(dto);
        }

        [HttpGet("{id:guid}", Name = "ObtenerPermiso")]
        public IActionResult GetPermiso(Guid id)
        {
            if (!_permisoRepository.ExistePermiso(id))
            {
                return NotFound();
            }
            var permiso = _permisoRepository.ObtenerPermiso(id);
            var dto = _mapper.Map<PermisoReadDto>(permiso);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CrearPermiso([FromBody] PermisoCreateDto permisoCreateDto)
        {
            if (permisoCreateDto == null)
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<Permiso>(permisoCreateDto);
            if (!_permisoRepository.CrearPermiso(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var dto = _mapper.Map<PermisoReadDto>(entidad);
            return CreatedAtRoute("ObtenerPermiso", new { id = dto.idPermiso }, dto);
        }

        [HttpPut]
        public IActionResult ActualizarPermiso([FromBody] PermisoReadDto permisoUpdateDto)
        {
            if (permisoUpdateDto == null || !_permisoRepository.ExistePermiso(permisoUpdateDto.idPermiso))
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<Permiso>(permisoUpdateDto);
            if (!_permisoRepository.ActualizarPermiso(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("Permiso actualizado correctamente.");
        }

        [HttpDelete("{id:guid}")]
        public IActionResult EliminarPermiso(Guid id)
        {
            if (!_permisoRepository.ExistePermiso(id))
            {
                return NotFound();
            }
            if (!_permisoRepository.EliminarPermiso(id))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("Permiso eliminado correctamente.");
        }

        [HttpGet("exists/{id:guid}")]
        public IActionResult ExistePermiso(Guid id)
        {
            var existe = _permisoRepository.ExistePermiso(id);
            if (!existe)
            {
                return NotFound();
            }
            return Ok(new { existe });
        }
    }
}