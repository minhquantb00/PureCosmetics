using Microsoft.AspNetCore.Http;
using PureCosmetics.AuthService.Application.Mappers;
using PureCosmetics.AuthService.Application.Models;
using PureCosmetics.AuthService.Application.Models.Requests.Address;
using PureCosmetics.AuthService.Application.Models.Responses.Address;
using PureCosmetics.AuthService.Application.ServiceContracts;
using PureCosmetics.AuthService.Domain.Entities;
using PureCosmetics.AuthService.Domain.RepositoryContracts;
using PureCosmetics.Commons.HttpContext;
using PureCosmetics.Commons.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.ServiceImplements
{
    public class AddressService : IAddressService
    {
        #region Fields and Constructors
        private readonly IAddressRepository _addressRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddressService(IAddressRepository addressRepository, IHttpContextAccessor httpContextAccessor)
        {
            _addressRepository = addressRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion
        #region Writes
        public async Task<ApiResponse<DataAddressResponse>> CreateAddress(AddressCreateRequest request)
        {
            bool isAuthenticated = HttpContextHelper.IsUserAuthenticated(_httpContextAccessor);
            if (!isAuthenticated)
            {
                return ApiResponse<DataAddressResponse>.Fail("User is not authenticated", System.Net.HttpStatusCode.Unauthorized);
            }

            int currentUserId = HttpContextHelper.CurrentUserId(_httpContextAccessor);
            var addressEntity = new Address(request.AddressUser, currentUserId, request.CustomerName, request.PhoneNumber, currentUserId);
            
            await _addressRepository.CreateAsyn(addressEntity);

            var response = AddressMapping.EntityToDto(addressEntity);

            return ApiResponse<DataAddressResponse>.Created(response, "Address created successfully");
        }

        public async Task<ApiResponse<DataAddressResponse>> UpdateAddress(AddressUpdateRequest request)
        {
            var entity =  await _addressRepository.GetByIdAsync(request.Id);
            if(entity == null)
            {
                return ApiResponse<DataAddressResponse>.Fail("Address not found", System.Net.HttpStatusCode.NotFound);
            }
            int currentUserId = HttpContextHelper.CurrentUserId(_httpContextAccessor);
            entity.Change(request.AddressUser, request.CustomerName, request.PhoneNumber, currentUserId);

            await _addressRepository.UpdateAsync(entity);

            var response = AddressMapping.EntityToDto(entity);
            return ApiResponse<DataAddressResponse>.Success(response, "Address updated successfully");
        }

        public async Task<ApiResponse<DataAddressResponse>> DeleteAddress(AddressDeleteRequest request)
        {
            var entity = await _addressRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                return ApiResponse<DataAddressResponse>.Fail("Address not found", System.Net.HttpStatusCode.NotFound);
            }

            await _addressRepository.DeleteAsync(entity.Id);
            return ApiResponse<DataAddressResponse>.Success(null!, "Address deleted successfully");
        }
        #endregion
        #region Reads
        public async Task<ApiResponse<PagedResult<DataAddressResponse>>> GetAddresses(AddressGetsRequest request)
        {
            var query = await _addressRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(a => a.AddressUser.Contains(request.Keyword) || 
                                         (a.CustomerName != null && a.CustomerName.Contains(request.Keyword)) ||
                                         (a.PhoneNumber != null && a.PhoneNumber.Contains(request.Keyword)));
            }
            var pagination = new Pagination { Page =  request.PageIndex, ItemsPerPage = request.PageSize };
            var pagedResult = await PagedResult<DataAddressResponse>.ToPagedResultAsync(pagination, query.Select(x => AddressMapping.EntityToDto(x)));

            return ApiResponse<PagedResult<DataAddressResponse>>.Success(pagedResult, "Addresses retrieved successfully");
        }

        public async Task<ApiResponse<DataAddressResponse>> GetAddressesById(AddressGetByIdRequest request)
        {
            var entity = await _addressRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                return ApiResponse<DataAddressResponse>.Fail("Address not found", System.Net.HttpStatusCode.NotFound);
            }
            var response = AddressMapping.EntityToDto(entity);
            return ApiResponse<DataAddressResponse>.Success(response, "Address retrieved successfully");
        }

        public async Task<ApiResponse<PagedResult<DataAddressResponse>>> GetAddressesByUserId(AddressGetByUserIdRequest request)
        {
            var query = await _addressRepository.GetAllAsync(x => x.UserId == request.UserId);
            if(query == null)
            {
                return ApiResponse<PagedResult<DataAddressResponse>>.Fail("No addresses found for the specified user", System.Net.HttpStatusCode.NotFound);
            }
            var pagination = new Pagination { Page = request.PageIndex, ItemsPerPage = request.PageSize };
            var pagedResult = await PagedResult<DataAddressResponse>.ToPagedResultAsync(pagination, query.Select(x => AddressMapping.EntityToDto(x)));
            return ApiResponse<PagedResult<DataAddressResponse>>.Success(pagedResult, "Addresses retrieved successfully");
        }
        #endregion

    }
}
