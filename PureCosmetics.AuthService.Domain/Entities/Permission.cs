using PureCosmetics.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Domain.Entities
{
    public class Permission : BaseEntity<int>
    {
        public string Name { get; private set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public Permission() { }
        public Permission(string name, string code, string description)
        {
            Name = name;
            Code = code;
            Description = description;
        }
    }
}
