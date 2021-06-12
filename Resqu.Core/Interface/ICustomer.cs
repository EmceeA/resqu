using Resqu.Core.Dto;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace Resqu.Core.Interface
{
    public interface ICustomer
    {
        Task<CustomerSignUpResponseDto> CustomerSignUp(CustomerSignUpRequestDto signUpModel);
        Task<CustomerSignInResponse> CustomerSignIn(CustomerSignInRequest signInModel);
        Task<CustomerSignInResponse> SignInCustomer(CustomerSignInRequest signInModel);
       
        Task<UpdateCustomerResponseDto> ActivateCustomerProfile(UpdateCustomerRequestDto requestDto);
        bool DebitCredit(decimal backOfficeCost, decimal vendorCost, string backOfficeWallet, string destinationWallet, string sourceWallet, decimal totalAmount);
        Task<ServiceDto> BookService(ServiceDto service);
        Task<EndServiceDto> EndService(string bookingId,string paymentType);
        Task<MakePaymentResponse> MakeCashPayment(string bookingId, string paymentType);
        Task<MakePaymentResponse> MakeCardPayment(string bookingId, string paymentType, MakeCardRequest cardRequest);
        Task<MakePaymentResponse> MakePayment(string bookingId);
        Task<List<ServiceListDto>> ServiceList();
        Task<List<ProductListDto>> ProductList();
        Task<List<ServiceCategoryListDto>> ServiceCategoryList();
        
    }

    public interface IVendor
    {
        Task<CustomerSignUpResponseDto> RegisterVendor(CustomerSignUpRequestDto signUpModel);

        //Task<CustomerSignUpResponseDto> RegisterVendor(CustomerSignUpRequestDto signUpModel);


    }

}
