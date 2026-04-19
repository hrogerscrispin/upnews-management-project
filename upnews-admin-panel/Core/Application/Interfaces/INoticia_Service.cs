using upnews_admin_panel.Core.Domain.Models;
using upnews_admin_panel.Core.Web.ViewModels;

namespace upnews_admin_panel.Core.Domain.Interfaces
{
    public interface INoticia_Service
    {
        /// <summary>
        /// Crear noticia (solo usuarios autenticados)
        /// </summary>
        Task<Noticia?> CrearNoticiaAsync(string usuarioId, NoticiaViewModel model);

        /// <summary>
        /// Editar noticia (respetando permisos)
        /// </summary>
        Task<Noticia?> EditarNoticiaAsync(string usuarioId, string noticiaId, NoticiaViewModel model);

        /// <summary>
        /// Eliminar/ocultar noticia (soft delete respetando permisos)
        /// </summary>
        Task<bool> EliminarNoticiaAsync(string usuarioId, string noticiaId);

        /// <summary>
        /// Obtener noticias según permisos del usuario
        /// </summary>
        Task<List<Noticia>> ObtenerNoticiasAsync(string usuarioId);

        /// <summary>
        /// Obtener una noticia por ID
        /// </summary>
        Task<Noticia?> ObtenerNoticiaPorIdAsync(string noticiaId);
    }
}
