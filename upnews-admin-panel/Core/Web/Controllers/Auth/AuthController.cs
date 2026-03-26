using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using upnews_admin_panel.Core.Domain.Interfaces.IAuth;
using upnews_admin_panel.Core.Web.ViewModels;

namespace upnews_admin_panel.Core.Web.Controllers.Auth
{
    public class AuthController(ICookieAuth_Service cookieAuth_Service, ILogin_Service login_Service) : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            //validar usuario
            var usuario = await login_Service.ValidarUsuario(model.Username, model.Password);
            if (usuario is null)
            {
                ModelState.AddModelError("", "Credenciales inválidas. Por favor, inténtelo de nuevo.");
                return View("Login");
            }

            //crear claims
            var claims = await cookieAuth_Service.SetCookie(usuario);

            //propiedades de la cookie
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(3) : null
            };

            //crear cookie de autenticación
            await HttpContext.SignInAsync(
                "CookieAuth",
                claims,
                authProperties
            );


            //redirigir
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

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


        public IActionResult AccessDenied()=>View();
    }
}
