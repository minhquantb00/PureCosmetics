using PureCosmetics.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Domain.Entities
{
    public class Address : BaseEntity<int>, IHasCreationTime, IHasModificationTime
    {
        public string AddressUser {  get; private set; } = string.Empty;
        public int UserId { get; private set; }
        public virtual User? User { get; private set; }
        public string? CustomerName { get; private set; } = string.Empty;
        public string? PhoneNumber { get; private set; } = string.Empty;
        public DateTime CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }

        public Address() { }
        public Address(string addressUser, int userId, string? customerName, string? phoneNumber,  int? creatorUserId = null)
        {
            AddressUser = addressUser;
            UserId = userId;
            CustomerName = customerName;
            PhoneNumber = phoneNumber;
            CreationTime = DateTime.Now;
            CreatorUserId = creatorUserId;
            LastModificationTime = null;
            LastModifierUserId = null;
        }

        public void Change(string addressUser, string? customerName, string? phoneNumber, int? lastModifierUserId = null)
        {
            AddressUser = addressUser;
            CustomerName = customerName;
            PhoneNumber = phoneNumber;
            LastModificationTime = DateTime.Now;
            LastModifierUserId = lastModifierUserId;
        }
    }
}
