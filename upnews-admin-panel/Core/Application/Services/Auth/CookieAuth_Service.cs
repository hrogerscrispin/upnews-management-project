using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using upnews_admin_panel.Core.Domain.Interfaces.IAuth;
using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Application.Services.Auth
{
    public class CookieAuth_Service : ICookieAuth_Service
    {
      
        public async Task<ClaimsPrincipal> CreateUserCookie(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim(ClaimTypes.Name, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.RolId)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(identity);
        }
    }
}
