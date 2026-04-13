using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using upnews_admin_panel.Core.Domain.Models;

public interface IUsuario_Service
{
    Task<Usuario?> ObtenerUsuarioLogueado(ClaimsPrincipal claimsUsuario);
    Task<UsuarioViewModel?> CrearNuevoUsuario(UsuarioViewModel model);

    Task<UsuarioViewModel?> EditarUsuario(string Id, UsuarioViewModel model);
}