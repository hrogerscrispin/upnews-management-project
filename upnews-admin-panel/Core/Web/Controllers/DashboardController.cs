using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using upnews_admin_panel.Core.Domain.Interfaces;
using upnews_admin_panel.Core.Web.ViewModels;

namespace upnews_admin_panel.Core.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IUsuario_Service usuarioService;
        private readonly INoticia_Service noticiaService;
        private readonly IPermisos_Service permisosService;

        public DashboardController(
            IUsuario_Service _usuarioService,
            INoticia_Service _noticiaService,
            IPermisos_Service _permisosService)
        {
            usuarioService = _usuarioService;
            noticiaService = _noticiaService;
            permisosService = _permisosService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrWhiteSpace(usuarioId))
                    return Unauthorized();

                var usuario = await usuarioService.ObtenerUsuarioLogueado(User);
                if (usuario == null)
                    return Unauthorized();

                var esAdmin = await permisosService.EsAdministradorAsync(usuarioId);
                var noticias = await noticiaService.ObtenerNoticiasAsync(usuarioId);

                var model = new DashboardViewModel
                {
                    Usuario = usuario,
                    EsAdmin = esAdmin,
                    EsEditor = !esAdmin,
                    Estadisticas = new EstadisticasViewModel
                    {
                        TotalNoticias = noticias.Count,
                        NoticiasActivas = noticias.Count(n => n.Activa),
                        NoticiasInactivas = noticias.Count(n => !n.Activa),
                        TotalUsuarios = esAdmin ? await ObtenerTotalUsuariosAsync() : 0,
                        EsAdmin = esAdmin
                    },
                    NoticiasRecientes = noticias.OrderByDescending(n => n.FechaPublicacion).Take(5).ToList()
                };

                return View("../Home/Index", model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction("Login", "Auth");
            }
        }

        /// <summary>
        /// Retorna el contenido del dashboard como partial view
        /// Se carga dinámicamente en el contenedor
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> DashboardPartial()
        {
            try
            {
                var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrWhiteSpace(usuarioId))
                    return Unauthorized();

                var usuario = await usuarioService.ObtenerUsuarioLogueado(User);
                if (usuario == null)
                    return Unauthorized();

                var esAdmin = await permisosService.EsAdministradorAsync(usuarioId);
                var noticias = await noticiaService.ObtenerNoticiasAsync(usuarioId);

                var model = new DashboardViewModel
                {
                    Usuario = usuario,
                    EsAdmin = esAdmin,
                    EsEditor = !esAdmin,
                    Estadisticas = new EstadisticasViewModel
                    {
                        TotalNoticias = noticias.Count,
                        NoticiasActivas = noticias.Count(n => n.Activa),
                        NoticiasInactivas = noticias.Count(n => !n.Activa),
                        TotalUsuarios = esAdmin ? await ObtenerTotalUsuariosAsync() : 0,
                        EsAdmin = esAdmin
                    },
                    NoticiasRecientes = noticias.OrderByDescending(n => n.FechaPublicacion).Take(5).ToList()
                };

                return PartialView("_DashboardPartial", model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return BadRequest(ex.Message);
            }
        }

        private async Task<int> ObtenerTotalUsuariosAsync()
        {
            // TODO: Implementar cuando tengas un método en IUsuario_Service para obtener total
            return 0;
        }
    }
}
