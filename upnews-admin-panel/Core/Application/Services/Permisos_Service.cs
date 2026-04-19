using MongoDB.Driver;
using upnews_admin_panel.Core.Application.Services;
using upnews_admin_panel.Core.Domain.Interfaces;
using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Application.Services
{
    public class Permisos_Service : IPermisos_Service
    {
        private readonly IMongoCollection<Usuario> usuarioCollection;
        private readonly IMongoCollection<Rol> rolCollection;

        public Permisos_Service(IMongoDB_Service mongoService)
        {
            usuarioCollection = mongoService.Usuarios;
            rolCollection = mongoService.Roles;
        }

        /// <summary>
        /// Obtiene el nombre del rol del usuario
        /// </summary>
        public async Task<string?> ObtenerRolUsuarioAsync(string usuarioId)
        {
            var usuario = await usuarioCollection.Find(u => u.Id == usuarioId).FirstOrDefaultAsync();
            if (usuario == null) return null;

            var rol = await rolCollection.Find(r => r.Id == usuario.RolId).FirstOrDefaultAsync();
            return rol?.Nombre;
        }

        /// <summary>
        /// Verifica si el usuario es administrador
        /// </summary>
        public async Task<bool> EsAdministradorAsync(string usuarioId)
        {
            var rolNombre = await ObtenerRolUsuarioAsync(usuarioId);
            return rolNombre?.ToLower() == "administrador";
        }

        /// <summary>
        /// Verifica si es editor
        /// </summary>
        public async Task<bool> EsEditorAsync(string usuarioId)
        {
            var rolNombre = await ObtenerRolUsuarioAsync(usuarioId);
            return rolNombre?.ToLower() == "editor";
        }

        /// <summary>
        /// Verifica si puede editar/eliminar una noticia
        /// Regla: Admin siempre, Editor solo sus propias
        /// </summary>
        public async Task<bool> PuedeEditarNoticiaAsync(string usuarioId, string noticiaAutorId)
        {
            // Si es admin, siempre puede
            if (await EsAdministradorAsync(usuarioId)) return true;

            // Si es editor, solo sus propias
            return usuarioId == noticiaAutorId;
        }
    }
}
