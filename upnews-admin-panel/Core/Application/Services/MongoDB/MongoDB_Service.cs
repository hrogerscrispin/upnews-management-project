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

            Console.WriteLine($"🔍 DEBUG - _settings: {(_settings == null ? "NULL" : "OK")}");
            Console.WriteLine($"🔍 DEBUG - Collections: {(_settings?.Collections == null ? "NULL" : "OK")}");
            Console.WriteLine($"🔍 DEBUG - Usuarios value: '{_settings?.Collections?.Usuarios ?? "NULL"}'");
            Console.WriteLine($"🔍 DEBUG - Roles value: '{_settings?.Collections?.Roles ?? "NULL"}'");
            Console.WriteLine($"🔍 DEBUG - Permisos value: '{_settings?.Collections?.Permisos ?? "NULL"}'");

            if (string.IsNullOrWhiteSpace(_settings?.ConnectionString))
            {
                throw new InvalidOperationException("MongoDB connection string is not configured. Check `MongoDB_Settings:ConnectionString` in appsettings.json or environment variables.");
            }

            if (string.IsNullOrWhiteSpace(_settings.DatabaseName))
            {
                throw new InvalidOperationException("MongoDB database name is not configured. Check `MongoDB_Settings:DatabaseName` in appsettings.json or environment variables.");
            }

            if (_settings.Collections == null)
            {
                throw new InvalidOperationException("Collections configuration is NULL. Check `MongoDB_Settings:Collections` in appsettings.json");
            }

            try
            {
                var client = new MongoClient(_settings.ConnectionString);
                _database = client.GetDatabase(_settings.DatabaseName);

                Console.WriteLine($"✓ Conectado a MongoDB: {_settings.DatabaseName}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to initialize MongoDB client. See inner exception for details." , ex);
            }
        }

        // colecciones MongoDB - similar a DbSets en Entity Framework
        public IMongoCollection<Usuario> Usuarios
        {
            get
            {
                Console.WriteLine($"🔍 Accediendo a Usuarios - _database es null? {_database == null}");
                
                if (_database == null)
                    throw new InvalidOperationException("_database es NULL en MongoDB_Service");
                
                if (string.IsNullOrWhiteSpace(_settings.Collections.Usuarios))
                    throw new InvalidOperationException("Colección 'Usuarios' no configurada en appsettings.json");
                
                Console.WriteLine($"✓ Obteniendo colección: {_settings.Collections.Usuarios}");
              //  Console.Write($"Usuario identificado: {}");
                return _database.GetCollection<Usuario>(_settings.Collections.Usuarios);
            }
        }

        public IMongoCollection<Permiso> Permisos
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_settings.Collections.Permisos))
                    throw new InvalidOperationException("Colección 'Permisos' no configurada");
                
                return _database.GetCollection<Permiso>(_settings.Collections.Permisos);
            }
        }

        public IMongoCollection<Rol> Roles
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_settings.Collections.Roles))
                    throw new InvalidOperationException("Colección 'Roles' no configurada");
                
                return _database.GetCollection<Rol>(_settings.Collections.Roles);
            }
        }
    }
}
