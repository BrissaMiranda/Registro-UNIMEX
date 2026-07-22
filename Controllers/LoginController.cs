using Microsoft.AspNetCore.Mvc;
using SistemaUniversidad.Services;
using SistemaUniversidad.ViewModels;
using Microsoft.AspNetCore.Http;

namespace SistemaUniversidad.Controllers
{
    public class LoginController : Controller
    {
        private readonly AutenticacionService _autenticacion;

        public LoginController(AutenticacionService autenticacion)
        {
            _autenticacion = autenticacion;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            var usuario = await _autenticacion.ValidarUsuario(modelo.Usuario, modelo.Password);

            if (usuario == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View(modelo);
            }

            // Guardar datos en sesión
            HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
            HttpContext.Session.SetString("Usuario", usuario.UsuarioNombre);
            HttpContext.Session.SetString("Rol", usuario.Rol!.NombreRol);

            // Si es el primer acceso
            if (usuario.PrimerAcceso)
            {
                return RedirectToAction("CambiarPassword", "Cuenta");
            }

            string rol = usuario.Rol!.NombreRol;

            switch (rol)
            {
                case "Administrador":
                    return RedirectToAction("Index", "Administrador");

                case "Estudiante":
                    return RedirectToAction("Index", "Estudiante");

                case "Docente":
                    return RedirectToAction("Index", "Docente");

                default:
                    ViewBag.Error = "El usuario no tiene un rol válido.";
                    return View(modelo);
            }
        }
    }
}
