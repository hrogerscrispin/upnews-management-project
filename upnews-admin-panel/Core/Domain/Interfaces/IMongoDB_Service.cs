using MongoDB.Driver;
using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Domain.Interfaces
{
    public class IMongoDB_Service
    {
        public IMongoCollection<Usuario> Usuarios { get; }
        public IMongoCollection<Rol> Roles { get; }
        public IMongoCollection<Permiso> Permisos{ get; }


    }
}
