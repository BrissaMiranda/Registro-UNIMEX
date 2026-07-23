// Programador: Pamela Saray Chavez Fernandez

namespace SistemaUniversidad.ViewModels
{
    public class EstudiantePerfilViewModel
    {
        public string Usuario { get; set; } = "";
        
        public string NombreCompleto { get; set; } = "";

        public string Carnet { get; set; } = "";

        public string Carrera { get; set; } = "";

        public string Facultad { get; set; } = "";

        public string Correo { get; set; } = "";

        public string Telefono { get; set; } = "";

        public int Edad { get; set; }

        public int VecesInicioSesion { get; set; }

        public DateTime? FechaUltimoAcceso { get; set; }
    }
}
