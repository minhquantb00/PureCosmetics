using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Models.Requests.Role
{
    /// <summary>
    /// Record for role get, get list request
    /// User create: QuanTM
    /// Created date: 2025/12/15
    /// Last modified date: 2025/12/15
    /// </summary>
    [ProtoContract]
    public record RoleGetRequest
    {
        [ProtoMember(1)] public int Id { get; set; }
    }

    [ProtoContract]
    public record RoleGetsRequest
    {
        /// <summary>
        /// Keyword to search roles
        /// </summary>
        [ProtoMember(1)] public string? Keyword { get; set; }

        /// <summary>
        /// Page index for pagination
        /// </summary>
        [ProtoMember(2)] public int PageIndex { get; set; }

        /// <summary>
        /// Page size for pagination
        /// </summary>
        [ProtoMember(3)] public int PageSize { get; set; }
    }
}
