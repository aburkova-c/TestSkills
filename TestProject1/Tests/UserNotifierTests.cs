using apitest.EmailLogic;
using apitest.Interfaces;
using FluentAssertions;

namespace apitest;

public class FakeEmailSenser : IEmailSender
{
    public string? To;
    public string? Text;
    public int CallCount;

    public void Send(string to, string text)
    {
        To = to;
        Text = text;
        CallCount++;
    }
}

public class UserNotifierTests
{
[Test]
    public void Notify_SendOnce()
    {
        var fakeEmailSenser = new FakeEmailSenser();
        var notifier = new UserNotifier(fakeEmailSenser);
        
        notifier.Notify(1);
        
        fakeEmailSenser.CallCount.Should().Be(1);
    }
}

