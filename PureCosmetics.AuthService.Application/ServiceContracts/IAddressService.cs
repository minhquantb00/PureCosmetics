using PureCosmetics.AuthService.Application.Models;
using PureCosmetics.AuthService.Application.Models.Requests.Address;
using PureCosmetics.AuthService.Application.Models.Responses.Address;
using PureCosmetics.Commons.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.ServiceContracts
{
    [ServiceContract]
    public interface IAddressService
    {
        [OperationContract]
        Task<ApiResponse<DataAddressResponse>> CreateAddress(AddressCreateRequest request);
        [OperationContract]
        Task<ApiResponse<DataAddressResponse>> UpdateAddress(AddressUpdateRequest request);
        [OperationContract]
        Task<ApiResponse<DataAddressResponse>> DeleteAddress(AddressDeleteRequest request);
        [OperationContract]
        Task<ApiResponse<DataAddressResponse>> GetAddressesById(AddressGetByIdRequest request);
        [OperationContract]
        Task<ApiResponse<PagedResult<DataAddressResponse>>> GetAddresses(AddressGetsRequest request);
        [OperationContract]
        Task<ApiResponse<PagedResult<DataAddressResponse>>> GetAddressesByUserId(AddressGetByUserIdRequest request);
    }
}
