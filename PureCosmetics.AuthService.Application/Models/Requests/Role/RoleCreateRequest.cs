using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Models.Requests.Role
{
    /// <summary>
    /// Record for role create request
    /// User create: QuanTM
    /// Created date: 2025/12/15
    /// Last modified date: 2025/12/15
    /// </summary>
    [ProtoContract]
    public record RoleCreateRequest
    {
        /// <summary>
        /// Name of the role
        /// </summary>
        [ProtoMember(1)] public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Code of the role
        /// </summary>
        [ProtoMember(2)] public string Code { get; private set; } = string.Empty;

        /// <summary>
        /// Description of the role
        /// </summary>
        [ProtoMember(3)] public string Description { get; private set; } = string.Empty;
    }
}
