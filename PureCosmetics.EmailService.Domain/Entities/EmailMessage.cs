using Microsoft.EntityFrameworkCore;
using PureCosmetics.Commons.Base;
using PureCosmetics.Commons.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Domain.Entities
{
    [Index(nameof(Status), nameof(ScheduleAt))]
    [Index(nameof(MessageId), IsUnique = true)]
    [Index(nameof(DeduplicationKey))]
    public class EmailMessage : BaseEntity<int>, IHasCreationTime
    {
        public Guid MessageId { get; private set; } = Guid.NewGuid();
        public string FromAddress { get; private set; } = string.Empty;
        public string FromName { get; private set; } = string.Empty;
        public string ToAddress { get; private set; } = string.Empty;
        public string? Cc { get; private set; }
        public string? Bcc { get; private set; }
        public string? ReplyTo { get; private set; }
        public string Subject { get; private set; } = string.Empty;
        public string BodyHtml { get; private set; } = string.Empty;
        public string? BodyText { get; private set; }
        public string? TemplateCode { get; private set; }
        public string? TemplateDataJson { get; private set; }
        public byte Priority { get; private set; } = 1;
        public EmailStatusEnum Status { get; private set; } = EmailStatusEnum.Queued;
        public string? Provider { get; private set; }
        public string? ProviderMessageId { get; private set; }
        public int Attempts { get; private set; }
        public int MaxAttempts { get; private set; } = 5;
        public DateTime? ScheduleAt { get; private set; }
        public DateTime? SentAt { get; private set; }
        public string? LastErrorMessage { get; private set; }
        public string? DeduplicationKey { get; private set; }
        public string? CorrelationId { get; private set; }
        public DateTime CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public EmailMessage() { }
        public EmailMessage(Guid messageId, string fromAddress, string fromName, string toAddress, string? cc, string? bcc, string? replyTo, string subject, string bodyHtml, string? bodyText, string? templateCode, string? templateDataJson, byte priority, EmailStatusEnum status, string? provider, string? providerMessageId, int attempts, int maxAttempts, DateTime? scheduleAt, DateTime? sentAt, string? lastErrorMessage, string? deduplicationKey, string? correlationId, DateTime creationTime, int? creatorUserId)
        {
            MessageId = messageId;
            FromAddress = fromAddress;
            FromName = fromName;
            ToAddress = toAddress;
            Cc = cc;
            Bcc = bcc;
            ReplyTo = replyTo;
            Subject = subject;
            BodyHtml = bodyHtml;
            BodyText = bodyText;
            TemplateCode = templateCode;
            TemplateDataJson = templateDataJson;
            Priority = priority;
            Status = status;
            Provider = provider;
            ProviderMessageId = providerMessageId;
            Attempts = attempts;
            MaxAttempts = maxAttempts;
            ScheduleAt = scheduleAt;
            SentAt = sentAt;
            LastErrorMessage = lastErrorMessage;
            DeduplicationKey = deduplicationKey;
            CorrelationId = correlationId;
            CreationTime = creationTime;
            CreatorUserId = creatorUserId;
        }

        public void MarkSending()
        {
            Status = EmailStatusEnum.Sending;
            LastErrorMessage = null;
        }

        public void MarkSent(string providerMessageId)
        {
            Status = EmailStatusEnum.Sent;
            ProviderMessageId = providerMessageId;
            SentAt = DateTime.UtcNow;
            LastErrorMessage = null;
        }

        public void Fail(string error)
        {
            Attempts++;
            LastErrorMessage = error;
            Status = EmailStatusEnum.Failed;
        }

        public void DeferWithBackoff(string error)
        {
            Attempts++;
            LastErrorMessage = error;
            Status = EmailStatusEnum.Deferred;
            var mins = Attempts switch { 1 => 1, 2 => 5, 3 => 15, _ => 60 };
            ScheduleAt = DateTime.UtcNow.AddMinutes(mins);
            Status = EmailStatusEnum.Queued;
        }
    }
}
