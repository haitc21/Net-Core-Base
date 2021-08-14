//  dotnet add package MailKit
// dotnet add package MimeKit

using System;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

public class SendMailService
{
    private readonly MailSettings _mailSettings;
    public SendMailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }
    public async Task<string> SendMailAsync(MailContent content)
    {
        var email = new MimeMessage();
        email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
        email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
        email.To.Add(new MailboxAddress(content.DisplayName, content.To));
        email.Subject = content.Subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = content.Body;
        email.Body = builder.ToMessageBody();

        using var smtpClient = new MailKit.Net.Smtp.SmtpClient();
        try
        {
            await smtpClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            Console.WriteLine("User: " + _mailSettings.Mail);
            Console.WriteLine("Pass: " + _mailSettings.Password);
            await smtpClient.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtpClient.SendAsync(email);
        }
        catch (Exception e)
        {
            Console.WriteLine("Coc gui duoc ============================================");
            return "Loi: " + e.Message;
        }

        smtpClient.Disconnect(true);
        return "Thanh cong";
    }
}