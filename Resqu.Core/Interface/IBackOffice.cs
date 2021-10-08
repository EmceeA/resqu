using Resqu.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Interface
{
    public interface IBackOffice
    {
        Task<List<CustomerSignInResponse>> UnbannedCustomers();
        Task<List<CustomerSignInResponse>> BannedCustomers();
        Task<ServiceDetail> GetDetailsById(int? id);
        Task<UpdateCustomerResponseDto> BanCustomer(string phone);

        //Task<>
        Task<UpdateCustomerResponseDto> UnBanCustomer(string phone);
        Task<UpdateCustomerResponseDto> BanVendor(int id);
        Task<UpdateCustomerResponseDto> UnBanVendor(int id);
        Task<UpdateCustomerResponseDto> UpdateVendorProfile(int? id, Core.Dto.Vendor vendor);
        Task<UpdateCustomerResponseDto> DeleteCustomer(string phone);
        Task<UpdateCustomerResponseDto> DeleteVendor(int id);
        Task<List<VendorListDto>> VendorList();
        Task<List<RequestListDto>> RequestList();
        Task<List<CustomerListDto>> CustomerList();
        Task<UpdateCustomerResponseDto> AddExpertise(ExpertiseDto expertise);
        Task<UpdateCustomerResponseDto> AddExpertiseCategory(ExpertiseCategoryDto expertise);

        Task<UpdateCustomerResponseDto> AddVendor(AddVendorDto addVendorDto);
        Task<UpdateCustomerResponseDto> AddService(ExpertiseDto expertiseDto);

        Task<UserLoginResponse> Login(UserLoginDto loginDto);

        
    }
}
