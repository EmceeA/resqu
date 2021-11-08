using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Resq.Web.Interface;
using Resq.Web.ViewModels;
using Resqu.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Resq.Web.Services
{
    public class ProductVendorService : IProductVendor
    {
        private readonly ResquContext _context;
        private readonly IWebHostEnvironment _hosting;
        private readonly IHttpContextAccessor _http;
        public ProductVendorService(ResquContext context, IWebHostEnvironment hosting, IHttpContextAccessor http)
        {
            _context = context;
            _hosting = hosting;
            _http = http;
        }

        public async Task<bool> CreateProductVendor(CreateProductVendorDto createProduct)
        {
            try
            {
                var vendorProd = new ProductVendor
                {
                    VendorAddress = createProduct.VendorAddress,
                    CreatedBy = _http.HttpContext.Session.GetString("userName"),
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    IsBan = false,
                    IsDeleted = false,
                    IsFullyVerified = false,
                    IsModified = false,
                    isVerified = true,
                    VendorName = createProduct.VendorName,
                    VendorPhone = createProduct.VendorPhone
                };
                _context.ProductVendors.Add(vendorProd);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
            
        }

        public async Task<List<ProductVendor>> GetProductVendors()
        {
            return await _context.ProductVendors.ToListAsync();
        }

       
    }
}
