using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using upnews_admin_panel.Core.Web.ViewModels;


namespace upnews_admin_panel.Core.Web.Controllers
{
    [Authorize(Roles = "Administrador, Editor")]
    public class HomeController : Controller
    {

       private readonly IUsuario_Service usuarioService;

    public HomeController(IUsuario_Service _usuarioService)
    {
        usuarioService = _usuarioService;
    }

    public async Task<IActionResult> Index(string? moduloInicial=null)
    {
        var usuario = await usuarioService.ObtenerUsuarioLogueado(User);

        var model = new DashboardViewModel
        {
            Usuario = usuario,
           // ModuloInicial = moduloInicial ?? "dashboard"
        };

        return View(model);
    }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
