using PureCosmetics.AuthService.Application.Models.Responses.Address;
using PureCosmetics.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Mappers
{
    /// <summary>
    /// Mapping class for address
    /// User create: QuanTM
    /// </summary>
    public class AddressMapping
    {
        /// <summary>
        /// Map from Entity address to Dto address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static DataAddressResponse EntityToDto(Address address)
        {
            return new DataAddressResponse
            {
                AddressUser = address.AddressUser,
                CreationTime = address.CreationTime,
                CreatorUserId = address.CreatorUserId,
                Id = address.Id,
                LastModificationTime = address.LastModificationTime,
                LastModifierUserId = address.LastModifierUserId,
                NumericalOrder = address.NumericalOrder,
                UserId = address.UserId,
                CustomerName = address.CustomerName ?? string.Empty,
                PhoneNumber = address.PhoneNumber ?? string.Empty
            };
        }
    }
}
