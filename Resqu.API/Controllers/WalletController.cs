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
    public class WalletController : Controller
    {
        private readonly IWallet _wallet;
        public WalletController(IWallet wallet)
        {
            _wallet = wallet;
        }

        [HttpPost]
        public async Task<IActionResult> FundWallet(WalletRequestDto fundRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var send = await _wallet.CreditWallet(fundRequest);
            return Ok(send);
        }


        [HttpPost]
        public async Task<IActionResult> CreateWallet(CreateWalletRequestDto createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var send = await _wallet.CreateWallet(createRequest);
            return Ok(send);
        }




    }
}
