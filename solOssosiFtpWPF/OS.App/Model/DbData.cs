namespace SC.Model.Common.Connection
{
    public enum DbEngine
    {
        MsSQL = 0,
        MySQL = 1
    }

    public class DbData
    {
        public string Name { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DbEngine Engine { get; set; } = DbEngine.MsSQL;
    }
}
