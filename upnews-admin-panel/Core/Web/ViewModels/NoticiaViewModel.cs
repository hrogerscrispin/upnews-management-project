using System.ComponentModel.DataAnnotations;

namespace upnews_admin_panel.Core.Web.ViewModels
{
    public class NoticiaViewModel
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(200)]
        public required string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [StringLength(40, ErrorMessage = "La descripción no puede exceder 40 caracteres")]
        public required string Descripcion { get; set; }

        [Required(ErrorMessage = "El contenido es requerido")]
        public required string Contenido { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        public required string CategoriaId { get; set; }

        [Required(ErrorMessage = "El país es requerido")]
        public required string PaisId { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        public required string EstadoId { get; set; }
    }
}
