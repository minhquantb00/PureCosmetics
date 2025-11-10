using Microsoft.EntityFrameworkCore;
using PureCosmetics.Commons.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Domain.Entities
{
    [Index(nameof(OccurredAt))]
    [Index(nameof(PublishedAt))]
    public class OutboxMessage : BaseEntity<int>
    {
        [Required, MaxLength(100)] public string MessageType { get; set; } = default!;
        public string PayloadJson { get; set; } = "{}";

        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
        public DateTime? PublishedAt { get; set; }
        public int AttemptCount { get; set; }
        [MaxLength(1000)] public string? LastError { get; set; }

        [MaxLength(64)] public string? CorrelationId { get; set; }
    }
}
