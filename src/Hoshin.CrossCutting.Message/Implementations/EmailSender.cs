using Hoshin.CrossCutting.Message.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Hoshin.CrossCutting.Logger.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;
using System.Collections.Generic;

namespace Hoshin.CrossCutting.Message.Implementations
{
    public class EmailSender : IEmailSender
    {
        private readonly ICustomLogger _logger;
        private readonly EmailSettings _emailSettings;
        public EmailSender(IOptions<EmailSettings> emailSettings,
            ICustomLogger logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public Task SendEmailAsync(string[] toEmails, string[] ccEmails, string[] bccEmails, string subject, string message, bool isBodyHtml, MailPriority mailPriority)
        {

            toEmails = toEmails.Distinct().ToArray();
            ccEmails = ccEmails.Distinct().ToArray();
            if (_emailSettings.UseSendGrid)
            {
                InitializeSendGrid(_emailSettings)
                    .SetClientSendGrid(_emailSettings.SendGridKey)
                    .SetPriority(mailPriority)
                    .IsHTML(isBodyHtml)
                    .From(_emailSettings.FromEmail)
                    .To(toEmails)
                    .Cc(ccEmails)
                    .Bcc(bccEmails)
                    .Subject(subject)
                    .Message(message)
                    .SendEmail()
                    .Wait();
            }
            else
            {
                Execute(toEmails, ccEmails, bccEmails, subject, message, isBodyHtml, mailPriority).Wait();
            }
            return Task.CompletedTask;
        }

        public async Task Execute(string[] toEmails, string[] ccEmails, string[] bccEmails, string subject, string message, bool isBodyHtml, MailPriority mailPriority)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, _emailSettings.UsernameName)
                };

                foreach (string te in toEmails)
                {
                    mail.To.Add(new MailAddress(te));
                }

                foreach (string cc in ccEmails)
                {
                    mail.CC.Add(new MailAddress(cc));
                }

                foreach (string bcc in bccEmails)
                {
                    mail.Bcc.Add(new MailAddress(bcc));
                }

                mail.Subject = subject;
                mail.Priority = mailPriority;

                mail.Body = message;
                mail.IsBodyHtml = isBodyHtml;
                //mail.Attachments.Add(new Attachment( (Server.MapPath("~/myimage.jpg")));

                using (SmtpClient smtp = new SmtpClient(_emailSettings.SecondayDomain, _emailSettings.SecondaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al intentar enviar un correo electrónico: " + ex.Message);
            }
        }

        #region Fluent SendGrid

        private string _from;
        private string _subject;
        private string _message;
        private string _userName;
        private bool _isHtml;
        private List<EmailAddress> _to;
        private List<EmailAddress> _cc;
        private List<EmailAddress> _bcc;
        private SendGridClient _client;        
        private MailPriority _priority;        

        private EmailSender(EmailSettings sendGridSettings)
        {
            _userName = sendGridSettings.UsernameName;
            _to = new List<EmailAddress>();
            _cc = new List<EmailAddress>();
            _bcc = new List<EmailAddress>();
        }

        public static EmailSender InitializeSendGrid(EmailSettings _sendGridSettings)
        {
            return new EmailSender(_sendGridSettings);
        }

        private EmailSender SetClientSendGrid(string key)
        {
            _client = new SendGridClient(key);
            return this;
        }

        private EmailSender IsHTML(bool isHtml)
        {
            _isHtml = isHtml;
            return this;
        }

        private EmailSender SetPriority(MailPriority priority)
        {
            _priority = priority;
            return this;
        }

        private async Task<EmailSender> SendEmail()
        {

            var msg = new SendGridMessage();
            if (_isHtml)
            {
                msg = new SendGridMessage()
                {
                    From = new EmailAddress(_from, _userName),
                    Subject = _subject,
                    HtmlContent = _message
                };
            }
            else
            {
                msg = new SendGridMessage()
                {
                    From = new EmailAddress(_from, _userName),
                    Subject = _subject,
                    PlainTextContent = _message
                };
            }

            msg.AddTos(_to);

            if (_cc.Count > 0)
            {
                msg.AddCcs(_cc);
            }

            if (_bcc.Count > 0)
            {
                msg.AddBccs(_bcc);
            }

            await _client.SendEmailAsync(msg);
            return this;
        }        

        private EmailSender From(string emailFrom)
        {
            _from = emailFrom;
            return this;
        }

        private EmailSender To(string[] toEmails)
        {
            _to = toEmails.Select(x => new EmailAddress
            {
                Email = x
            }).ToList();

            return this;
        }

        private EmailSender Cc(string[] ccEmails)
        {
            _cc = ccEmails.Select(x => new EmailAddress
            {
                Email = x

            }).ToList();
            return this;
        }

        private EmailSender Bcc(string[] bccEmails)
        {
            _bcc = bccEmails.Select(x => new EmailAddress
            {
                Email = x

            }).ToList();
            return this;
        }

        private EmailSender Message(string message)
        {
            _message = message;
            return this;
        }

        private EmailSender Subject(string subject)
        {
            _subject = subject;
            return this;
        }

        #endregion
    }
}
