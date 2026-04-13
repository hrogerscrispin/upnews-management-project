using System.ComponentModel.DataAnnotations;

public class UsuarioViewModel
{
    [Required(ErrorMessage = "El campo 'nombre' es requerido.")]
    public required string  Nombre { get; set; }
    
    [EmailAddress]
    [Required(ErrorMessage = "El campo 'correo' es requerido.")]
    public required string Correo {get; set; }

    public  string? Contrasena{get; set; }

    [Required(ErrorMessage = "El campo 'rol' es requerido.")]
    public required int RolId   {get; set; }

}