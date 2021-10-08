using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resqu.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resqu.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddCard(AddCardDto add)
        {

            return Ok();
        }
    }
}
