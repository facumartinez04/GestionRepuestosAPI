export const isAuthenticated = () => {
  const token = localStorage.getItem('token');
  return !!token;
};


export const getApiUrl = () => {
  const apiUrl = "https://localhost:7268/api";
  
  return apiUrl;
}