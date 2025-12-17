using PureCosmetics.AuthService.Application.Models.Responses.Role;
using PureCosmetics.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Mappers
{
    public static class RoleMapping
    {
        public static DataResponseRole EntityToDto(this Role entity)
        {
            return new DataResponseRole
            {
                Id = entity.Id,
                Code = entity.Code,
                Description = entity.Description,
                Name = entity.Name
            };
        }
    }
}
