namespace GestionRespuestosAPI.Modelos.Dtos
{
    public class UsuarioRegisterDto
    {

        public string NombreUsuario { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;
    }
}
