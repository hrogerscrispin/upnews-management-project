using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Domain.Interfaces.IAuth
{
    public interface ILogin_Service
    {
        Task<Usuario> ValidarUsuario(string email, string clave);
    }
}
