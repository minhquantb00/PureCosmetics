using PureCosmetics.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Domain.Entities
{
    public class RefreshToken : BaseEntity<int>
    {
        public int UserId { get; private set; }
        public User? User { get; set; }
        public string Token { get; private set; } = string.Empty;
        public DateTime ExpireTime { get; private set; }
        public DateTime? RevokedTime { get; private set; }
        public RefreshToken() { }
        public RefreshToken(int userId, string token, DateTime expireTime)
        {
            UserId = userId;
            Token = token;
            ExpireTime = DateTime.Now.AddHours(8);
            ExpireTime = expireTime;
            RevokedTime = null;
        }
    }
}
