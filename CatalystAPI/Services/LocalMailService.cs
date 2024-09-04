
using System;


namespace CatalsytAPI.Services;

public interface IMailService
{
    void Send (string subject, string message);
}


public class LocalMailService : IMailService
{
    private string _mailTo = "admin@mycompany.com";
    private string _mailFrom = "noreply@mycompany.com";

    public void Send(string subject, string message){
        // Dummy implementation
        Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with {nameof(LocalMailService)}.");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Message: {message}");
    }
}