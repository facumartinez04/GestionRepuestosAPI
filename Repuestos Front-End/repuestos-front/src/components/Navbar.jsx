import React, { useContext } from 'react'
import { NavLink } from 'react-router-dom'
import '../assets/styles/navbar.css'
import { AuthContext } from '../auth/AuthContext'


export default function Navbar() {

    const state = useContext(AuthContext);

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
      <div className="container-fluid">
        <a className="navbar-brand" href="#">Repuestos</a>
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav"
          aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav ms-auto">
            <li className="nav-item">
                <a className="nav-link" aria-current="page" href="#">Inicio</a>
            </li>
            <li className="nav-item">
                <NavLink className="nav-link" to="/vehiculos">Vehiculos</NavLink>
            </li>
            <li className="nav-item">
                <NavLink className="nav-link" to="/proveedores">Proveedores</NavLink>
            </li>
           {
            state.state.isAuthenticated ? (
              <li className="nav-item">
                <NavLink className="nav-link text-white btnLogout" to="/logout">Cerrar Sesión</NavLink>
              </li>
            ) : (
              <li className="nav-item">
              </li>
            )
           }
          </ul>
        </div>
      </div>
    </nav>
  )
}
