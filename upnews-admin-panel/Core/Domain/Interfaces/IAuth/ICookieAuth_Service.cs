using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Domain.Interfaces.IAuth
{
    public interface ICookieAuth_Service
    {
        Task<ClaimsPrincipal> SetCookie(Usuario usuario);
        Task<Usuario>BuscarUsuario(string username, string pass);
    }
}
