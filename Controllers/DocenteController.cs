using Microsoft.AspNetCore.Mvc;
using SistemaUniversidad.Base;
using SistemaUniversidad.Services;

// Programador: Juan Eduardo Lopez Jaime
namespace SistemaUniversidad.Controllers
{
    public class DocenteController : BaseController
    {
        private readonly PerfilService _perfilService;

        public DocenteController(PerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        public async Task<IActionResult> Index()
        {
            if (!SesionIniciada())
                return RedirigirLogin();

            if (RolUsuario() != "Docente")
                return RedirigirLogin();

            int idUsuario = HttpContext.Session.GetInt32("IdUsuario")!.Value;

            var perfil = await _perfilService.ObtenerPerfilDocente(idUsuario);

            if (perfil == null)
                return RedirigirLogin();

            return View(perfil);
        }
    }
}
