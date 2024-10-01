using Software2_API.Repositories.Models;

namespace Software2_API.Dtos
{
    public class InicioSesionDto
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public int Respuesta { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime TiempoExpiracion { get; set; }
        public ResponseUserDto UserResponse { get; set; } = new();
    }
}
