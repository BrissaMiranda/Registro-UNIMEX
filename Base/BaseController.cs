using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SistemaUniversidad.Base
{
    public class BaseController : Controller
    {
        protected bool SesionIniciada()
        {
            return HttpContext.Session.GetInt32("IdUsuario") != null;
        }

        protected string RolUsuario()
        {
            return HttpContext.Session.GetString("Rol") ?? "";
        }

        protected IActionResult RedirigirLogin()
        {
            return RedirectToAction("Index", "Login");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            base.OnActionExecuting(context);
        }
    }
}
