using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Models.Requests.Role
{
    /// <summary>
    /// Record for role remove request
    /// </summary>
    [ProtoContract]
    public record RoleRemoveRequest
    {
        /// <summary>
        /// Id of the role to be removed
        /// </summary>
        [ProtoMember(1)] public int Id { get; set; }
    }
}
