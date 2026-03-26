using System.ComponentModel.DataAnnotations;

namespace upnews_admin_panel.Core.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress]
        public required string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
