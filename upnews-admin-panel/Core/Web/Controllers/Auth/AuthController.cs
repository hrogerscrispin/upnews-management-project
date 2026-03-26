using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using upnews_admin_panel.Core.Domain.Interfaces.IAuth;

namespace upnews_admin_panel.Core.Web.Controllers.Auth
{
    public class AuthController(ICookieAuth_Service cookieAuth_Service, ILogin_Service login_Service) : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string returnUrl, bool rememberMe)
        {

            //validar usuario
            var usuario = await login_Service.ValidarUsuario(username, password);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Credenciales inválidas. Por favor, inténtelo de nuevo.");
                return View("Login");
            }

            //crear claims
            var claims = await cookieAuth_Service.SetCookie(usuario);

            //propiedades de la cookie
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = rememberMe ? DateTimeOffset.UtcNow.AddDays(3) : null
            };

            //crear cookie de autenticación
            await HttpContext.SignInAsync(
                "CookieAuth",
                claims,
                authProperties
            );


            //redirigir
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                "CookieAuth"
            );
            return RedirectToAction("Index");
        }
    }
}
