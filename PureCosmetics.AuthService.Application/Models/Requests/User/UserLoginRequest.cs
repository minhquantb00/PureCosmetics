using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Models.Requests.User
{
    [ProtoContract]
    public class UserLoginRequest
    {
        [ProtoMember(1)] public string UserName { get; set; } = string.Empty;
        [ProtoMember(2)] public string Password { get; set; } = string.Empty;
    }
}
