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
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IMapper _mapper;

        public ProveedoresController(IProveedorRepository proveedorRepository, IMapper mapper)
        {
            _proveedorRepository = proveedorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProveedores()
        {
            var proveedores = _proveedorRepository.ObtenerProveedores();
            if (proveedores == null || !proveedores.Any())
            {
                return NotFound();
            }
            var dto = _mapper.Map<IEnumerable<ProveedorReadDto>>(proveedores);
            return Ok(dto);
        }

        [HttpGet("{id:int}", Name = "ObtenerProveedor")]
        public IActionResult GetProveedor(int id)
        {
            if (!_proveedorRepository.ExisteProveedor(id))
            {
                return NotFound();
            }
            var proveedor = _proveedorRepository.ObtenerProveedor(id);
            var dto = _mapper.Map<ProveedorReadDto>(proveedor);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CrearProveedor([FromBody] ProveedorCreateDto proveedorCreateDto)
        {
            if (proveedorCreateDto == null)
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<Proveedor>(proveedorCreateDto);
            if (!_proveedorRepository.CrearProveedor(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var dto = _mapper.Map<ProveedorReadDto>(entidad);
            return CreatedAtRoute("ObtenerProveedor", new { id = dto.Id }, dto);
        }

        [HttpPut]
        public IActionResult ActualizarProveedor([FromBody] ProveedorReadDto proveedorUpdateDto)
        {
            if (proveedorUpdateDto == null || !_proveedorRepository.ExisteProveedor(proveedorUpdateDto.Id))
            {
                return BadRequest();
            }
            var entidad = _mapper.Map<Proveedor>(proveedorUpdateDto);
            if (!_proveedorRepository.ActualizarProveedor(entidad))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("Proveedor actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]
        public IActionResult EliminarProveedor(int id)
        {
            if (!_proveedorRepository.ExisteProveedor(id))
            {
                return NotFound();
            }
            if (!_proveedorRepository.EliminarProveedor(id))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("Proveedor eliminado correctamente.");
        }

        [HttpGet("exists/{id:int}")]
        public IActionResult ExisteProveedor(int id)
        {
            var existe = _proveedorRepository.ExisteProveedor(id);
            if (!existe)
            {
                return NotFound();
            }
            return Ok(new { existe });
        }
    }
}