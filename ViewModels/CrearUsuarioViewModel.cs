using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaUniversidad.ViewModels
{
    public class CrearUsuarioViewModel
    {
        [Required]
        public string Usuario { get; set; } = "";

        [Required]
        public int IdRol { get; set; }

        public int? IdEstudiante { get; set; }

        public int? IdDocente { get; set; }

        public bool Activo { get; set; } = true;

        public List<SelectListItem> Roles { get; set; } = new();

        public List<SelectListItem> Estudiantes { get; set; } = new();

        public List<SelectListItem> Docentes { get; set; } = new();
    }
}
