using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GestionRepuestosAPI.Modelos;
using GestionRepuestosAPI.Modelos.Dtos;
using GestionRepuestosAPI.Repository.Interfaces;
using GestionRespuestosAPI.Modelos.Dtos;
using GestionRespuestosAPI.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace GestionRepuestosAPI.Controllers
{
    [Authorize(Roles = "Administrador,Usuario")]
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly IMapper _mapper;
        protected RespuestaAPI _respuestaAPI;

        public VehiculosController(IVehiculoRepository vehiculoRepository, IMapper mapper)
        {
            _vehiculoRepository = vehiculoRepository;
            _mapper = mapper;
            _respuestaAPI = new RespuestaAPI();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetVehiculos()
        {
            var vehiculos = _vehiculoRepository.ObtenerVehiculos();
            if (vehiculos == null || !vehiculos.Any())
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "No se encontraron vehículos.";
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_respuestaAPI);
            }
            var vehiculosDto = _mapper.Map<IEnumerable<VehiculoReadDto>>(vehiculos);
            _respuestaAPI.Result = vehiculosDto;
            _respuestaAPI.Message = "Vehículos obtenidos correctamente.";
            return Ok(_respuestaAPI);
        }

        [HttpGet("{id:int}", Name = "ObtenerVehiculo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetVehiculo(int id)
        {
            if (id <= 0)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "El ID del vehículo debe ser mayor que cero.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }
            if (!_vehiculoRepository.ExisteVehiculo(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = $"No se encontró el vehículo con ID {id}.";
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_respuestaAPI);
            }
            var vehiculo = _vehiculoRepository.ObtenerVehiculo(id);
            var vehiculoDto = _mapper.Map<VehiculoReadDto>(vehiculo);
            _respuestaAPI.Result = vehiculoDto;
            _respuestaAPI.Message = "Vehículo obtenido correctamente.";
            return Ok(_respuestaAPI);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CrearVehiculo([FromBody] VehiculoCreateDto vehiculoCreateDto)
        {
            if (vehiculoCreateDto == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Los datos del vehículo son inválidos.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }
            var vehiculo = _mapper.Map<Vehiculo>(vehiculoCreateDto);
            if (!_vehiculoRepository.CrearVehiculo(vehiculo))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Error al crear el vehículo.";
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }
            var vehiculoReadDto = _mapper.Map<VehiculoReadDto>(vehiculo);
            _respuestaAPI.Result = vehiculoReadDto;
            _respuestaAPI.Message = "Vehículo creado correctamente.";
            _respuestaAPI.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("ObtenerVehiculo", new { id = vehiculoReadDto.Id }, _respuestaAPI);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ActualizarVehiculo([FromBody] VehiculoReadDto vehiculoUpdateDto)
        {
            if (vehiculoUpdateDto == null || vehiculoUpdateDto.Id <= 0)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Los datos del vehículo son inválidos.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }
            if (!_vehiculoRepository.ExisteVehiculo(vehiculoUpdateDto.Id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = $"No se encontró el vehículo con ID {vehiculoUpdateDto.Id}.";
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_respuestaAPI);
            }
            var vehiculo = _mapper.Map<Vehiculo>(vehiculoUpdateDto);
            if (!_vehiculoRepository.ActualizarVehiculo(vehiculo))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Error al actualizar el vehículo.";
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }
            _respuestaAPI.Message = "Vehículo actualizado correctamente.";
            _respuestaAPI.Result = null;
            return Ok(_respuestaAPI);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EliminarVehiculo(int id)
        {
            if (!_vehiculoRepository.ExisteVehiculo(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = $"No se encontró el vehículo con ID {id}.";
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_respuestaAPI);
            }
            if (!_vehiculoRepository.EliminarVehiculo(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Error al eliminar el vehículo.";
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }
            _respuestaAPI.Message = "Vehículo eliminado correctamente.";
            _respuestaAPI.Result = null;
            return Ok(_respuestaAPI);
        }

        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ExisteVehiculo(int id)
        {
            var existe = _vehiculoRepository.ExisteVehiculo(id);
            if (!existe)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = $"No se encontró el vehículo con ID {id}.";
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_respuestaAPI);
            }
            _respuestaAPI.Result = new { existe };
            _respuestaAPI.Message = "Existencia verificada.";
            return Ok(_respuestaAPI);
        }
    }
}
