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
        private readonly IMongoCollection<Usuario> usuarioCollection;
        public Login_Service(IMongoDB_Service _mongoDB_Service)
        {
            usuarioCollection = _mongoDB_Service.Usuarios;
        }

        public async Task<Usuario?> ValidarUsuario(string email, string clave)
        {
            try
            {
                var usuario = await usuarioCollection
                    .Find(u=>u.Correo == email && u.Clave == clave && u.Activo).FirstOrDefaultAsync();

                    if (usuario != null) 
                        System.Console.WriteLine("Usuario identificado: "+usuario.Correo);
                    else
                        System.Console.WriteLine("Usuario no encontrado en el sistema");

                
                return usuario; 
            }
            catch(Exception ex) 
            { 
                Console.WriteLine($"ERROR en ValidarUsuario: {ex}");
                throw new ApplicationException("Error al validar el usuario: " + ex.Message);
            }
        }
    }
}
