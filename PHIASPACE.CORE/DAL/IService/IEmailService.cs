using System.Collections.Generic;
using System.Threading.Tasks;

namespace PHIASPACE.CORE.DAL.IService
{

    public interface IEmailService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string body, bool isHtml = false);
        Task SendEmailWithAttachmentAsync(string recipientEmail, string subject, string body, string attachmentPath, bool isHtml = false);
        Task SendTemplatedEmailAsync(string recipientEmail, string subject, string templatePath, Dictionary<string, string> templateData, bool isHtml = true);
    }
}