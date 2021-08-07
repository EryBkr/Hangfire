using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireExample.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task Sender(string userId, string message)
        {
            //userid ile birlikte kullanıcının email adresini alıp mail atabiliriz
            

            //appsettings.json dan api keyi aldık
            var apiKey = _config.GetSection("APIs")["SendGridApi"];
            //mail atma işlemini SendGridApi aracılığıyla yapıyoruz
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("crazyeray94@gmail.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("eray.bkr94@gmail.com", "Example User");
            var htmlContent = $"<strong>{userId} numaralı kullanıcı {message} içerikli mesaj gitmiştir</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
