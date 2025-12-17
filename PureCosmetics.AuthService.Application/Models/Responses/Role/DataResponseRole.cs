using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Models.Responses.Role
{
    /// <summary>
    /// Data response record for role
    /// User create: QuanTM
    /// Created date: 2025/12/15
    /// Last modified date: 2025/12/15
    /// </summary>
    [ProtoContract]
    public record DataResponseRole
    {
        /// <summary>
        /// Id of the role
        /// </summary>
        [ProtoMember(1)] public int Id { get; set; }

        /// <summary>
        /// Name of the role
        /// </summary>
        [ProtoMember(2)] public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Code of the role
        /// </summary>
        [ProtoMember(3)] public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Description of the role
        /// </summary>
        [ProtoMember(4)] public string Description { get; set; } = string.Empty;
    }
}
