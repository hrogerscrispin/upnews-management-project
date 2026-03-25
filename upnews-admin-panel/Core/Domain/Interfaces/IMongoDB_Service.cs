using MongoDB.Driver;
using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Domain.Interfaces
{
    public interface IMongoDB_Service
    {
        IMongoCollection<Usuario> Usuarios { get; }
        IMongoCollection<Rol> Roles { get; }
        IMongoCollection<Permiso> Permisos { get; }
    }
}
