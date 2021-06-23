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
    public class BackOfficeController : Controller
    {
        private readonly IBackOffice _customer;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly ILogger<BackOfficeController> _logger;
        public BackOfficeController(IBackOffice customer, IJwtAuthManager jwtAuthManager, ILogger<BackOfficeController> logger)
        {
            _customer = customer;
            _jwtAuthManager = jwtAuthManager;
            _logger = logger;
        }

       

        [HttpPost]
        public async Task<IActionResult> BanCustomer(string phone)
        {
            var banCustomer = await _customer.BanCustomer(phone);
            if (banCustomer.Status == true && banCustomer.Message == "Customer Banned Successfully")
            {
                return Ok(banCustomer);
            }
            return BadRequest(banCustomer);
        }


        [HttpGet]
        public async Task<IActionResult> VendorBan(int id)
        {
            var banCustomer = await _customer.BanVendor(id);
            if (banCustomer.Status == true && banCustomer.Message == "Vendor Banned Successfully")
            {
                return Ok(banCustomer);
            }
            return BadRequest(banCustomer);
        }


        [HttpGet]
        public async Task<IActionResult> VendorDelete(int id)
        {
            var banCustomer = await _customer.DeleteVendor(id);
            if (banCustomer.Status == true && banCustomer.Message == "Vendor Deleted Successfully")
            {
                return Ok(banCustomer);
            }
            return BadRequest(banCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile(int id,Vendor vendor)
        {
            var banCustomer = await _customer.UpdateVendorProfile(id,vendor);
            if (banCustomer.Status == true && banCustomer.Message == "Vendor Profile Updated Successfully")
            {
                return Ok(banCustomer);
            }
            return BadRequest(banCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> VendorUnBan(int id)
        {
            var banCustomer = await _customer.UnBanVendor(id);
            if (banCustomer.Status == true && banCustomer.Message == "Vendor UnBanned Successfully")
            {
                return Ok(banCustomer);
            }
            return BadRequest(banCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> BannedCustomers()
        {
            var bannedCustomer = await _customer.BannedCustomers();
            if (bannedCustomer.Count == 0)
            {
                return Ok(new { message = "No List of Banned Customers" });
            }
            return Ok(bannedCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> RequestList()
        {
            var requestList = await _customer.RequestList();
            if (requestList.Count == 0)
            {
                return Ok(new { message = "No List of Request"});
            }
            return Ok(requestList);
        }


        [HttpGet]
        public async Task<IActionResult> VendorList()
        {
            var vendors = await _customer.VendorList();
            if (vendors.Count == 0)
            {
                return Ok(new { message = "No List of Vendors"});
            }
            return Ok(vendors);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerList()
        {
            var customers = await _customer.CustomerList();
            if (customers.Count == 0)
            {
                return Ok(new { message = "No List of Customers" });
            }
            return Ok(customers);
        }


        [HttpGet]
        public async Task<IActionResult> UnBannedCustomers()
        {
            var unbannedCustomer = await _customer.UnbannedCustomers();
            if (unbannedCustomer.Count == 0)
            {
                return Ok(new { message = "No List of UnBanned Customers" });
            }
            return Ok(unbannedCustomer);
        }




        [HttpPost]
        public async Task<IActionResult> UnBanCustomer(string phone)
        {
            var banCustomer = await _customer.UnBanCustomer(phone);
            if (banCustomer.Status == true && banCustomer.Message == "Customer UnBanned Successfully")
            {
                return Ok(banCustomer);
            }
            return BadRequest(banCustomer);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpertiseCategory(ExpertiseCategoryDto expertise)
        {
            var expertiseCategory = await _customer.AddExpertiseCategory(expertise);
            if (expertiseCategory.Status == true && expertiseCategory.Message == "ExpertiseCategory Added Successfully")
            {
                return Ok(expertiseCategory);
            }
            return BadRequest(expertiseCategory);
        }


        [HttpPost]
        public async Task<IActionResult> AddExpertise(ExpertiseDto expertise)
        {
            var expertiser = await _customer.AddExpertise(expertise);
            if (expertiser.Status == true && expertiser.Message == "Added Successfully")
            {
                return Ok(expertiser);
            }
            return BadRequest(expertiser);
        }


    }
}
