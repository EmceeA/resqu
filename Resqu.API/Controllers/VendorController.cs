using Microsoft.AspNetCore.Authorization;
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
    public class VendorController : Controller
    {
        private readonly IVendor _vendor;
        private readonly ILogger<VendorController> _logger;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IHttpContextAccessor _http;
        public VendorController(IVendor vendor, ILogger<VendorController> logger, IJwtAuthManager jwtAuthManager, IHttpContextAccessor http)
        {
            _vendor = vendor;
            _logger = logger;
            _jwtAuthManager = jwtAuthManager;
            _http = http;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> VendorGoOnline()
        {
            var mobileNo = _http.HttpContext.Session.GetString("mobileNo");
            var online = await _vendor.GoOnline(mobileNo);
            return Ok(online);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> VendorGoOffline()
        {
            var mobileNo = _http.HttpContext.Session.GetString("mobileNo");
            var online = await _vendor.GoOffline(mobileNo);
            return Ok(online);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EndService(string bookingId)
        {
            var result = await _vendor.EndService(bookingId);
            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> StartService(string bookingId)
        {
            var result = await _vendor.StartService(bookingId);
            return Ok(result);
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RejectRequest(string bookingId)
        {
            var result = await _vendor.RejectRequest(bookingId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AcceptRequest(string bookingId)
        {
            var result = await _vendor.AcceptRequest(bookingId);
            return Ok(result);
        }



        [HttpPost]
        public async Task<IActionResult> VendorLogin(VendorLoginRequestDto vendorLogin)
        {
            var result = await _vendor.VendorLogin(vendorLogin);
            _http.HttpContext.Session.SetString("mobileNo", result.VendorDetails.PhoneNo);
            var claims = new[]
        {
            new Claim(ClaimTypes.Name,vendorLogin.Phone),
        };

            var jwtResult = _jwtAuthManager.GenerateTokens(vendorLogin.Phone, claims, DateTime.Now);
            _logger.LogInformation($"User [{vendorLogin.Phone}] logged in the system.");
            if (result.Response == "Success")
            {
                result.token = jwtResult.AccessToken;
                result.resettoken = jwtResult.RefreshToken;
                result.firebase_token = await _vendor.GenerateFirebaseToken();
                return Ok(result);
            }
            return BadRequest(result);
        }
    }

    
}
