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
    [Index(nameof(EmailAddress), nameof(OccurredAt))]
    public class BounceLog : BaseEntity<int>
    {
        [Key] public long Id { get; set; }

        [Required, MaxLength(320)] public string EmailAddress { get; set; } = default!;
        [MaxLength(50)] public string? Provider { get; set; }
        [MaxLength(200)] public string? ProviderMessageId { get; set; }

        public BounceTypeEnum BounceType { get; set; } = BounceTypeEnum.Hard;
        [MaxLength(200)] public string? DiagnosticCode { get; set; }
        public string RawPayloadJson { get; set; } = "{}";

        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }
}
