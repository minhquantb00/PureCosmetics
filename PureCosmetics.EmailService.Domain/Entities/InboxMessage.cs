using Microsoft.EntityFrameworkCore;
using PureCosmetics.Commons.Base;
using PureCosmetics.Commons.Enumerates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Domain.Entities
{
    [Index(nameof(Status), nameof(ReceivedAt))]
    [Index(nameof(DeduplicationKey))]
    public class InboxMessage : BaseEntity<int>
    {
        [Required, MaxLength(100)] public string MessageType { get; set; } = default!; // OrderPlaced...
        public string PayloadJson { get; set; } = "{}";

        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }
        public InboxProcessStatusEnum Status { get; set; } = InboxProcessStatusEnum.Pending;

        [MaxLength(1000)] public string? Error { get; set; }
        [MaxLength(64)] public string? CorrelationId { get; set; }
        [MaxLength(100)] public string? DeduplicationKey { get; set; }
    }
}
