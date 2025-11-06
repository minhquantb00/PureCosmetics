using PureCosmetics.AuthService.Application.Models.Responses.User;
using PureCosmetics.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Mappers
{
    public class UserMapping
    {
        public static DataUserResponse EntityToDto(User user)
        {
            return new DataUserResponse
            {
                Id = user.Id,
                IsActive = user.IsActive,
                CreationTime = user.CreationTime,
                CreatorUserId = user.CreatorUserId,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth,
                DeleterUserId = user.DeleterUserId,
                DeletionTime = user.DeletionTime,
                Email = user.Email,
                FirstName = user.FirstName,
                FullName = user.FullName,
                IsDeleted = user.IsDeleted,
                LastLoginTime = user.LastLoginTime,
                LastModificationTime = user.LastModificationTime,
                LastModifierUserId = user.LastModifierUserId,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
