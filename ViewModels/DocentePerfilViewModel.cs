namespace SistemaUniversidad.ViewModels
{
    public class DocentePerfilViewModel
    {
        public string Usuario { get; set; } = "";
        
        public string NombreCompleto { get; set; } = "";

        public string Facultad { get; set; } = "";

        public string Especialidad { get; set; } = "";

        public string Correo { get; set; } = "";

        public string Telefono { get; set; } = "";

        public int VecesInicioSesion { get; set; }

        public DateTime? FechaUltimoAcceso { get; set; }
    }
}
