namespace OnTest.Blazor.Settings
{
    public class AppConfig
    {
        public string BaseAddress { get; set; }
        public string OAuthGoogleUrl => BaseAddress + "/oauth/google";
        public string OAuthGitHubUrl => BaseAddress + "/oauth/github";
        public string OAuthLinkedInUrl => BaseAddress + "/oauth/linkedin";
    }
}