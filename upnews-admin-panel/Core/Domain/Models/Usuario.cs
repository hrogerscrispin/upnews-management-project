using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace upnews_admin_panel.Core.Domain.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("correo")]
        public string Correo { get; set; }

        [BsonElement("clave")]
        public string Clave { get; set; }

        [BsonElement("rolId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RolId { get; set; }

        [BsonElement("fechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [BsonElement("activo")]
        public bool Activo { get; set; }

        [BsonIgnore]
        public Rol Rol { get; set; }
    }
}
