﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resqu.Core.Dto;
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
        public CustomerController(ICustomer customer, IJwtAuthManager jwtAuthManager, ILogger<CustomerController> logger, IHttpContextAccessor accessor)
        {
            _customer = customer;
            _jwtAuthManager = jwtAuthManager;
            _logger = logger;
            _accessor = accessor;
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
        [Authorize]
        public ActionResult Logout()
        {
            var phone = User.Identity.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(phone); 
            // can be more specific to ip, user agent, device name, etc.
            _logger.LogInformation($"User [{phone}] logged out the system.");
            return Ok();
        }



        [HttpPost]
        [Authorize]
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
            
            requestDto.Phone = _accessor.HttpContext.Session.GetString("phone");
            //requestDto.Phone = phone;
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

        public async Task<IActionResult> BookService(ServiceDto service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _customer.BookService(service);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAddress(double lat, double lon)
        {
            var result = await _customer.GetAddress(lat,lon);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> AcceptRequest(string bookingId)
        {
            var result = await _customer.AcceptRequest(bookingId);
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
        public async Task<IActionResult> ProductList()
        {
            var productList = await _customer.ProductList();
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
        public async Task<IActionResult> CustomerSignIn(CustomerSignInRequest signUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var register = await _customer.CustomerSignIn(signUp);
            var claims = new[]
        {
            new Claim(ClaimTypes.Name,signUp.PhoneNumber),
        };

            var jwtResult = _jwtAuthManager.GenerateTokens(signUp.PhoneNumber, claims, DateTime.Now);
            _logger.LogInformation($"User [{signUp.PhoneNumber}] logged in the system.");
            if (register.Response == "Successfully Logged In")
            {
                return Ok(new CustomerSignInResponse
                {
                    PhoneNumber = signUp.PhoneNumber,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString,
                    EmailAddress = register.EmailAddress,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    RegulatoryIdentity = register.RegulatoryIdentity,
                    Response = register.Response,
                    Status = register.Status
                    
                });
            }
            return BadRequest(register);
        }
    }
}
