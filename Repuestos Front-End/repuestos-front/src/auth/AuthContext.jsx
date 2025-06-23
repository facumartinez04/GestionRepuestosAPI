import { createContext, useReducer } from 'react';
import { authReducer } from './authReducer';

export const AuthContext = createContext();

const init = () => {
  const token = localStorage.getItem('token');
  
  return {
    isAuthenticated: !!token,
    token: token || null,
  };
};

export const AuthProvider = ({ children }) => {
  const [state, dispatch] = useReducer(authReducer, {}, init);

  return (
    <AuthContext.Provider value={{ state, dispatch }}>
      {children}
    </AuthContext.Provider>
  );
};
