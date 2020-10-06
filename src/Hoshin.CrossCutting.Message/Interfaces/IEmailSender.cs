using System.Net.Mail;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.Message.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string[] toEmails, string[] ccEmails, string[] bccEmails, string subject, string message, bool isBodyHtml, MailPriority mailPriority);
        //bool SendEmailAsync(string[] toEmails, string[] ccEmails, string[] bccEmails, string subject, string message, bool isBodyHtml, MailPriority mailPriority);
    }
}
