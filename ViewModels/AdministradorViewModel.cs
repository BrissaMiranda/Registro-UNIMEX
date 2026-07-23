// Programador Josue
namespace SistemaUniversidad.ViewModels
{
    public class AdministradorViewModel
    {
        public List<UsuarioViewModel> Usuarios { get; set; } = new();

        public int TotalUsuarios { get; set; }

        public int TotalEstudiantes { get; set; }

        public int TotalDocentes { get; set; }

        public int TotalAdministradores { get; set; }

        public int UsuariosActivos { get; set; }
    }
}
