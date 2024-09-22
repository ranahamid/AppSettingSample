namespace AppSettingSample.Models
{
    public class SocialLoginSettings
    {
        public bool SocialLoginEnabled { get; set; }
        public KeyValueSettings Google { get; set; }
        public KeyValueSettings Microsoft { get; set; }
        public FacebookKeyValueSettings Facebook { get; set; }
        public TwitterKeyValueSettings Twitter { get; set; }
    }
    public class KeyValueSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
    public class TwitterKeyValueSettings
    {
        public string ConsumerAPIKey { get; set; }
        public string ConsumerSecret { get; set; }
    }
    public class FacebookKeyValueSettings
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }
}