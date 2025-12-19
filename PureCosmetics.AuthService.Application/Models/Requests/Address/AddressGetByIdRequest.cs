using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Models.Requests.Address
{
    [ProtoContract]
    public record AddressGetByIdRequest
    {
        [ProtoMember(1)] public required int Id { get; set; }
    }

    [ProtoContract]
    public record AddressGetsRequest
    {
        [ProtoMember(1)] public string? Keyword { get; set; } = string.Empty;
        [ProtoMember(2)] public int PageIndex { get; set; } = 1;
        [ProtoMember(3)] public int PageSize { get; set; } = 10;
    }

    [ProtoContract]
    public record AddressGetByUserIdRequest
    {
        [ProtoMember(1)] public required int UserId { get; set; }

        [ProtoMember(2)] public int PageSize { get; set; } = 10;
        [ProtoMember(3)] public int PageIndex { get; set; } = 1;
    }
}
