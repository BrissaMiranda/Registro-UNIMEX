using System.ComponentModel.DataAnnotations;

namespace SistemaUniversidad.ViewModels
{
    public class CambiarPasswordViewModel
    {
        [Required(ErrorMessage = "Ingrese su contraseña actual")]
        [DataType(DataType.Password)]
        public string PasswordActual { get; set; } = "";

        [Required(ErrorMessage = "Ingrese la nueva contraseña")]
        [DataType(DataType.Password)]
        public string PasswordNueva { get; set; } = "";

        [Required(ErrorMessage = "Confirme la nueva contraseña")]
        [DataType(DataType.Password)]
        [Compare("PasswordNueva", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarPassword { get; set; } = "";
    }
}
