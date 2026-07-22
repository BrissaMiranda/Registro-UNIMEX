using System.ComponentModel.DataAnnotations;

namespace SistemaUniversidad.Models
{
    public class Estudiante
    {
        [Key]
        public int IdEstudiante { get; set; }

        public string Carnet { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Carrera { get; set; } = string.Empty;

        public string Facultad { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public int Edad { get; set; }

        public int? IdUsuario { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
