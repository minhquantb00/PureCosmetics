using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Models.Requests.Address
{
    [ProtoContract]
    public record AddressCreateRequest
    {
        [ProtoMember(1)] public string AddressUser { get; set; } = string.Empty;
        [ProtoMember(2)] public int UserId { get; set; }
        [ProtoMember(3)] public string CustomerName { get; set; } = string.Empty;
        [ProtoMember(4)] public string PhoneNumber { get; set; } = string.Empty;
    }

    [ProtoContract]
    public record AddressUpdateRequest
    {
        [ProtoMember(1)] public int Id { get; set; }
        [ProtoMember(2)] public string AddressUser { get; set; } = string.Empty;
        [ProtoMember(3)] public int UserId { get; set; }
        [ProtoMember(4)] public string CustomerName { get; set; } = string.Empty;
        [ProtoMember(5)] public string PhoneNumber { get; set; } = string.Empty;
    }

    [ProtoContract]
    public record AddressDeleteRequest
    {
        [ProtoMember(1)] public required int Id { get; set; }
    }
}
