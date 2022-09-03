
namespace AFEXChile.Components
{
    public static class Credentials
    {
        public static string APIKey = System.Configuration.ConfigurationManager.AppSettings["APIKey"].ToString();
        public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
    }
}