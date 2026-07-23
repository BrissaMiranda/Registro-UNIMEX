using System.ComponentModel.DataAnnotations;

// Programador: Juan Eduardo Lopez Jaime
namespace SistemaUniversidad.Models
{
    public class Docente
    {
        [Key]
        public int IdDocente { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Facultad { get; set; } = string.Empty;

        public string Especialidad { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public int? IdUsuario { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
