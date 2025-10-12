using PureCosmetics.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Domain.Entities
{
    public class RolePermission : BaseEntity<int>
    {
        public int RoleId { get; private set; }
        public Role? Role { get; set; }
        public int PermissionId { get; private set; }
        public Permission? Permission { get; set; }
        public RolePermission() { }
        public RolePermission(int roleId, int permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}
