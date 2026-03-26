using MongoDB.Bson.Serialization.Attributes;

namespace upnews_admin_panel.Core.Domain.Models
{
    public class Rol
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("permisos")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public List<string> PermisoIds { get; set; } = new();

        //[BsonIgnore]
        //public List<string> Permiso { get; set; }
    }
}
