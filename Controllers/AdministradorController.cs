using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaUniversidad.Data;
using SistemaUniversidad.Models;
using SistemaUniversidad.ViewModels;
using SistemaUniversidad.Base;

namespace SistemaUniversidad.Controllers
{ 
    public class AdministradorController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AdministradorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTA DE USUARIOS
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SesionIniciada())
                {
                    return RedirigirLogin();
                }

                if (RolUsuario() != "Administrador")
                {
                    return RedirigirLogin();
                }

           var usuarios = await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Estudiante)
                .Include(u => u.Docente)
                .Select(u => new UsuarioViewModel
                {
                    IdUsuario = u.IdUsuario,

                    Usuario = u.UsuarioNombre,

                    NombreCompleto =
                        u.Estudiante != null
                            ? u.Estudiante.Nombre + " " + u.Estudiante.Apellido
                        : u.Docente != null
                            ? u.Docente.Nombre + " " + u.Docente.Apellido
                        : "Administrador",

                    Rol = u.Rol!.NombreRol,

                    Activo = u.Activo,

                    VecesInicioSesion = u.VecesInicioSesion,

                    FechaUltimoAcceso = u.FechaUltimoAcceso,

                    PrimerAcceso = u.PrimerAcceso
                })
                .ToListAsync();

            var modelo = new AdministradorViewModel
            {
                Usuarios = usuarios,

                TotalUsuarios = await _context.Usuarios.CountAsync(),

                TotalEstudiantes = await _context.Estudiantes.CountAsync(),

                TotalDocentes = await _context.Docentes.CountAsync(),

                TotalAdministradores = await _context.Usuarios
                    .CountAsync(u => u.IdRol == 1),

                UsuariosActivos = await _context.Usuarios
                    .CountAsync(u => u.Activo)
            };

            return View(modelo);
        }

        // FORMULARIO CREAR USUARIO
        [HttpGet]
        public async Task<IActionResult> CrearUsuario()
        {
            var modelo = new CrearUsuarioViewModel();

            await CargarListas(modelo);

            return View(modelo);
        }

        // GUARDAR USUARIO
        [HttpPost]
        public async Task<IActionResult> CrearUsuario(CrearUsuarioViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                await CargarListas(modelo);
                return View(modelo);
            }

            // Verificar que el nombre de usuario no exista
            bool existe = await _context.Usuarios
                .AnyAsync(u => u.UsuarioNombre == modelo.Usuario);

            if (existe)
            {
                ModelState.AddModelError("", "El nombre de usuario ya existe.");

                await CargarListas(modelo);

                return View(modelo);
            }

            var usuario = new Usuario
            {
                UsuarioNombre = modelo.Usuario,
                Password = "12345",
                PrimerAcceso = true,
                VecesInicioSesion = 0,
                FechaCreacion = DateTime.Now,
                FechaUltimoAcceso = null,
                Activo = modelo.Activo,
                IdRol = modelo.IdRol
            };

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            // Si es estudiante
            if (modelo.IdRol == 2 && modelo.IdEstudiante.HasValue)
            {
                var estudiante = await _context.Estudiantes
                    .FirstOrDefaultAsync(e => e.IdEstudiante == modelo.IdEstudiante);

                if (estudiante == null)
                {
                    ModelState.AddModelError("", "El estudiante no existe.");
                    await CargarListas(modelo);
                    return View(modelo);
                }

                if (estudiante.IdUsuario != null)
                {
                    ModelState.AddModelError("", "Este estudiante ya tiene un usuario asignado.");
                    await CargarListas(modelo);
                    return View(modelo);
                }

                estudiante.IdUsuario = usuario.IdUsuario;
            }

            // Si es docente
            if (modelo.IdRol == 3 && modelo.IdDocente.HasValue)
            {
                var docente = await _context.Docentes
                    .FirstOrDefaultAsync(d => d.IdDocente == modelo.IdDocente);

                if (docente == null)
                {
                    ModelState.AddModelError("", "El docente no existe.");
                    await CargarListas(modelo);
                    return View(modelo);
                }

                if (docente.IdUsuario != null)
                {
                    ModelState.AddModelError("", "Este docente ya tiene un usuario asignado.");
                    await CargarListas(modelo);
                    return View(modelo);
                }

                docente.IdUsuario = usuario.IdUsuario;
            }

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Usuario registrado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // CARGAR LISTAS
        private async Task CargarListas(CrearUsuarioViewModel modelo)
        {

            modelo.Roles.Add(new SelectListItem
            {
                Value = "",
                Text = "-- Seleccione un rol --"
            });

            modelo.Roles.AddRange(await _context.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.IdRol.ToString(),
                    Text = r.NombreRol
                })
                .ToListAsync());
            
            modelo.Estudiantes.Add(new SelectListItem
            {
                Value = "",
                Text = "-- Seleccione un Estudiante --"
            });

            modelo.Estudiantes.AddRange(await _context.Estudiantes
                .Where(e => e.IdUsuario == null)
                .Select(e => new SelectListItem
                {
                    Value = e.IdEstudiante.ToString(),
                    Text = e.Nombre + " " + e.Apellido
                })
                .ToListAsync());
            
            modelo.Docentes.Add(new SelectListItem
            {
                Value = "",
                Text = "-- Seleccione un Docente --"
            });

            modelo.Docentes.AddRange(await _context.Docentes
                .Where(d => d.IdUsuario == null)
                .Select(d => new SelectListItem
                {
                    Value = d.IdDocente.ToString(),
                    Text = d.Nombre + " " + d.Apellido
                })
                .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> CambiarEstado(int id)
        {
            if (!SesionIniciada())
                return RedirigirLogin();

            if (RolUsuario() != "Administrador")
                return RedirigirLogin();

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            usuario.Activo = !usuario.Activo;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Estado del usuario actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RestablecerPassword(int id)
        {
            if (!SesionIniciada())
                return RedirigirLogin();

            if (RolUsuario() != "Administrador")
                return RedirigirLogin();

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            usuario.Password = "12345";
            usuario.PrimerAcceso = true;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "La contraseña fue restablecida correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
