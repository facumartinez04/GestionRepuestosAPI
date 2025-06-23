import React, { useState, useEffect } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import axios from 'axios';
import { getApiUrl } from '../auth/helpers';

function VehiculoModal({ show, onHide, vehiculo, recargar }) {
  const [id, setVehiculoId] = useState(null);
  const [marca, setMarca] = useState('');
  const [modelo, setModelo] = useState('');
  const [anio, setAnio] = useState('');
  const getApi = getApiUrl();


  useEffect(() => {
    if (vehiculo) {
      setMarca(vehiculo.marca);
      setModelo(vehiculo.modelo);
      setAnio(vehiculo.anio);
      setVehiculoId(vehiculo.id);
    } else {
      setMarca('');
      setModelo('');
      setAnio('');
    }
  }, [vehiculo]);

  const handleGuardar = async () => {

    const data = { id ,marca, modelo, anio };

    try {
      if (vehiculo) {
        await axios.put(`${getApi}/Vehiculos`, data, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        });
      } else {
        await axios.post(`${getApi}/Vehiculos`, data, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        });
      }

      recargar();
      onHide();
    } catch (error) {
      console.error('Error al guardar vehículo', error);
    }
  };

  return (
    <Modal show={show} onHide={onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{vehiculo ? 'Editar Vehículo' : 'Crear Vehículo'}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group controlId="marca" className="mb-3">
            <Form.Label>Marca</Form.Label>
            <Form.Control
              type="text"
              value={marca}
              onChange={(e) => setMarca(e.target.value)}
            />
          </Form.Group>
          <Form.Group controlId="modelo" className="mb-3">
            <Form.Label>Modelo</Form.Label>
            <Form.Control
              type="text"
              value={modelo}
              onChange={(e) => setModelo(e.target.value)}
            />
          </Form.Group>
          <Form.Group controlId="anio" className="mb-3">
            <Form.Label>Año</Form.Label>
            <Form.Control
              type="number"
              value={anio}
              onChange={(e) => setAnio(e.target.value)}
            />
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onHide}>Cancelar</Button>
        <Button variant="primary" onClick={handleGuardar}>
          {vehiculo ? 'Guardar Cambios' : 'Crear'}
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default VehiculoModal;
