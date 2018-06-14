//---------------------------------------------------------------------------------------
// Description: service for email related functions
//---------------------------------------------------------------------------------------
using AppServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using AppServices.Interfaces;

namespace AppServices.Services
{
    public class EmailServices : IEmailServices
    {
        /// <summary>
        /// send email to app devs
        /// </summary>
        /// <param name="input"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(EmailDtoInput input, IConfiguration config)
        {
            try
            {
                var apiKey = config.GetSection("MailSettings")["SendGridApiKey"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(input.FromEmail, config.GetSection("MailSettings")["FromAlias"]);
                var subject = input.Subject;
                var emailAddressArray = config.GetSection("MailSettings")["AdminEmail"].Split(",");
                List<EmailAddress> toEmailAddress = new List<EmailAddress>();
                foreach (var email in emailAddressArray)
                {
                    toEmailAddress.Add(new EmailAddress(email));
                }
                var htmlContent = input.Message;
                var plainText = input.Message;
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, toEmailAddress, subject,plainText, htmlContent);
                var response = await client.SendEmailAsync(msg);
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
