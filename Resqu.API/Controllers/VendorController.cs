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
        public VendorController(IVendor vendor, ILogger<VendorController> logger, IJwtAuthManager jwtAuthManager)
        {
            _vendor = vendor;
            _logger = logger;
            _jwtAuthManager = jwtAuthManager;
        }
        [HttpPost]
        public async Task<IActionResult> VendorLogin(VendorLoginRequestDto vendorLogin)
        {
            var result = await _vendor.VendorLogin(vendorLogin);
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
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
