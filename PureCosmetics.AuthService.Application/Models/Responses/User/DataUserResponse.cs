using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Models.Responses.User
{
    [ProtoContract]
    public record DataUserResponse
    {
        [ProtoMember(1)] public int Id { get; set; }
        [ProtoMember(2)] public string? AvatarUrl { get; set; }
        [ProtoMember(3)] public string Email { get; set; }
        [ProtoMember(4)] public string PhoneNumber { get; set; }
        [ProtoMember(5)] public string UserName { get; set; }
        [ProtoMember(6)] public string PasswordHash { get; set; }
        [ProtoMember(7)] public string PasswordSalt { get; set; }
        [ProtoMember(8)] public string FirstName { get; set; }
        [ProtoMember(9)] public string LastName { get; set; }
        [ProtoMember(10)] public string FullName { get; set; }
        [ProtoMember(11)] public DateTime DateOfBirth { get; set; }
        [ProtoMember(12)] public DateTime? LastLoginTime { get; set; }
        [ProtoMember(13)] public DateTime CreationTime { get; set; }
        [ProtoMember(14)] public int? CreatorUserId { get; set; }
        [ProtoMember(15)] public DateTime? LastModificationTime { get; set; }
        [ProtoMember(16)] public int? LastModifierUserId { get; set; }
        [ProtoMember(17)] public bool IsDeleted { get; set; }
        [ProtoMember(18)] public DateTime? DeletionTime { get; set; }
        [ProtoMember(19)] public int? DeleterUserId { get; set; }
        [ProtoMember(20)] public bool IsActive { get; set; }
    }
    [ProtoContract]
    public record DataResponseLogin
    {
        [ProtoMember(1)] public string AccessToken { get; set; } = string.Empty;
        [ProtoMember(2)] public string RefreshToken { get; set; } = string.Empty;
    }
}
