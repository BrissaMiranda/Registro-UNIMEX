using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaUniversidad.Base;
using SistemaUniversidad.Data;
using SistemaUniversidad.ViewModels;

namespace SistemaUniversidad.Controllers
{
    public class CuentaController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CuentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult CambiarPassword()
        {
            if (!SesionIniciada())
                return RedirigirLogin();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarPassword(CambiarPasswordViewModel modelo)
        {
            if (!SesionIniciada())
                return RedirigirLogin();

            if (!ModelState.IsValid)
                return View(modelo);

            int idUsuario = HttpContext.Session.GetInt32("IdUsuario")!.Value;

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            if (usuario == null)
                return RedirigirLogin();

            if (usuario.Password != modelo.PasswordActual)
            {
                ModelState.AddModelError("", "La contraseña actual es incorrecta.");
                return View(modelo);
            }

            if (modelo.PasswordActual == modelo.PasswordNueva)
            {
                ModelState.AddModelError("", "La nueva contraseña debe ser diferente.");
                return View(modelo);
            }

            usuario.Password = modelo.PasswordNueva;
            usuario.PrimerAcceso = false;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Contraseña actualizada correctamente.";

            switch (usuario.IdRol)
            {
                case 1:
                    return RedirectToAction("Index", "Administrador");

                case 2:
                    return RedirectToAction("Index", "Estudiante");

                case 3:
                    return RedirectToAction("Index", "Docente");

                default:
                    return RedirigirLogin();
            }
        }
    }
}
