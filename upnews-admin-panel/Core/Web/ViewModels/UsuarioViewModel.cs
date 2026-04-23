using System.ComponentModel.DataAnnotations;
using upnews_admin_panel.Core.Domain.Models;

public class UsuarioViewModel
{

    public string? Id { get; set; } // null = crear, con valor = editar
    
    [Required(ErrorMessage = "El campo 'nombre' es requerido.")]
    public required string  Nombre { get; set; }

    public bool? Activo{get; set;}
    
    [EmailAddress]
    [Required(ErrorMessage = "El campo 'correo' es requerido.")]
    public required string Correo {get; set; }

    public string? Contrasena {get;set;}

    [Required(ErrorMessage = "El campo 'rol' es requerido.")]
    public required string RolId { get; set; }

    public string? RolNombre{get;set;}

    // Lista de roles disponibles para el dropdown (no se serializa)
    public List<Rol> RolesDisponibles { get; set; } = new();
}