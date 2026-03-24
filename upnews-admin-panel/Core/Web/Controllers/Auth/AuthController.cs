using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using upnews_admin_panel.Core.Domain.Interfaces.IAuth;

namespace upnews_admin_panel.Core.Web.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly ICookieAuth_Service cookieAuth_Service;
        private readonly ILogin_Service login_Service;
        public AuthController(ICookieAuth_Service _cookieAuthService,ILogin_Service _login_Service)
        {
            this.cookieAuth_Service = _cookieAuthService;
            this.login_Service = _login_Service;
        }
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string returnUrl)
        {

            //validar usuario
            var usuario = await login_Service.ValidarUsuario(username, password);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Credenciales inválidas. Por favor, inténtelo de nuevo.");
                return View("Login");
            }

            //crear claims
            var claims = await cookieAuth_Service.CreateUserCookie(usuario);

            //crear cookie de autenticación
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claims
            );


            //redirigir
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");

        }
    }
}
