using Resqu.Core.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resqu.Core.Interface
{
    public interface ICustomer
    {
        Task<List<GetAllServiceDto>> GetAllServices();
        Task<List<GetAllServiceDto>> GetServiceByName(GetServiceByNameRequest request);

        Task<List<GetIssueDto>> GetIssueByServiceTypeId(int serviceTypeId);
        Task<Response> AddIssue(IssuesDto issue);
        Task<GetServiceCategoryByServiceDto> GetServiceCategoryByService(int serviceId);
        Task<Response> AddServiceCategoryToService(AddServiceCategoryToService categoryToService);
        Task<CustomerSignUpResponseDto> CustomerSignUp(CustomerSignUpRequestDto signUpModel);
        Task<CustomerSignInResponse> CustomerSignIn(CustomerSignInRequest signInModel);
        Task<CustomerSignInResponse> SignInCustomer(CustomerSignInRequest signInModel);
       
        Task<UpdateCustomerResponseDto> ActivateCustomerProfile(UpdateCustomerRequestDto requestDto);
        bool DebitCredit(decimal backOfficeCost, decimal vendorCost, string backOfficeWallet, string destinationWallet, string sourceWallet, decimal totalAmount);
        Task<ServiceResponseDto> BookService(ServiceDto service);

        Task<CustomerRequestResponseDto> CustomerRequestDetails(string vendorId);
        Task<float> GetTravelTime(float distance);
        Task<UpdateCustomerResponseDto> AcceptRequest(string bookingId);
        Task<UpdateCustomerResponseDto> RejectRequest(string bookingId);
        Task<UpdateCustomerResponseDto> GoOnline(string mobileNo);
        Task<UpdateCustomerResponseDto> GoOffline(string mobileNo);
        Task<RootObject> GetAddress(double lat, double lon);
        Task<Location> GetLatitudeLongitudeByAddress(string address);
        Task<double> CalculateDistance(double slat, double slon, double dlat, double dlon);
        Task<VendorDistanceResponseDto> CalculateShortestDistance(string customerLocation, string vendorLocation, string subCategory, string serviceName);
        Task<ConnectResponseDto> Connect(ConnectRequestDto connect);

        Task<OtpConfirmationResponseDto> ConfirmOtp(OtpDto otp);
        Task<OtpGenerateResponseDto> GenerateOtp(string phoneNo);

        Task<RateVendorResponseDto> RateVendor(RateVendorDto rateVendor);
        Task<DedicatedAccountResponse> CreateCustomerAccount(DedicatedAccountRequest request);
        Task<DedicatedNubanAccountResponse> CreateDedicatedNubanAccount(DedicatedNubanAccountRequest request);
        Task<TransferToWalletResponseDto> TransferToWallet(TransferToWalletRequestDto transfer);
        Task<WalletBalanceResponseDto> GetWalletBalance(WalletBalanceRequestDto walletBalance);
        Task<PayoutResponseDto> PayOut(PayoutRequestDto payout);
        Task<EndServiceDto> EndService(string bookingId,string paymentType);
        Task<MakePaymentResponse> MakeCashPayment(string bookingId, string paymentType);
        Task<MakePaymentResponse> MakeCardPayment(string bookingId, string paymentType, MakeCardRequest cardRequest);
        Task<MakePaymentResponse> MakePayment(string bookingId);
        Task<OtpConfirmationResponseDto> StartService(string bookingId);
        Task<OtpConfirmationResponseDto> EndService(string bookingId);
        Task<List<ServiceListDto>> ServiceList();
        Task<List<ProductListDto>> ProductList();
        Task<List<ServiceCategoryListDto>> ServiceCategoryList();
        Task<List<ServiceCategoryDto>> ServiceCategoryByExpertise(int expertiseId);
        
    }

    public interface IVendor
    {
        Task<CustomerSignUpResponseDto> RegisterVendor(CustomerSignUpRequestDto signUpModel);

        //Task<CustomerSignUpResponseDto> RegisterVendor(CustomerSignUpRequestDto signUpModel);


    }

}
