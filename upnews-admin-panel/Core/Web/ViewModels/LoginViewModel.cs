using System.ComponentModel.DataAnnotations;

namespace upnews_admin_panel.Core.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El Usuario es requerido")]
        [EmailAddress]
        public string? Username { get; set; }

        [Required(ErrorMessage = "La Contraseña es requerida")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
