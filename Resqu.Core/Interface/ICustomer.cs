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
        
    }

    public interface IVendor
    {
        Task<CustomerSignUpResponseDto> RegisterVendor(CustomerSignUpRequestDto signUpModel);

        //Task<CustomerSignUpResponseDto> RegisterVendor(CustomerSignUpRequestDto signUpModel);


    }

}
