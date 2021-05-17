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
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomer customer, IJwtAuthManager jwtAuthManager, ILogger<CustomerController> logger)
        {
            _customer = customer;
            _jwtAuthManager = jwtAuthManager;
            _logger = logger;
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
            return BadRequest(register);
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
        public async Task<IActionResult> CustomerSignIn(CustomerSignInRequest signUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var register = await _customer.SignInCustomer(signUp);
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
