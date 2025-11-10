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
    [Index(nameof(Name), IsUnique = true)]
    public class EmailProviderConfig : BaseEntity<int>, IHasCreationTime, IHasModificationTime
    {
        [Required, MaxLength(100)] public string Name { get; set; } = default!;
        public ProviderTypeEnum Type { get; set; } = ProviderTypeEnum.Smtp;

        public string SettingsJson { get; set; } = "{}"; // host/port/user or apiKey/region...
        [Required, MaxLength(320)] public string FromAddress { get; set; } = default!;
        [MaxLength(200)] public string? FromName { get; set; }

        public int? RateLimitPerMinute { get; set; }
        public bool IsDefault { get; set; }
        public bool Enabled { get; set; } = true;
        public DateTime CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
    }
}
