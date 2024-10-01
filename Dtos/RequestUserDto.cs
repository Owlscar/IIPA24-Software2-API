using System.ComponentModel.DataAnnotations;

namespace Software2_API.Dtos
{
    public class RequestUserDto
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = string.Empty;
        public int IdRole { get; set; } = 2;
        public int IdState { get; set; } = 1;
    }
}
