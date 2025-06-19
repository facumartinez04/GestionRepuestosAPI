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
    public class UsuariosRolesController : ControllerBase
    {
        private readonly IUsuarioRolRepository _usuarioRolRepository;
        private readonly IMapper _mapper;

        public UsuariosRolesController(IUsuarioRolRepository usuarioRolRepository, IMapper mapper)
        {
            _usuarioRolRepository = usuarioRolRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsuariosRoles()
        {
            var lista = _usuarioRolRepository.ObtenerUsuariosRoles();
            if (lista == null || !lista.Any())
            {
                return NotFound();
            }
            var dto = _mapper.Map<IEnumerable<UsuarioRolReadDto>>(lista);
            return Ok(dto);
        }

        [HttpGet("{usuarioId:int}/{rolId:guid}", Name = "ObtenerUsuarioRol")]
        public IActionResult GetUsuarioRol(int usuarioId, Guid rolId)
        {
            if (!_usuarioRolRepository.ExisteUsuarioRol(usuarioId, rolId))
            {
                return NotFound();
            }
            var entidad = _usuarioRolRepository.ObtenerUsuarioRol(usuarioId, rolId);
            var dto = _mapper.Map<UsuarioRolReadDto>(entidad);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CrearUsuarioRol([FromBody] UsuarioRolCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<UsuarioRol>(createDto);
            if (!_usuarioRolRepository.CrearUsuarioRol(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var dto = _mapper.Map<UsuarioRolReadDto>(entidad);
            return CreatedAtRoute("ObtenerUsuarioRol", new { usuarioId = dto.idUsuario, rolId = dto.idRol }, dto);
        }

        [HttpPut]
        public IActionResult ActualizarUsuarioRol([FromBody] UsuarioRolReadDto updateDto)
        {
            if (updateDto == null || !_usuarioRolRepository.ExisteUsuarioRol(updateDto.idUsuario, updateDto.idRol))
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<UsuarioRol>(updateDto);
            if (!_usuarioRolRepository.ActualizarUsuarioRol(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("UsuarioRol actualizado correctamente.");
        }

        [HttpDelete("{usuarioId:int}/{rolId:guid}")]
        public IActionResult EliminarUsuarioRol(int usuarioId, Guid rolId)
        {
            if (!_usuarioRolRepository.ExisteUsuarioRol(usuarioId, rolId))
            {
                return NotFound();
            }
            if (!_usuarioRolRepository.EliminarUsuarioRol(usuarioId, rolId))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("UsuarioRol eliminado correctamente.");
        }

        [HttpGet("exists/{usuarioId:int}/{rolId:guid}")]
        public IActionResult ExisteUsuarioRol(int usuarioId, Guid rolId)
        {
            var existe = _usuarioRolRepository.ExisteUsuarioRol(usuarioId, rolId);
            if (!existe)
            {
                return NotFound();
            }
            return Ok(new { existe });
        }


        [HttpGet("roles/{usuarioId:int}")]
        public IActionResult ObtenerRolesPorUsuario(int usuarioId)
        {
            var roles = _usuarioRolRepository.ObtenerRolesPorUsuario(usuarioId);
            if (roles == null || !roles.Any())
            {
                return NotFound();
            }
            var dto = _mapper.Map<IEnumerable<UsuarioRolReadDto>>(roles);
            return Ok(dto);
        }
    }
}