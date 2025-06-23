import React, { useState, useEffect } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import axios from 'axios';
import { getApiUrl } from '../auth/helpers';

function ProveedorModal({ show, onHide, proveedor, recargar }) {
  const [id, setProveedorId] = useState(null);
  const [nombre, setNombre] = useState('');
  const [telefono, setTelefono] = useState('');
  const [email, setEmail] = useState('');
  const getApi = getApiUrl();

  useEffect(() => {
    if (proveedor) {
      setProveedorId(proveedor.id);
      setNombre(proveedor.nombre);
      setTelefono(proveedor.telefono);
      setEmail(proveedor.email);
    } else {
      setProveedorId(null);
      setNombre('');
      setTelefono('');
      setEmail('');
    }
  }, [proveedor]);

  const handleGuardar = async () => {
    const data = { id, nombre, telefono, email };

    try {
      if (proveedor) {
        await axios.put(`${getApi}/Proveedores`, data, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        });
      } else {
        await axios.post(`${getApi}/Proveedores`, data, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        });
      }

      recargar();
      onHide();
    } catch (error) {
      console.error('Error al guardar proveedor', error);
    }
  };

  return (
    <Modal show={show} onHide={onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{proveedor ? 'Editar Proveedor' : 'Crear Proveedor'}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group controlId="nombre" className="mb-3">
            <Form.Label>Nombre</Form.Label>
            <Form.Control
              type="text"
              value={nombre}
              onChange={(e) => setNombre(e.target.value)}
            />
          </Form.Group>
          <Form.Group controlId="telefono" className="mb-3">
            <Form.Label>Teléfono</Form.Label>
            <Form.Control
              type="text"
              value={telefono}
              onChange={(e) => setTelefono(e.target.value)}
            />
          </Form.Group>
          <Form.Group controlId="email" className="mb-3">
            <Form.Label>Email</Form.Label>
            <Form.Control
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onHide}>Cancelar</Button>
        <Button variant="primary" onClick={handleGuardar}>
          {proveedor ? 'Guardar Cambios' : 'Crear'}
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default ProveedorModal;
