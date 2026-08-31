using apitest.Interfaces;

namespace apitest.EmailLogic;

public class UserNotifier
{
    private readonly IEmailSender _emailSender;
    
    public UserNotifier(IEmailSender emailSender){
        _emailSender = emailSender;
    }

    public void Notify(int userId)
    {
        _emailSender.Send("user@mail.com", $"Hello, {userId}!");
    }
}