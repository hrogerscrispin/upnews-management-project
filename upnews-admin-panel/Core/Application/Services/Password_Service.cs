using BCrypt.Net;

public class Password_Service : IPasswordService
{
    /// <summary>
    /// Encripta una contraseña usando BCrypt
    /// </summary>
    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("La contraseña no puede estar vacía", nameof(password));

        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Verifica una contraseña contra su hash
    /// </summary>
    public bool VerifyPassword(string password, string hash)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
            return false;

        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    /// <summary>
    /// Genera una contraseña: @ + primeras 3 letras del nombre en mayuscula + año actual
    /// Ejemplo: "Carlos" → "@CAR2026"
    /// </summary>
    string IPasswordService.GenerarContrasena(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre) || nombre.Length < 3)
            throw new ArgumentException("El nombre debe tener al menos 3 caracteres");

        var tresPrimeras = nombre.Substring(0, 3).ToUpper();
        var anoActual = DateTime.Now.Year;

        return $"@{tresPrimeras}{anoActual}";
    }
}