using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Domain.Interfaces
{
    public interface IRol_Service
    {
        /// <summary>
        /// Obtiene todos los roles disponibles
        /// </summary>
        Task<List<Rol>> ListarTodosLosRoles();

        /// <summary>
        /// Obtiene un rol por su ID
        /// </summary>
        Task<Rol?> ObtenerRolPorId(string rolId);

        /// <summary>
        /// Obtiene el nombre del rol por su ID
        /// </summary>
        Task<string?> ObtenerNombreRol(string rolId);
    }
}
