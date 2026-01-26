using Microsoft.Extensions.Options;
using MongoDB.Driver;
using upnews_admin_panel.Core.Domain.Interfaces;
using upnews_admin_panel.Core.Domain.Models;
using upnews_admin_panel.Core.Infrastructure.Data.MongoDB;

namespace upnews_admin_panel.Core.Application.Services.MongoDB_Services
{
    //similar a un DbContext en Entity Framework
    public class MongoDB_Service : IMongoDB_Service
    {
        private readonly MongoDB_Settings _settings;
        private readonly IMongoDatabase _database;

        public MongoDB_Service(IOptions<MongoDB_Settings> settings)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(_settings.ConnectionString))
            {
                throw new InvalidOperationException("MongoDB connection string is not configured. Check `MongoDB_Settings:ConnectionString` in appsettings.json or environment variables.");
            }

            if (string.IsNullOrWhiteSpace(_settings.DatabaseName))
            {
                throw new InvalidOperationException("MongoDB database name is not configured. Check `MongoDB_Settings:DatabaseName` in appsettings.json or environment variables.");
            }

            try
            {
                var client = new MongoClient(_settings.ConnectionString);
                _database = client.GetDatabase(_settings.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to initialize MongoDB client. See inner exception for details." , ex);
            }
        }

        // colecciones MongoDB - similar a DbSets en Entity Framework
        public IMongoCollection<Usuario> Usuarios =>
            _database.GetCollection<Usuario>(this._settings.Collections.Usuarios);

        public IMongoCollection<Permiso> Permisos =>
            _database.GetCollection<Permiso>(this._settings.Collections.Permisos);

        public IMongoCollection<Rol> Roles =>
            _database.GetCollection<Rol>(this._settings.Collections.Roles);
    }
}
