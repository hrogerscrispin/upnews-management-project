using DnsClient.Protocol;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Security.Claims;
using upnews_admin_panel.Core.Domain.Interfaces;
using upnews_admin_panel.Core.Domain.Interfaces.IAuth;
using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Application.Services.Auth
{
    public class CookieAuth_Service : ICookieAuth_Service
    {
        private readonly IMongoCollection<Usuario> _usuariosCollection;
        private readonly IMongoCollection<Rol> _rolesCollecion;
        private readonly IMongoCollection<Permiso> _permisosCollection;
        //inyeccion de la dependencia Mongo service como Singleton
        public CookieAuth_Service(IMongoDB_Service mongoDB_Service)
        {
            _usuariosCollection = mongoDB_Service.Usuarios;
            _rolesCollecion = mongoDB_Service.Roles;
            _permisosCollection = mongoDB_Service.Permisos;
        }
        public async Task<ClaimsPrincipal> SetCookie(Usuario usuario)
        {
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo)  
            };

            var rol = await _rolesCollecion
                    .Find(r=>r.Id== usuario.RolId)
                    .FirstOrDefaultAsync();

            if (rol != null)
            {
                claims.Add(new Claim(ClaimTypes.Role,rol.Nombre));

                var permisos = await _permisosCollection
                        .Find(p=>rol.PermisoIds.Contains(p.Id))
                        .ToListAsync();

                foreach(var permiso in permisos)
                {
                    claims.Add(new Claim("permiso", permiso.Codigo));
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(claimsIdentity);

            return principal;
        }
    }
}
