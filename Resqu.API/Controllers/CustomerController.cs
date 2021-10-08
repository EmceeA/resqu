using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resqu.Core.Dto;
using Resqu.Core.Entities;
using Resqu.Core.Interface;
using Resqu.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Resqu.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomer _customer;
        private readonly IHttpContextAccessor _accessor;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly ILogger<CustomerController> _logger;
        private readonly ResquContext _context;
        public CustomerController(ICustomer customer, IJwtAuthManager jwtAuthManager, ILogger<CustomerController> logger, IHttpContextAccessor accessor,ResquContext context)
        {
            _customer = customer;
            _jwtAuthManager = jwtAuthManager;
            _logger = logger;
            _accessor = accessor;
            _context = context;
        }

       

        [HttpPost]
        public async Task<IActionResult> CustomerSignUp(CustomerSignUpRequestDto signUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var register = await _customer.CustomerSignUp(signUp);
            if (register.Status == "Success")
            {
                return Ok(register);
            }
            return BadRequest(register.Status);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateWalletBalance(UpdateWalletBalanceDto  balanceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var register = await _customer.UpdateWalletBalance(balanceDto);
            return Ok(register);
        }


        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public ActionResult Logout()
        {
            var phone = User.Identity.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(phone); 
            // can be more specific to ip, user agent, device name, etc.
            _logger.LogInformation($"User [{phone}] logged out the system.");
            return Ok();
        }



        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<ActionResult> ActivateProfile(UpdateCustomerRequestDto requestDto)
        {
            var phone = User.Identity.Name;
            requestDto.Phone = phone;
            var activate = await _customer.ActivateCustomerProfile(requestDto);
            if (activate.Status == true)
            {
                _logger.LogInformation($"User [{phone}] logged out the system.");
                return Ok(activate);
            }
            
            return BadRequest(activate);
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> ValidateOtp(OtpDto requestDto)
        {

            var phone = _accessor.HttpContext.Session.GetString("phone");
            if (phone == null)
            {
                phone = _context.Customers.Where(d => d.PhoneNumber == requestDto.Phone).Select(s => s.PhoneNumber).FirstOrDefault();
            }
            requestDto.Phone = phone;
            var activate = await _customer.ConfirmOtp(requestDto);
            if (activate.Status == true)
            {
                _logger.LogInformation($"User [{requestDto.Phone}] logged into the system.");
                return Ok(activate);
            }

            return BadRequest(activate);
        }


        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> GenerateOtp(string phoneNo)
        {

            //requestDto.Phone = phone;
            var generate = await _customer.GenerateOtp(phoneNo);
            if (generate.Status == true)
            {
                _logger.LogInformation($"User [{phoneNo}] logged into the system.");
                return Ok(generate);
            }

            return BadRequest(generate);
        }

        [HttpPost]

        public async Task<IActionResult> EstimatePrice(EstimatePriceRequestDto service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _customer.EstimatePrice(service);
            return Ok(result);
        }


        [HttpPost]

        public async Task<IActionResult> BookService(ServiceDto service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _customer.BookService(service);
            //await _hub.Clients.All.CustomerRequestDetails("customerrequestdetails", result.VendorPhone);
            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> DeleteCard(long id)
        {
            var result = await _customer.DeleteCard(id);
            //await _hub.Clients.All.CustomerRequestDetails("customerrequestdetails", result.VendorPhone);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> BookNow(string bookingId)
        {
            var result = await _customer.BookNow(bookingId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAddress(double latitude, double longitude)
        {
            var result = await _customer.GetAddress(latitude,longitude);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetServiceByName(GetServiceByNameRequest request)
        {
            var result = await _customer.GetServiceByName(request);
            if (result.Count == 0)
            {
                return Ok("Result not found");
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerRequestDetails(string vendorId)
        {
            var result = await _customer.CustomerRequestDetails(vendorId);
            return Ok(result);
        }



        [HttpGet]
        public async Task<IActionResult> AcceptRequest(string bookingId)
        {
            var result = await _customer.AcceptRequest(bookingId);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GoOffline(string phone)
        {
            var result = await _customer.GoOffline(phone);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> StartService(string bookingId)
        {
            var result = await _customer.StartService(bookingId);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> EndService(string bookingId)
        {
            var result = await _customer.EndService(bookingId);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GoOnline(string phone)
        {
            var result = await _customer.GoOnline(phone);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ServiceCategoryByExpertise(int expertiseId)
        {
            var result = await _customer.ServiceCategoryByExpertise(expertiseId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> RejectRequest(string bookingId)
        {
            var result = await _customer.RejectRequest(bookingId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLatitudeLongitudeByAddress(string address)
        {
            var result = await _customer.GetLatitudeLongitudeByAddress(address);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetNearestVendor(string customerLocation, string vendorLocation,string serviceName, string subCategory)
        {
            var result = await _customer.CalculateShortestDistance(customerLocation,vendorLocation,serviceName,subCategory);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> CalculateDistance(double slat, double slon, double dlat, double dlon)
        {
            var result = await _customer.CalculateDistance(slat, slon,dlat,dlon);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> EndService(string bookingId, string paymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _customer.EndService(bookingId,paymentType);
            return Ok(result);
        }

        [HttpGet]

        public async Task<IActionResult> GetServiceList()
        {
            var serviceList = await _customer.ServiceList();
            return Ok(serviceList);
        }

        [HttpGet]

        public async Task<IActionResult> ServiceCategoryList()
        {
            var serviceCategoryList = await _customer.ServiceCategoryList();
            return Ok(serviceCategoryList);
        }

        [HttpGet]

        public async Task<IActionResult> GetServiceCategoryByService(int serviceId)
        {
            var serviceCategoryList = await _customer.GetServiceCategoryByService(serviceId);
            return Ok(serviceCategoryList);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllServices()
        {
            var serviceCategoryList = await _customer.GetAllServices();
            return Ok(serviceCategoryList);
        }



        [HttpGet]
        public async Task<IActionResult> GenerateFirebaseToken(string customerGuid)
        {
            var token = await _customer.GenerateFirebaseToken(customerGuid);
            return Ok(token);
        }

        [HttpPost]

        public async Task<IActionResult> AddServiceCategoryToService(AddServiceCategoryToService categoryToService)
        {
            var serviceCategoryList = await _customer.AddServiceCategoryToService(categoryToService);
            return Ok(serviceCategoryList);
        }

        [HttpPost]

        public async Task<IActionResult> AddIssue(IssuesDto issue)
        {
            var serviceCategoryList = await _customer.AddIssue(issue);
            return Ok(serviceCategoryList);
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var productList = await _customer.ProductList();
            return Ok(productList);
        }

        [HttpGet]
        public async Task<IActionResult> GetIssueByServiceTypeId(int serviceTypeId)
        {
            var productList = await _customer.GetIssueByServiceTypeId(serviceTypeId);
            return Ok(productList);
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment(string bookingId)
        {
            var payment = await _customer.MakePayment(bookingId);
            return Ok(payment);
        }


        [HttpPost]
        public async Task<IActionResult> RateVendor(RateVendorDto rate)
        {
            var rater = await _customer.RateVendor(rate);
            return Ok(rater);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCustomerAccount(DedicatedAccountRequest request)
        {
            var payment = await _customer.CreateCustomerAccount(request);
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDedicatedNubanAccount(DedicatedNubanAccountRequest request)
        {
            var payment = await _customer.CreateDedicatedNubanAccount(request);
            return Ok(payment);
        }

        [HttpGet]
        public async Task<IActionResult> TransferToWallet(TransferToWalletRequestDto transfer)
        {
            var walletTransfer = await _customer.TransferToWallet(transfer);
            return Ok(walletTransfer);
        }

        [HttpGet]
        public async Task<IActionResult> GetWalletBalance(WalletBalanceRequestDto walletBalance)
        {
            var balance = await _customer.GetWalletBalance(walletBalance);
            return Ok(balance);
        }


        [HttpPost]
        public async Task<IActionResult> GetCustomerWalletBalance(CustomerWalletBalanceRequestDto walletBalance)
        {
            var balance = await _customer.GetCustomerWalletBalance(walletBalance);
            return Ok(balance);
        }

        [HttpPost]
        public async Task<IActionResult> MakeServicePayment(PayVendorDto pay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var balance = await _customer.PayVendor(pay);
            return Ok(balance);
        }






        [HttpPost]
        public async Task<IActionResult> AddCard(AddCardDto addCardDto)
        {
            var card = await _customer.RegisterCard(addCardDto);
            if (card.ResponseStatus == true && card.ResponseMessage == "Card Successfully Added")
            {
                return Ok(card);
            }
            return BadRequest(card);
        }
        [HttpPost]
        public async Task<IActionResult> CustomerSignIn(CustomerSignInRequest signUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var register = await _customer.CustomerSignIn(signUp);
            var claims = new[]
        {
            new Claim(ClaimTypes.Name,signUp.UserName),
        };

            var jwtResult = _jwtAuthManager.GenerateTokens(signUp.UserName, claims, DateTime.Now);
            _logger.LogInformation($"User [{signUp.UserName}] logged in the system.");
            if (register.Response == "Successfully Logged In")
            {
                return Ok(new CustomerSignInResponse
                {
                    PhoneNumber = signUp.UserName,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString,
                    EmailAddress = register.EmailAddress,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Response = register.Response,
                    FirebaseToken = await _customer.GenerateFirebaseToken(register.CustomerGuid),
                    Status = register.Status

                }); ;
            }
            return BadRequest(register);
        }
    }
}
