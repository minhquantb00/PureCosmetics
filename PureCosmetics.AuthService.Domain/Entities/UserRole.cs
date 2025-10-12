using PureCosmetics.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Domain.Entities
{
    public class UserRole : BaseEntity<int> 
    {
        public int UserId { get; private set; }
        public User? User { get; set; }
        public int RoleId { get; private set; }
        public Role? Role { get; set; }
        public UserRole() { }
        public UserRole(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
