using Hoshin.CrossCutting.Logger.Interfaces;
using Hoshin.CrossCutting.Message.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hoshin.CrossCutting.WorkflowCore.GenericSteps
{
    public class SendEmail : StepBodyAsync
    {
        private readonly IEmailSender _emailSender;
        private readonly ICustomLogger _logger;
        public SendEmail(IEmailSender emailSender, ICustomLogger logger)
        {
            _emailSender = emailSender;
            _logger = logger;
        }

        public List<string> EmailAddresses { get; set; }
        public List<string> CcEmailAddresses { get; set; }
        public List<string> BccEmailAddresses { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }

        public MailPriority MailPriority { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
                if (EmailAddresses == null)
                {
                    EmailAddresses = new List<string>();
                }
                if (CcEmailAddresses == null)
                {
                    CcEmailAddresses = new List<string>();
                }
                if (BccEmailAddresses == null)
                {
                    BccEmailAddresses = new List<string>();
                }
            //_logger.LogInformation("STEP: SendEmail");
            //var x = _emailSender.SendEmailAsync(EmailAddresses.ToArray(), CcEmailAddresses.ToArray(), BccEmailAddresses.ToArray(), EmailSubject, EmailMessage, IsBodyHtml, MailPriority);
            Task.Run(() =>
                _emailSender.SendEmailAsync(EmailAddresses.ToArray(), CcEmailAddresses.ToArray(), BccEmailAddresses.ToArray(), EmailSubject, EmailMessage, true, MailPriority));
            
            return ExecutionResult.Next();
        }
    }
}