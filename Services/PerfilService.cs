using Microsoft.EntityFrameworkCore;
using SistemaUniversidad.Data;
using SistemaUniversidad.ViewModels;

namespace SistemaUniversidad.Services
{
    public class PerfilService
    {
        private readonly ApplicationDbContext _context;

        public PerfilService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EstudiantePerfilViewModel?> ObtenerPerfilEstudiante(int idUsuario)
        {
            return await _context.Estudiantes
                .Include(e => e.Usuario)
                .Where(e => e.IdUsuario == idUsuario)
                .Select(e => new EstudiantePerfilViewModel
                {
                    Usuario = e.Usuario!.UsuarioNombre,
                    NombreCompleto = e.Nombre + " " + e.Apellido,
                    Carnet = e.Carnet,
                    Carrera = e.Carrera,
                    Facultad = e.Facultad,
                    Correo = e.Correo,
                    Telefono = e.Telefono,
                    Edad = e.Edad,
                    VecesInicioSesion = e.Usuario!.VecesInicioSesion,
                    FechaUltimoAcceso = e.Usuario.FechaUltimoAcceso
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DocentePerfilViewModel?> ObtenerPerfilDocente(int idUsuario)
        {
            return await _context.Docentes
                .Include(d => d.Usuario)
                .Where(d => d.IdUsuario == idUsuario)
                .Select(d => new DocentePerfilViewModel
                {
                    Usuario = d.Usuario!.UsuarioNombre,
                    NombreCompleto = d.Nombre + " " + d.Apellido,
                    Facultad = d.Facultad,
                    Especialidad = d.Especialidad,
                    Correo = d.Correo,
                    Telefono = d.Telefono,
                    VecesInicioSesion = d.Usuario!.VecesInicioSesion,
                    FechaUltimoAcceso = d.Usuario.FechaUltimoAcceso
                })
                .FirstOrDefaultAsync();
        }
    }
}
