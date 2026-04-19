using Microsoft.AspNetCore.Mvc;
using upnews_admin_panel.Core.Domain.Models;
using upnews_admin_panel.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using upnews_admin_panel.Core.Domain.Constants;


[Authorize(Roles = Roles.Admin)]
public class UsuarioController : Controller
{
    private readonly IUsuario_Service usuarioService;
    private readonly IRol_Service rolService;

    private readonly IPermisos_Service permisoService;

    public UsuarioController(IUsuario_Service _usuarioService, IRol_Service _rolService, IPermisos_Service _permisosService)
    {
        usuarioService = _usuarioService;
        rolService = _rolService;
        permisoService = _permisosService;
    }


    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var usuarios = await usuarioService.ListarTodosLosUsuarios();
            var roles = await rolService.ListarTodosLosRoles();

            // Pasar los roles a través de ViewData para que estén disponibles en la vista
            ViewData["Roles"] = roles;
            
            // Si es una solicitud AJAX, retornar partial view
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ListarPartial", usuarios);
            }
            
            // Si es una solicitud normal, retornar la partial view de igual forma
            // ya que se cargará dinámicamente desde el dashboard
            return PartialView("_ListarPartial", usuarios);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar la lista de usuarios: {ex.Message}");
            ViewData["Roles"] = new List<Rol>();
            return PartialView("_ListarPartial", new List<Usuario>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> FormCrear()
    {
        try
        {

            var roles = await rolService.ListarTodosLosRoles();
            var model = new UsuarioViewModel
            {
                Nombre = string.Empty, // **hack: prueba
                Correo = string.Empty,
                RolId = string.Empty,
                RolesDisponibles = roles
            };

            return PartialView("_FormUsuario", model);
            
        }catch(Exception ex)
        {
            System.Console.WriteLine($"Ha ocurrido un error al crear el usuario: {ex.Message}");
            return PartialView("", ex);
        }
    }

    [HttpGet]

    public async Task<IActionResult> FormEditar(string Id)
    {
        try
        {
            var usuario = await usuarioService.ObtenerUsuarioPorId(Id);
            if(usuario is null) 
                return NotFound();

            var roles = await rolService.ListarTodosLosRoles();  
            var model = new UsuarioViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre ?? string.Empty,
                Correo = usuario.Correo ?? string.Empty,
                RolId = usuario.RolId ?? string.Empty,
                RolesDisponibles = roles
            };

            return PartialView("_FormUsuario", model);
        }
        catch (Exception ex)
        {
            return PartialView("", ex);
        }
    }

}