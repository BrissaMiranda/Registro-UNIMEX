namespace SistemaUniversidad.ViewModels
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }

        public string Usuario { get; set; } = "";

        public string NombreCompleto { get; set; } = "";

        public string Rol { get; set; } = "";

        public bool Activo { get; set; }

        public int VecesInicioSesion { get; set; }

        public DateTime? FechaUltimoAcceso { get; set; }

        public bool PrimerAcceso { get; set; }
    }
}
