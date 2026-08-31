using apitest.Interfaces;

namespace apitest.EmailLogic;

public class EmailSender : IEmailSender
{
    public void Send(string to, string text)
    {
        Console.WriteLine($"Sending mail to {to}: {text}"); 
    }
}