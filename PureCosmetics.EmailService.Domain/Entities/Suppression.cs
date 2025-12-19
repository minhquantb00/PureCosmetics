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
    [Index(nameof(EmailAddress),IsUnique = true)]
    [Index(nameof(Reason))]
    public class Suppression : BaseEntity<int>
    {
        [Required, MaxLength(320)] public string EmailAddress { get; set; } = default!;
        public SuppressionReasonEnum Reason { get; set; } = SuppressionReasonEnum.Unsubscribed;

        [MaxLength(50)] public string? Source { get; set; } // Webhook/Admin/User
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
    }
}
