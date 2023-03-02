namespace IoneVectronConverter.Common.Config
{
    public class Settings{

        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public VectronSettings Vectron { get; set; }

    }

    public class ConnectionStrings
    {
        public string Default { get; set; }
    }

    public class Logging
    {
        public LogLevel Loglevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string AspNetCore { get; set; }
    }
}
