namespace WebApp.Strategy.Models
{
    public class Settings
    {
        public static string claimDatabaseType = "databasetype";
        public EDatabaseType DatabaseType;
        public EDatabaseType getDefaultDatabaseType => EDatabaseType.SqlServer;

    }
}
