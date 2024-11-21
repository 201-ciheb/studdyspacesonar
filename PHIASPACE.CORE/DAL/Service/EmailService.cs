using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.Utility;

namespace PHIASPACE.CORE.DAL.Service;
public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string recipientEmail, string subject, string body, bool isHtml = false)
    {
        await SendEmailInternalAsync(recipientEmail, subject, body, isHtml);
    }

    public async Task SendEmailWithAttachmentAsync(string recipientEmail, string subject, string body, string attachmentPath, bool isHtml = false)
    {
        await SendEmailInternalAsync(recipientEmail, subject, body, isHtml, attachmentPath);
    }

    public async Task SendTemplatedEmailAsync(string recipientEmail, string subject, string templatePath, Dictionary<string, string> templateData, bool isHtml = true)
    {
        string template = await System.IO.File.ReadAllTextAsync(templatePath);
        string body = ReplaceTemplateTokens(template, templateData);
        await SendEmailInternalAsync(recipientEmail, subject, body, isHtml);
    }

    private async Task SendEmailInternalAsync(string recipientEmail, string subject, string body, bool isHtml, string attachmentPath = null)
    {
        using (var message = new MailMessage())
        {
            message.From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);
            message.To.Add(new MailAddress(recipientEmail));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            if (!string.IsNullOrEmpty(attachmentPath))
            {
                try
                {
                    Attachment attachment = new Attachment(attachmentPath);
                    message.Attachments.Add(attachment);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to add attachment: {ex.Message}");
                    throw;
                }
            }

            using (var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                try
                {
                    await client.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                    throw;
                }
            }
        }
    }

    private string ReplaceTemplateTokens(string template, Dictionary<string, string> templateData)
    {
        foreach (var kvp in templateData)
        {
            template = template.Replace($"{{{kvp.Key}}}", kvp.Value);
        }
        return template;
    }
}
