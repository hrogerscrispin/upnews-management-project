using System.Security.Claims;
using MongoDB.Bson;
using MongoDB.Driver;
using upnews_admin_panel.Core.Domain.Interfaces;
using upnews_admin_panel.Core.Domain.Models;

public class Usuario_Service : IUsuario_Service
{
    private readonly IMongoCollection<Usuario> usuarioCollection;
    private readonly IMongoCollection<Rol> rolCollection;
    private readonly IPasswordService passwordService;

    public Usuario_Service(IMongoDB_Service _mongoService, IPasswordService _passwordService)
    {
        usuarioCollection = _mongoService.Usuarios;
        passwordService = _passwordService;
        rolCollection = _mongoService.Roles;
    }


    public async Task<Usuario?> ObtenerUsuarioLogueado(ClaimsPrincipal claimsUsuario)
    {
        var usuarioId = claimsUsuario.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(string.IsNullOrWhiteSpace(usuarioId)) return null;

        return await usuarioCollection
                .Find(u=>u.Id == usuarioId)
                .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Lista todos los usuarios registrados (activos e inactivos)
    /// </summary>
    public async Task<List<Usuario>> ListarTodosLosUsuarios()
    {
        try
        {
            var usuarios = await usuarioCollection
                .Find(u => true)
                .ToListAsync(); 

            var resultado = new List<UsuarioViewModel>();

            foreach(var usuario in usuarios)
            {
                usuario.Rol = await rolCollection
                .Find(r => r.Id == usuario.RolId)
                .FirstOrDefaultAsync();

                resultado.Add(new UsuarioViewModel
                {
                    Nombre = usuario.Nombre ?? string.Empty,
                    Correo = usuario.Correo ?? string.Empty,
                    RolId = usuario.RolId ?? string.Empty,
                    RolNombre = usuario.Rol.Nombre
                    
                });
            }
            
            return usuarios;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al listar usuarios: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Crea un nuevo usuario con ViewModel, contraseña autogenerada y siempre activo
    /// Contraseña: @ + primeras 3 letras del nombre en mayuscula + año actual
    /// </summary>
    public async Task<UsuarioViewModel?> CrearNuevoUsuario(UsuarioViewModel model)
    {
        try
        {
            // 1. Validar que el correo sea único
            var usuarioExistente = await usuarioCollection
                .Find(u => u.Correo == model.Correo)
                .FirstOrDefaultAsync();

            if (usuarioExistente != null)
                throw new InvalidOperationException($"El correo '{model.Correo}' ya está registrado");

            // 2. Generar contraseña automáticamente: @ + primeras 3 letras + año actual
            var passwordGenerada = passwordService.GenerarContrasena(model.Nombre);

            // 3. Encriptar la contraseña usando el PasswordService
            var claveEncriptada = passwordService.HashPassword(passwordGenerada);

            // 4. Crear el modelo de dominio desde el ViewModel
            var nuevoUsuario = new Usuario
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Nombre = model.Nombre,
                Correo = model.Correo,
                Clave = claveEncriptada, 
                RolId = model.RolId.ToString(),
                FechaCreacion = DateTime.UtcNow, 
                Activo = true 
            };

            // 5. Insertar en MongoDB
            await usuarioCollection.InsertOneAsync(nuevoUsuario);

            // 6. Mapear a ViewModel para retornar (SIN contraseña por seguridad)
            var respuesta = new UsuarioViewModel
            {
                Nombre = nuevoUsuario.Nombre,
                Correo = nuevoUsuario.Correo,
                RolId = nuevoUsuario.RolId
                
            };

            return respuesta;
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error de validación al crear usuario: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear usuario: {ex.Message}");
            throw;
        }
    }


    // todo: validar logica de soft-delete, permitir cambio de estado a inactivo.. 
    public async Task<UsuarioViewModel?> EditarUsuario(string Id, UsuarioViewModel model)
    {
        try{

            var usuarioExistente = await usuarioCollection
            .Find(x=>x.Id == Id)
            .FirstOrDefaultAsync();

            if (usuarioExistente == null)
                throw new InvalidOperationException($"No se ha encontrado el usuario con ID {Id}");

           var correoExistente = await usuarioCollection
                .Find(u => u.Correo == model.Correo && u.Id != Id)
                .FirstOrDefaultAsync();

            
            if (correoExistente != null)
                throw new InvalidOperationException($"El correo {model.Correo} ya está en uso.");

            var usuarioActualizado = Builders<Usuario>.Update
                .Set(x=>x.Nombre, model.Nombre)
                .Set(x=>x.Correo, model.Correo)
                .Set(x=>x.RolId, model.RolId.ToString());;

            if(!string.IsNullOrEmpty(model.Contrasena)){
                var encriptada = passwordService.HashPassword(model.Contrasena);
                usuarioActualizado = usuarioActualizado
                    .Set(x=>x.Clave,encriptada); 
            }


            var resultado = await usuarioCollection.FindOneAndUpdateAsync(
                x=>x.Id == Id,
                usuarioActualizado,
                new FindOneAndUpdateOptions<Usuario>{ReturnDocument = ReturnDocument.After}
            );

            if (resultado is null)
                throw new InvalidOperationException("No se pudo actualizar la informacion del usuario");


             

            var respuesta = new UsuarioViewModel{
                Nombre = resultado.Nombre ?? string.Empty, //**HACK: solucion temporal
                Correo = resultado.Correo ?? string.Empty, //**HACK: solucion temporal
                RolId = resultado.RolId ?? string.Empty,
            };


            return respuesta;

        }catch(Exception ex){
            Console.WriteLine($"Error al actualizar los datos del usuario. {ex.Message}");
            throw;
        }
    }

    public async Task<Usuario?> ObtenerUsuarioPorId(string Id)
    {
        try
        {
            
            var usuario = await usuarioCollection
                .Find(x=>x.Id == Id)
                .FirstOrDefaultAsync();


            if(usuario != null)
            {
                usuario.Rol = await rolCollection
                    .Find(x=>x.Id == usuario.RolId)
                    .FirstOrDefaultAsync();
            }

            return usuario;

        }catch(Exception ex)
        {
            System.Console.WriteLine($"Error al obtener el usuario especificado: {ex.Message}");
            throw;
        }
    }
}