using PureCosmetics.Commons.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Domain.Entities
{
    public class EmailAttachment : BaseEntity<int>
    {
        [ForeignKey(nameof(EmailMessage))] public int EmailMessageId { get; set; }
        public EmailMessage EmailMessage { get; set; } = default!;

        [Required, MaxLength(255)] public string FileName { get; set; } = default!;
        [MaxLength(128)] public string? ContentType { get; set; }
        public long Length { get; set; }

        [MaxLength(1000)] public string? StorageUrl { get; set; } // hoặc BlobId nếu dùng blob
        public bool IsInline { get; set; }
        [MaxLength(200)] public string? ContentId { get; set; } // dùng cho inline images
        [MaxLength(64)] public string? Sha256 { get; set; }
    }
}
