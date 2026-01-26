namespace upnews_admin_panel.Core.Infrastructure.Data.MongoDB
{
    public class MongoDB_Settings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;

        public Collections_Settings Collections { get; set; } = new();
    }
}
