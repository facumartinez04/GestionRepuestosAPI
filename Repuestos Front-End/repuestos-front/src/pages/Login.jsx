import React, { useState, useContext } from 'react';
import { AuthContext } from '../auth/AuthContext';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

function Login() {
  const { dispatch } = useContext(AuthContext);
  const navigate = useNavigate();
  const [nombreusuario, setUsuario] = useState('');
  const [password, setClave] = useState('');
  const [alerta, setAlerta] = useState({ mensaje: '', tipo: '', visible: false });


  const handleLogin = async (e) => {

    
    e.preventDefault();
    try {
      const res = await axios.post('https://localhost:7268/api/Usuarios/login', { nombreusuario, password });

      console.log(res);
      const token = res.data.result.token;

      localStorage.setItem('token', token);
    dispatch({ type: 'login', payload: token });
    setAlerta({
            mensaje: res.data.message,
            tipo: 'success',
            visible: true
        });
    setTimeout(() => {
        navigate('/vehiculos');
    }, 2000);            
    } catch (err) {
        console.error(err);
        setAlerta({
            mensaje: err.response.data.message,
            tipo: 'danger',
            visible: true
        });
    }
  };

  return (
    <div className="container mt-5">
      <h3>Iniciar Sesión</h3>
      <form onSubmit={handleLogin}>
        <input type="text" value={nombreusuario} onChange={(e) => setUsuario(e.target.value)} className="form-control mb-2" placeholder="Usuario" />
        <input type="password" value={password} onChange={(e) => setClave(e.target.value)} className="form-control mb-3" placeholder="Contraseña" />
        <button type="submit" className="btn btn-primary">Ingresar</button>
        {alerta.visible && (
          <div className={`alert alert-${alerta.tipo} mt-3`}>
            {alerta.mensaje}
          </div>
        )}
      </form>
    </div>
  );
}

export default Login;
