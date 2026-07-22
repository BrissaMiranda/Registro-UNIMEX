using System.ComponentModel.DataAnnotations;

namespace SistemaUniversidad.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }

        public string NombreRol { get; set; } = string.Empty;

        // Un rol puede pertenecer a muchos usuarios
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
