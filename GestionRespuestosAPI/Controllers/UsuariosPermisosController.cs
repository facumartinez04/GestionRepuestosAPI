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
    public class UsuariosPermisosController : ControllerBase
    {
        private readonly IUsuarioPermisoRepository _usuarioPermisoRepository;
        private readonly IMapper _mapper;

        public UsuariosPermisosController(IUsuarioPermisoRepository usuarioPermisoRepository, IMapper mapper)
        {
            _usuarioPermisoRepository = usuarioPermisoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsuariosPermisos()
        {
            var lista = _usuarioPermisoRepository.ObtenerUsuariosPermisos();
            if (lista == null || !lista.Any())
            {
                return NotFound();
            }
            var dto = _mapper.Map<IEnumerable<UsuarioPermisoReadDto>>(lista);
            return Ok(dto);
        }

        [HttpGet("{usuarioId:int}/{permisoId:guid}", Name = "ObtenerUsuarioPermiso")]
        public IActionResult GetUsuarioPermiso(int usuarioId, Guid permisoId)
        {
            if (!_usuarioPermisoRepository.ExisteUsuarioPermiso(usuarioId, permisoId))
            {
                return NotFound();
            }
            var entidad = _usuarioPermisoRepository.ObtenerUsuarioPermiso(usuarioId, permisoId);
            var dto = _mapper.Map<UsuarioPermisoReadDto>(entidad);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CrearUsuarioPermiso([FromBody] UsuarioPermisoCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<UsuarioPermiso>(createDto);
            if (!_usuarioPermisoRepository.CrearUsuarioPermiso(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var dto = _mapper.Map<UsuarioPermisoReadDto>(entidad);
            return CreatedAtRoute("ObtenerUsuarioPermiso", new { usuarioId = dto.idUsuario, permisoId = dto.idPermiso }, dto);
        }

        [HttpPut]
        public IActionResult ActualizarUsuarioPermiso([FromBody] UsuarioPermisoReadDto updateDto)
        {
            if (updateDto == null || !_usuarioPermisoRepository.ExisteUsuarioPermiso(updateDto.idUsuario, updateDto.idPermiso))
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<UsuarioPermiso>(updateDto);
            if (!_usuarioPermisoRepository.ActualizarUsuarioPermiso(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("UsuarioPermiso actualizado correctamente.");
        }

        [HttpDelete("{usuarioId:int}/{permisoId:guid}")]
        public IActionResult EliminarUsuarioPermiso(int usuarioId, Guid permisoId)
        {
            if (!_usuarioPermisoRepository.ExisteUsuarioPermiso(usuarioId, permisoId))
            {
                return NotFound();
            }
            if (!_usuarioPermisoRepository.EliminarUsuarioPermiso(usuarioId, permisoId))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("UsuarioPermiso eliminado correctamente.");
        }

        [HttpGet("exists/{usuarioId:int}/{permisoId:guid}")]
        public IActionResult ExisteUsuarioPermiso(int usuarioId, Guid permisoId)
        {
            var existe = _usuarioPermisoRepository.ExisteUsuarioPermiso(usuarioId, permisoId);
            if (!existe)
            {
                return NotFound();
            }
            return Ok(new { existe });
        }


        [HttpGet("usuario/{idUsuario:int}")]
        public IActionResult GetUsuarioPermisos(int idUsuario)
        {
            var lista = _usuarioPermisoRepository.ObtenerPermisosPorUsuario(idUsuario);
            if (lista == null || !lista.Any())
            {
                return NotFound();
            }
            var dto = _mapper.Map<IEnumerable<UsuarioPermisoReadDto>>(lista);
            return Ok(dto);
        }
    }
}