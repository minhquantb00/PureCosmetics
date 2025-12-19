using PureCosmetics.EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Domain.RepositoryContracts
{
    public interface IEmailAttachmentRepository
    {
        Task CreateAsync(EmailAttachment emailAttachment);
        Task UpdateAsync(EmailAttachment emailAttachment);
        Task<bool> DeleteAsync(int id);
    }
}
