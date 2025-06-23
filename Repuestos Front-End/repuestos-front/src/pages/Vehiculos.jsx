import React, { useState, useEffect } from 'react';
import axios from 'axios';
import VehiculoModal from '../components/VehiculoModal';
import { getApiUrl } from '../auth/helpers'; 


function Vehiculos() {
  const [vehiculos, setVehiculos] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [vehiculoActual, setVehiculoActual] = useState(null);
  const getApi = getApiUrl();

  useEffect(() => {
    cargarVehiculos();
  }, []);

  const cargarVehiculos = async () => {
    try {
      const res = await axios.get(`${getApi}/Vehiculos`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        }
      });
        console.log(res);
      setVehiculos(res.data.result);
    } catch (error) {
      console.error('Error al cargar vehículos', error);
    }
  };

  const eliminarVehiculo = async (id) => {
    try {
      await axios.delete(`${getApi}/Vehiculos/${id}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        }
      });
      cargarVehiculos();
    } catch (error) {
      console.error('Error al eliminar vehículo', error);
    }
  };

  const abrirCrear = () => {
    setVehiculoActual(null);
    setShowModal(true);
  };

  const abrirEditar = (vehiculo) => {
    setVehiculoActual(vehiculo);
    setShowModal(true);
  };

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Vehículos</h2>
        <button className="btn btn-primary" onClick={abrirCrear}>Crear Vehículo</button>
      </div>

      <table className="table table-striped">
        <thead className="table-dark">
          <tr>
            <th>ID</th>
            <th>Marca</th>
            <th>Modelo</th>
            <th>Año</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          {vehiculos.map((vehiculo) => (
            <tr key={vehiculo.id}>
              <td>{vehiculo.id}</td>
              <td>{vehiculo.marca}</td>
              <td>{vehiculo.modelo}</td>
              <td>{vehiculo.anio}</td>
              <td>
                <button className="btn btn-warning btn-sm me-2" onClick={() => abrirEditar(vehiculo)}>Editar</button>
                <button className="btn btn-danger btn-sm" onClick={() => eliminarVehiculo(vehiculo.id)}>Eliminar</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <VehiculoModal
        show={showModal}
        onHide={() => setShowModal(false)}
        vehiculo={vehiculoActual}
        recargar={cargarVehiculos}
      />
    </div>
  );
}

export default Vehiculos;
