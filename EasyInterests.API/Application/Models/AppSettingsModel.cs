namespace EasyInterests.API.Application.Models
{
    public class LogLevel
    {
      public string Default { get; set; }
      public string Microsoft { get; set; }
    }
    public class Logging
    {
      public LogLevel LogLevel { get; set; }
    }
    public class AppSettingsModel
    {
      public string SecretKey { get; set; }
      public string AllowedHosts { get; set; }
      public Logging Logging { get; set; }
    }
}
