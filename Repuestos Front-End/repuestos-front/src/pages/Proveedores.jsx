import React, { useState, useEffect } from 'react';
import axios from 'axios';
import ProveedorModal from '../components/ProveedorModal';
import { getApiUrl } from '../auth/helpers';

function Proveedores() {
  const [proveedores, setProveedores] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [proveedorActual, setProveedorActual] = useState(null);
  const getApi = getApiUrl();

  useEffect(() => {
    cargarProveedores();
  }, []);

  const cargarProveedores = async () => {
    try {
      const res = await axios.get(`${getApi}/Proveedores`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        }
      });
      console.log(res);
      setProveedores(res.data.result); // o res.data, según tu API
    } catch (error) {
      console.error('Error al cargar proveedores', error);
    }
  };

  const eliminarProveedor = async (id) => {
    try {
      await axios.delete(`${getApi}/Proveedores/${id}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        }
      });
      cargarProveedores();
    } catch (error) {
      console.error('Error al eliminar proveedor', error);
    }
  };

  const abrirCrear = () => {
    setProveedorActual(null);
    setShowModal(true);
  };

  const abrirEditar = (proveedor) => {
    setProveedorActual(proveedor);
    setShowModal(true);
  };

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Proveedores</h2>
        <button className="btn btn-primary" onClick={abrirCrear}>Crear Proveedor</button>
      </div>

      <table className="table table-striped">
        <thead className="table-dark">
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Teléfono</th>
            <th>Email</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          {proveedores.map((proveedor) => (
            <tr key={proveedor.id}>
              <td>{proveedor.id}</td>
              <td>{proveedor.nombre}</td>
              <td>{proveedor.telefono}</td>
              <td>{proveedor.email}</td>
              <td>
                <button className="btn btn-warning btn-sm me-2" onClick={() => abrirEditar(proveedor)}>Editar</button>
                <button className="btn btn-danger btn-sm" onClick={() => eliminarProveedor(proveedor.id)}>Eliminar</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <ProveedorModal
        show={showModal}
        onHide={() => setShowModal(false)}
        proveedor={proveedorActual}
        recargar={cargarProveedores}
      />
    </div>
  );
}

export default Proveedores;
