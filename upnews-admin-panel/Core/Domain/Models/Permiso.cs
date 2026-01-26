using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace upnews_admin_panel.Core.Domain.Models
{
    public class Permiso
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("codigo")]
        public string Codigo { get; set; }

        [BsonElement("descripcion")]
        public string Descripcion { get; set; }
    }
}
