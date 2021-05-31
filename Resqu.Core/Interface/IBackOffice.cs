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

        Task<UpdateCustomerResponseDto> BanCustomer(string phone);

        Task<UpdateCustomerResponseDto> UnBanCustomer(string phone);
        Task<UpdateCustomerResponseDto> DeleteCustomer(string phone);
        Task<List<VendorListDto>> VendorList();
        Task<List<RequestListDto>> RequestList();
        Task<List<CustomerListDto>> CustomerList();
        Task<UpdateCustomerResponseDto> AddExpertise(ExpertiseDto expertise);
        Task<UpdateCustomerResponseDto> AddExpertiseCategory(ExpertiseCategoryDto expertise);

        Task<UpdateCustomerResponseDto> AddVendor();

        Task<UserLoginResponse> Login(UserLoginDto loginDto);

        
    }
}
