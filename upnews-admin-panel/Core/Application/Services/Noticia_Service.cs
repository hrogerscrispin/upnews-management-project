using MongoDB.Driver;
using upnews_admin_panel.Core.Domain.Interfaces;
using upnews_admin_panel.Core.Domain.Models;
using upnews_admin_panel.Core.Web.ViewModels;

namespace upnews_admin_panel.Core.Application.Services
{
    public class Noticia_Service : INoticia_Service
    {
        private readonly IMongoCollection<Noticia> noticiaCollection;
        private readonly IPermisos_Service permisosService;

        public Noticia_Service(IMongoDB_Service mongoService, IPermisos_Service _permisosService)
        {
            noticiaCollection = mongoService.Noticias;
            permisosService = _permisosService;
        }

        /// <summary>
        /// Crear noticia (solo usuarios autenticados)
        /// </summary>
        public async Task<Noticia?> CrearNoticiaAsync(string usuarioId, NoticiaViewModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuarioId))
                    throw new UnauthorizedAccessException("Debe estar autenticado");

                var nuevaNoticia = new Noticia
                {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Titulo = model.Titulo,
                    Descripcion = model.Descripcion,
                    Contenido = model.Contenido,
                    AutorId = usuarioId,
                    CategoriaId = model.CategoriaId,
                    PaisId = model.PaisId,
                    EstadoId = model.EstadoId,
                    FechaPublicacion = DateTime.UtcNow,
                    Activa = true
                };

                await noticiaCollection.InsertOneAsync(nuevaNoticia);
                return nuevaNoticia;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear noticia: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Editar noticia (Admin: cualquiera, Editor: solo suyas)
        /// </summary>
        public async Task<Noticia?> EditarNoticiaAsync(string usuarioId, string noticiaId, NoticiaViewModel model)
        {
            try
            {
                var noticia = await noticiaCollection.Find(n => n.Id == noticiaId).FirstOrDefaultAsync();
                if (noticia == null)
                    throw new KeyNotFoundException("Noticia no encontrada");

                if(string.IsNullOrEmpty(noticia.AutorId))
                    throw new InvalidOperationException("La noticia no tiene un autor asignado.");

                if (!await permisosService.PuedeEditarNoticiaAsync(usuarioId, noticia.AutorId))
                    throw new UnauthorizedAccessException("No tiene permiso para editar esta noticia");

                var actualizaciones = Builders<Noticia>.Update
                    .Set(n => n.Titulo, model.Titulo)
                    .Set(n => n.Descripcion, model.Descripcion)
                    .Set(n => n.Contenido, model.Contenido)
                    .Set(n => n.CategoriaId, model.CategoriaId)
                    .Set(n => n.PaisId, model.PaisId)
                    .Set(n => n.EstadoId, model.EstadoId);

                var resultado = await noticiaCollection.FindOneAndUpdateAsync(
                    n => n.Id == noticiaId,
                    actualizaciones,
                    new FindOneAndUpdateOptions<Noticia> { ReturnDocument = ReturnDocument.After }
                );

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar noticia: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Eliminar noticia (soft delete - marcar como inactiva)
        /// </summary>
        public async Task<bool> EliminarNoticiaAsync(string usuarioId, string noticiaId)
        {
            try
            {
                var noticia = await noticiaCollection.Find(n => n.Id == noticiaId).FirstOrDefaultAsync();
                if (noticia == null)
                    throw new KeyNotFoundException("Noticia no encontrada");

                if(string.IsNullOrEmpty(noticia.AutorId))
                    throw new InvalidOperationException("La noticia no tiene un autor asignado.");

                if (!await permisosService.PuedeEditarNoticiaAsync(usuarioId, noticia.AutorId))
                    throw new UnauthorizedAccessException("No tiene permiso para eliminar esta noticia");

                var resultado = await noticiaCollection.UpdateOneAsync(
                    n => n.Id == noticiaId,
                    Builders<Noticia>.Update.Set(n => n.Activa, false)
                );

                return resultado.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar noticia: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtener noticias según rol
        /// </summary>
        public async Task<List<Noticia>> ObtenerNoticiasAsync(string usuarioId)
        {
            try
            {
                if (await permisosService.EsAdministradorAsync(usuarioId))
                {
                    return await noticiaCollection
                        .Find(n => n.Activa == true)
                        .ToListAsync();
                }

                return await noticiaCollection
                    .Find(n => n.AutorId == usuarioId && n.Activa == true)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener noticias: {ex.Message}");
                throw;
            }
        }

        public async Task<Noticia?> ObtenerNoticiaPorIdAsync(string noticiaId)
        {
            return await noticiaCollection.Find(n => n.Id == noticiaId && n.Activa == true).FirstOrDefaultAsync();
        }
    }
}
