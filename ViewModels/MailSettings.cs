namespace Blogly.ViewModels
{
    public class MailSettings
    {
        //Configure and use smtp server
        //from google for example
        public string? EMail { get; set; }
        public string? DisplayName { get; set; }
        public string? MailPassword { get; set; }
        public string? MailHost { get; set; }
        public int MailPort { get; set; }
    }
}
