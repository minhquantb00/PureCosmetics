using PureCosmetics.Commons.Base;
using PureCosmetics.Commons.Encrypts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Domain.Entities
{
    public class User : BaseEntity<int>, IHasCreationTime, IHasModificationTime, IDeletable, IActivatable
    {
        public string? AvatarUrl { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string UserName { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string PasswordSalt { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public DateTime DateOfBirth { get; private set; }
        public DateTime? LastLoginTime { get; private set; }
        public DateTime CreationTime { get;  set; }
        public int? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public bool IsActive { get; set; }

        public User() { }
        public User(string email, string phoneNumber, string userName, string password, string firstName, string lastName, DateTime dateOfBirth, int numericalOrder, string avatarUrl, int? creatorUserId = null)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            UserName = userName;
            PasswordSalt = BCrypt.Net.BCrypt.GenerateSalt(12);
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, PasswordSalt);
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            LastLoginTime = null;
            CreationTime = DateTime.Now;
            IsDeleted = false;
            IsActive = true;
            LastModificationTime = null;
            LastModifierUserId = null;
            DeletionTime = null;
            DeleterUserId = null;
            NumericalOrder = numericalOrder;
            AvatarUrl = avatarUrl;
        }

        public void Change(int id, string email, string phoneNumber, string userName, string firstName, string lastName, DateTime dateOfBirth)
        {
            Id = id;
            Email = email;
            PhoneNumber = phoneNumber;
            UserName = userName;
            PhoneNumber = phoneNumber;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            LastModificationTime = DateTime.Now;
        }

        public void ChangeAvatar(string avatarUrl)
        {
            AvatarUrl = avatarUrl;
        }
    }
}
