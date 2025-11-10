using Microsoft.Extensions.DependencyInjection;
using PureCosmetics.EmailService.Domain.RepositoryContracts;
using PureCosmetics.EmailService.Infrastructure.RepositoryImplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Infrastructure.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IBounceLogRepository, BounceLogRepository>();
            services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<IEmailMessageRepository, EmailMessageRepository>();
            services.AddScoped<IEmailProviderConfigRepository, EmailProviderConfigRepository>();
            services.AddScoped<ISuppressionRepository, SuppressionRepository>();
            services.AddScoped<IInboxMessageRepository, InboxMessageRepository>();
            services.AddScoped<IEmailAttachmentRepository, EmailAttachmentRepository>();
            return services;
        }
    }
}
