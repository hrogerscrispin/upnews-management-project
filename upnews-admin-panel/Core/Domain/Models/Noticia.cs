using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace upnews_admin_panel.Core.Domain.Models
{
    public class Noticia
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("titulo")]
        public string? Titulo { get; set; }

        [BsonElement("descripcion")]
        public string? Descripcion { get; set; }

        [BsonElement("contenido")]
        public string? Contenido { get; set; }

        [BsonElement("autorId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? AutorId { get; set; }

        [BsonElement("categoriaId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CategoriaId { get; set; }

        [BsonElement("paisId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PaisId { get; set; }

        [BsonElement("estadoId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? EstadoId { get; set; }

        [BsonElement("fechaPublicacion")]
        public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;

        [BsonElement("activa")]
        public bool Activa { get; set; } = true;

        [BsonIgnore]
        public Usuario? Autor { get; set; }
    }
}
