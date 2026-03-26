using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;
using upnews_admin_panel.Core.Domain.Interfaces;
using upnews_admin_panel.Core.Domain.Interfaces.IAuth;
using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Application.Services.Auth_Services
{
    public class Login_Service : ILogin_Service
    {
        private readonly IMongoDB_Service mongoDB_Service;
        public Login_Service(IMongoDB_Service _mongoDB_Service)
        {
            this.mongoDB_Service = _mongoDB_Service;
        }

        public async Task<Usuario> ValidarUsuario(string email, string clave)
        {
            try
            {
                Console.WriteLine($"🔍 Validando usuario: {email}");
                Console.WriteLine($"🔍 mongoDB_Service null? {mongoDB_Service == null}");
                
                var usuario = await mongoDB_Service.Usuarios
                    .Find(u => u.Correo == email && u.Clave == clave && u.Activo)
                    .FirstOrDefaultAsync();

                if (usuario != null)
                    Console.WriteLine($"✓ Usuario encontrado: {usuario.Correo}");
                else
                    Console.WriteLine($"❌ Usuario NO encontrado: {email}");

                return usuario;
            }
            catch(Exception ex) 
            { 
                Console.WriteLine($"❌ ERROR en ValidarUsuario: {ex}");
                throw new ApplicationException("Error al validar el usuario: " + ex.Message);
            }
        }
    }
}
