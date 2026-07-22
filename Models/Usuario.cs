using System.ComponentModel.DataAnnotations;

namespace SistemaUniversidad.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public string UsuarioNombre { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool PrimerAcceso { get; set; }

        public int VecesInicioSesion { get; set; }

        public DateTime? FechaUltimoAcceso { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }

        public int IdRol { get; set; }

        // Relación con Rol
        public Rol? Rol { get; set; }

        // Relación con Estudiante
        public Estudiante? Estudiante { get; set; }

        // Relación con Docente
        public Docente? Docente { get; set; }
    }
}
