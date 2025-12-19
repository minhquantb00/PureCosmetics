using Microsoft.EntityFrameworkCore;
using PureCosmetics.EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Infrastructure.ORM
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public ApplicationDbContext() { }
        public DbSet<BounceLog> BounceLogs { get; set; }
        public DbSet<InboxMessage> InboxMessages { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }
        public DbSet<EmailAttachment> EmailAttachments { get; set; }
        public DbSet<EmailProviderConfig> EmailProviderConfigs { get; set; }
        public DbSet<Suppression> Subpressions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EmailAttachment>()
                .HasOne(ea => ea.EmailMessage)
                .WithMany()
                .HasForeignKey(ea => ea.EmailMessageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
