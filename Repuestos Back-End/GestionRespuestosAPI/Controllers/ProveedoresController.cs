using AutoMapper;
using GestionRepuestosAPI.Modelos;
using GestionRepuestosAPI.Modelos.Dtos;
using GestionRepuestosAPI.Repository.Interfaces;
using GestionRespuestosAPI.Modelos.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace GestionRepuestosAPI.Controllers
{
    [Authorize(Roles = "Administrador,Usuario")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IMapper _mapper;
        protected RespuestaAPI _respuestaAPI;

        public ProveedoresController(IProveedorRepository proveedorRepository, IMapper mapper)
        {
            _proveedorRepository = proveedorRepository;
            _mapper = mapper;
            _respuestaAPI = new RespuestaAPI();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProveedores()
        {
            var proveedores = _proveedorRepository.ObtenerProveedores();
            if (proveedores == null || !proveedores.Any())
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "No se encontraron proveedores.";
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_respuestaAPI);
            }
            var dto = _mapper.Map<IEnumerable<ProveedorReadDto>>(proveedores);
            _respuestaAPI.Result = dto;
            _respuestaAPI.Message = "Proveedores obtenidos correctamente.";
            return Ok(_respuestaAPI);
        }

        [HttpGet("{id:int}", Name = "ObtenerProveedor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProveedor(int id)
        {
            if (!_proveedorRepository.ExisteProveedor(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = $"No se encontró el proveedor con ID {id}.";
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_respuestaAPI);
            }
            var proveedor = _proveedorRepository.ObtenerProveedor(id);
            var dto = _mapper.Map<ProveedorReadDto>(proveedor);
            _respuestaAPI.Result = dto;
            _respuestaAPI.Message = "Proveedor obtenido correctamente.";
            return Ok(_respuestaAPI);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CrearProveedor([FromBody] ProveedorCreateDto proveedorCreateDto)
        {
            if (proveedorCreateDto == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Los datos del proveedor son inválidos.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }

            var entidad = _mapper.Map<Proveedor>(proveedorCreateDto);
            if (!_proveedorRepository.CrearProveedor(entidad))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Error al crear el proveedor.";
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }

            var dto = _mapper.Map<ProveedorReadDto>(entidad);
            _respuestaAPI.Result = dto;
            _respuestaAPI.Message = "Proveedor creado correctamente.";
            _respuestaAPI.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("ObtenerProveedor", new { id = dto.Id }, _respuestaAPI);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ActualizarProveedor([FromBody] ProveedorReadDto proveedorUpdateDto)
        {
            if (proveedorUpdateDto == null || proveedorUpdateDto.Id <= 0)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Los datos del proveedor son inválidos.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }

            if (!_proveedorRepository.ExisteProveedor(proveedorUpdateDto.Id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = $"No se encontró el proveedor con ID {proveedorUpdateDto.Id}.";
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_respuestaAPI);
            }

            var entidad = _mapper.Map<Proveedor>(proveedorUpdateDto);
            if (!_proveedorRepository.ActualizarProveedor(entidad))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Error al actualizar el proveedor.";
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }

            _respuestaAPI.Message = "Proveedor actualizado correctamente.";
            _respuestaAPI.Result = null;
            return Ok(_respuestaAPI);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EliminarProveedor(int id)
        {
            if (!_proveedorRepository.ExisteProveedor(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = $"No se encontró el proveedor con ID {id}.";
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_respuestaAPI);
            }

            if (!_proveedorRepository.EliminarProveedor(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Error al eliminar el proveedor.";
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }

            _respuestaAPI.Message = "Proveedor eliminado correctamente.";
            _respuestaAPI.Result = null;
            return Ok(_respuestaAPI);
        }

        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ExisteProveedor(int id)
        {
            var existe = _proveedorRepository.ExisteProveedor(id);
            if (!existe)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = $"No se encontró el proveedor con ID {id}.";
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_respuestaAPI);
            }

            _respuestaAPI.Result = new { existe };
            _respuestaAPI.Message = "Existencia verificada.";
            return Ok(_respuestaAPI);
        }
    }
}
