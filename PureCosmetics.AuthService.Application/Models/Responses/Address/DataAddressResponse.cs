using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Models.Responses.Address
{
    [ProtoContract]
    public record DataAddressResponse
    {
        [ProtoMember(1)] public int Id { get; set; }
        [ProtoMember(2)] public string AddressUser { get; set; }
        [ProtoMember(3)] public int UserId { get; set; }
        [ProtoMember(4)] public DateTime CreationTime { get; set; }
        [ProtoMember(5)] public int? CreatorUserId { get; set; }
        [ProtoMember(6)] public DateTime? LastModificationTime { get; set; }
        [ProtoMember(7)] public int? LastModifierUserId { get; set; }
        [ProtoMember(8)] public int NumericalOrder { get; set; }
        [ProtoMember(9)] public string CustomerName { get; set; }
        [ProtoMember(10)] public string PhoneNumber { get; set; }
    }
}
