using MongoDB.Driver;
using upnews_admin_panel.Core.Domain.Interfaces;
using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Application.Services
{
    public class Rol_Service : IRol_Service
    {
        private readonly IMongoCollection<Rol> rolesCollection;

        public Rol_Service(IMongoDB_Service mongoService)
        {
            rolesCollection = mongoService.Roles;
        }

        /// <summary>
        /// Obtiene todos los roles disponibles
        /// </summary>
        public async Task<List<Rol>> ListarTodosLosRoles()
        {
            try
            {
                return await rolesCollection
                    .Find(r => true)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar roles: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un rol por su ID
        /// </summary>
        public async Task<Rol?> ObtenerRolPorId(string rolId)
        {
            try
            {
                return await rolesCollection
                    .Find(r => r.Id == rolId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener rol: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene el nombre del rol por su ID
        /// </summary>
        public async Task<string?> ObtenerNombreRol(string rolId)
        {
            try
            {
                var rol = await ObtenerRolPorId(rolId);
                return rol?.Nombre;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener nombre del rol: {ex.Message}");
                throw;
            }
        }
    }
}
