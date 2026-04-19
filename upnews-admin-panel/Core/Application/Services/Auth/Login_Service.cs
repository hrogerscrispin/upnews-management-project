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
                .Find(u => u.Correo == email && u.Activo == true)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado en el sistema");
                return null;
            }

            if (string.IsNullOrEmpty(usuario.Clave))
            {
                Console.WriteLine("Contraseña del usuario no configurada");
                return null;
            }

            var passwordService = new Password_Service();
            bool esValida = false;

            // 🔐 1. Intentar verificar como hash
            try
            {
                esValida = passwordService.VerifyPassword(clave, usuario.Clave);
            }
            catch
            {
                // Si falla, probablemente no es un hash válido
                esValida = false;
            }

            // ⚠️ 2. Fallback a texto plano (solo temporal)
            if (!esValida)
            {
                if (usuario.Clave == clave)
                {
                    Console.WriteLine("Login con contraseña en texto plano (migrando...)");

                    // 🔥 3. Rehashear automáticamente
                    var nuevoHash = passwordService.HashPassword(clave);

                    var update = Builders<Usuario>.Update.Set(u => u.Clave, nuevoHash);
                    await usuarioCollection.UpdateOneAsync(
                        u => u.Id == usuario.Id,
                        update
                    );

                    esValida = true;
                }
            }

            if (!esValida)
            {
                Console.WriteLine("Contraseña incorrecta");
                return null;
            }

            Console.WriteLine("Usuario identificado: " + usuario.Correo);
            return usuario;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR en ValidarUsuario: {ex}");
            throw new ApplicationException("Error al validar el usuario: " + ex.Message);
        }
    }
    }
}
