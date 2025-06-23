
import { Routes, Route, Navigate } from 'react-router-dom';
import PrivateRoute from './PrivateRoute';
import Login from '../pages/Login';
import Logout from '../pages/Logout';
import Vehiculos from '../pages/Vehiculos';
import Proveedores from '../pages/Proveedores';


function AppRouter() {
  return (
     <Routes>
      <Route path="/login" element={<Login />} />

      <Route
        path="/vehiculos"
        element={
          <PrivateRoute>
            <Vehiculos />
          </PrivateRoute>
        }
      />


            <Route
        path="/proveedores"
        element={
          <PrivateRoute>
            <Proveedores />
          </PrivateRoute>
        }
      />


      {}
      <Route path="/logout" element={<Logout />} />

      {}
      <Route path="*" element={<Navigate to="/login" />} />
    </Routes>
  );
}

export default AppRouter;
