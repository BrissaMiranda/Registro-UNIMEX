using Microsoft.AspNetCore.Mvc;
using SistemaUniversidad.Base;
using SistemaUniversidad.Services;

namespace SistemaUniversidad.Controllers
{
    public class EstudianteController : BaseController
    {
        private readonly PerfilService _perfilService;

        public EstudianteController(PerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        public async Task<IActionResult> Index()
        {
            if (!SesionIniciada())
                return RedirigirLogin();

            if (RolUsuario() != "Estudiante")
                return RedirigirLogin();

            int idUsuario = HttpContext.Session.GetInt32("IdUsuario")!.Value;

            var perfil = await _perfilService.ObtenerPerfilEstudiante(idUsuario);

            if (perfil == null)
                return RedirigirLogin();

            return View(perfil);
        }
    }
}
