using Microsoft.AspNetCore.Mvc;
using Resqu.Core.Dto;
using Resqu.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resqu.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VendorController : Controller
    {
        private readonly IVendor _vendor;
        public VendorController(IVendor vendor)
        {
            _vendor = vendor;
        }
        [HttpPost]
        public async Task<IActionResult> VendorLogin(VendorLoginRequestDto vendorLogin)
        {
            var result = await _vendor.VendorLogin(vendorLogin);
            if (result.Response == "Success")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
