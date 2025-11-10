using Microsoft.Extensions.Options;
using PureCosmetics.EmailService.Application.Ports.Providers;
using PureCosmetics.EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Infrastructure.Providers
{
    public sealed class SmtpOptions
    {
        public string Host { get; set; } = default!;
        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string? User { get; set; }
        public string? Password { get; set; }
        public string FromAddress { get; set; } = default!;
        public string? FromName { get; set; }
        public string MessageIdDomain { get; set; } = "email.local";
    }

    public sealed class SmtpEmailProvider : IEmailProvider
    {
        private readonly SmtpOptions _opt;
        public SmtpEmailProvider(IOptions<SmtpOptions> opt) => _opt = opt.Value;

        public async Task<(bool ok, string? providerMessageId, string? error)>
            SendAsync(EmailMessage msg, CancellationToken ct)
        {
            using var client = new SmtpClient(_opt.Host, _opt.Port)
            {
                EnableSsl = _opt.EnableSsl,
                Credentials = string.IsNullOrWhiteSpace(_opt.User)
                    ? null
                    : new NetworkCredential(_opt.User, _opt.Password)
            };

            var fromAddr = string.IsNullOrWhiteSpace(msg.FromAddress) ? _opt.FromAddress : msg.FromAddress;
            var fromName = string.IsNullOrWhiteSpace(msg.FromName) ? _opt.FromName : msg.FromName;

            using var mail = new MailMessage
            {
                From = new MailAddress(fromAddr, fromName),
                Subject = msg.Subject ?? string.Empty,
                Body = string.IsNullOrEmpty(msg.BodyHtml) ? (msg.BodyText ?? string.Empty) : msg.BodyHtml,
                IsBodyHtml = !string.IsNullOrWhiteSpace(msg.BodyHtml)
            };

            mail.To.Add(msg.ToAddress);
            if (!string.IsNullOrWhiteSpace(msg.ReplyTo)) mail.ReplyToList.Add(msg.ReplyTo);
            AddMany(mail.CC, msg.Cc);
            AddMany(mail.Bcc, msg.Bcc);

            if (!string.IsNullOrWhiteSpace(msg.BodyHtml) && !string.IsNullOrWhiteSpace(msg.BodyText))
            {
                mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(msg.BodyText!, null, MediaTypeNames.Text.Plain));
                mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(msg.BodyHtml!, null, MediaTypeNames.Text.Html));
            }

            var messageId = $"<{msg.MessageId}@{_opt.MessageIdDomain}>";
            mail.Headers.Add("Message-ID", messageId);

            try
            {
                using var reg = ct.Register(() => client.SendAsyncCancel());
                await client.SendMailAsync(mail);
                return (true, messageId, null);
            }
            catch (SmtpException ex) { return (false, null, ex.Message); }
            catch (Exception ex) { return (false, null, ex.Message); }
        }

        private static void AddMany(MailAddressCollection col, string? csv)
        {
            if (string.IsNullOrWhiteSpace(csv)) return;
            foreach (var e in csv.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var addr = e.Trim();
                if (addr.Length > 0) col.Add(addr);
            }
        }
    }

}
