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
        
        //inyeccion de la dependencia Mongo service como Singleton
        public CookieAuth_Service(IMongoDB_Service mongoDB_Service)
        {
            _usuariosCollection = mongoDB_Service.Usuarios;
        }

        public async Task<Usuario> BuscarUsuario(string username, string pass)
        {
            try
            {
                return await _usuariosCollection
                    .Find(u => u.Correo == username && u.Clave == pass && u.Activo)
                    .FirstOrDefaultAsync();

            }catch(Exception ex)
            {
                throw new Exception("Error al validar el usuario: "+ex.Message);
            }
        }

        public Task<ClaimsPrincipal> SetCookie(Usuario usuario)
        {
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo)  
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(claimsIdentity);

            return Task.FromResult(principal);
        }
    }
}
