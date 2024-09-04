

using CatalystAPI.Interfaces;


namespace CatalsytAPI.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = "cloudadmin@mycompany.com";
        private string _mailFrom = "cloudnoreply@mycompany.com";

        public void Send(string subject, string message){
            // Dummy implementation
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with {nameof(CloudMailService)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}