using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Domain.Interfaces
{
    public interface IPermisos_Service
    {
        /// <summary>
        /// Obtiene el nombre del rol del usuario
        /// </summary>
        Task<string?> ObtenerRolUsuarioAsync(string usuarioId);

        /// <summary>
        /// Verifica si el usuario es administrador
        /// </summary>
        Task<bool> EsAdministradorAsync(string usuarioId);

        /// <summary>
        /// Verifica si es editor
        /// </summary>
        Task<bool> EsEditorAsync(string usuarioId);

        /// <summary>
        /// Verifica si puede editar/eliminar una noticia
        /// Regla: Admin siempre, Editor solo sus propias
        /// </summary>
        Task<bool> PuedeEditarNoticiaAsync(string usuarioId, string noticiaAutorId);
    }
}
