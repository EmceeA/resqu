using Microsoft.AspNetCore.Http;
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
    public class OtpController : Controller
    {
        private readonly IOtp _otp;
        public OtpController(IOtp otp)
        {
            _otp = otp;
        }

        [HttpPost]
        public async Task<IActionResult> SendOtp(SendOtpRequestDto signUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var send = await _otp.SendOtp(signUp);
            if (send.Status == true && send.Message == "Success")
            {
                return Ok(send);
            }
            return BadRequest(send);
        }



        
    }
}
