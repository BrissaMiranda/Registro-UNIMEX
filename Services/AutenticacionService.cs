using Microsoft.EntityFrameworkCore;
using SistemaUniversidad.Data;
using SistemaUniversidad.Models;

namespace SistemaUniversidad.Services
{
    public class AutenticacionService
    {
        private readonly ApplicationDbContext _context;

        public AutenticacionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ValidarUsuario(string usuario, string password)
        {
            var usuarioEncontrado = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u =>
                    u.UsuarioNombre == usuario &&
                    u.Password == password &&
                    u.Activo);

            if (usuarioEncontrado == null)
                return null;

            usuarioEncontrado.VecesInicioSesion++;

            usuarioEncontrado.FechaUltimoAcceso = DateTime.Now;

            await _context.SaveChangesAsync();

            return usuarioEncontrado;
        }
    }
}
