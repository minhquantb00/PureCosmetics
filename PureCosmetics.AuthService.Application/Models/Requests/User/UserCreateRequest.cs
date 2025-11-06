using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Models.Requests.User
{
    [ProtoContract]
    public record UserCreateRequest
    {
        [ProtoMember(1)] public string Email { get; set; } = string.Empty;
        [ProtoMember(2)] public string PhoneNumber { get; set; } = string.Empty;
        [ProtoMember(3)] public string UserName { get; set; } = string.Empty;
        [ProtoMember(4)] public string Password { get; set; } = string.Empty;
        [ProtoMember(5)] public string FirstName { get; set; } = string.Empty;
        [ProtoMember(6)] public string LastName { get; set; } = string.Empty;
        [ProtoMember(7)] public DateTime DateOfBirth { get; set; }
    }

    [ProtoContract]
    public record UserUpdateRequest
    {
        [ProtoMember(1)] public int Id { get; set; }
        [ProtoMember(2)] public string Email { get; set; } = string.Empty;
        [ProtoMember(3)] public string PhoneNumber { get; set; } = string.Empty;
        [ProtoMember(4)] public string UserName { get; set; } = string.Empty;
        [ProtoMember(5)] public string FirstName { get; set; } = string.Empty;
        [ProtoMember(6)] public string LastName { get; set; } = string.Empty;
        [ProtoMember(7)] public DateTime DateOfBirth { get; set; }
    }
}
