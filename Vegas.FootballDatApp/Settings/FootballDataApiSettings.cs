namespace Vegas.FootballDatApp.Settings
{
    public class FootballDataApiSettings : IFootballDataApiSettings
    {
        public string Endpoint { get; set; }

        public string Version { get; set; }

        public string Token { get; set; }
    }

    public interface IFootballDataApiSettings
    {
        string Endpoint { get; set; }

        string Version { get; set; }

        string Token { get; set; }
    }
}
