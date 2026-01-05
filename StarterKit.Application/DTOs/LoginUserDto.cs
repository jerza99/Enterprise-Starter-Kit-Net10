using System.ComponentModel.DataAnnotations;

namespace StarterKit.Application.DTOs
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "El email o usuario es requerido")]
        [Display(Name = "Email o Usuario")]
        public string EmailOrUserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; } = false;
    }
}