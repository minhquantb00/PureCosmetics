using Microsoft.EntityFrameworkCore;
using PureCosmetics.Commons.Base;
using System;

namespace PureCosmetics.EmailService.Domain.Entities
{
    [Index(nameof(Code), IsUnique = true)]
    public class EmailTemplate : BaseEntity<int>, IActivatable, IHasCreationTime, IHasModificationTime
    {
        public string Code { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Locale { get; private set; } = string.Empty;
        public string Subject { get; private set; } = string.Empty;
        public string BodyHtml { get; private set; } = string.Empty;
        public string BodyText { get; private set; } = string.Empty;
        public string VariablesJson { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;
        public bool IsActive { get; set; }
        public int VersionNo { get; private set; }
        public DateTime CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public EmailTemplate() { }
        public EmailTemplate(string code, string name, string locale, string subject, string bodyHtml, string bodyText, string variablesJson, string category, bool isActive, int versionNo)
        {
            Code = code;
            Name = name;
            Locale = locale;
            Subject = subject;
            BodyHtml = bodyHtml;
            BodyText = bodyText;
            VariablesJson = variablesJson;
            Category = category;
            IsActive = isActive;
            VersionNo = versionNo;
        }
    }
}
