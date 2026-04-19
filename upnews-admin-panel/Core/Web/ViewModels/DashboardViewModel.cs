using upnews_admin_panel.Core.Domain.Models;

namespace upnews_admin_panel.Core.Web.ViewModels
{
    public class DashboardViewModel
    {
        public Usuario? Usuario { get; set; }
        public bool EsAdmin { get; set; }
        public bool EsEditor { get; set; }
        public EstadisticasViewModel? Estadisticas { get; set; }
        public List<Noticia> NoticiasRecientes { get; set; } = new();
    }

    public class EstadisticasViewModel
    {
        public int TotalNoticias { get; set; }
        public int NoticiasActivas { get; set; }
        public int NoticiasInactivas { get; set; }
        public int TotalUsuarios { get; set; }
        public bool EsAdmin { get; set; }
    }
}
