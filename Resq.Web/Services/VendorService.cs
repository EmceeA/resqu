using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class VendorService : IVendor
    {
        private readonly ResquContext _context;
        private readonly IWebHostEnvironment _hosting;
        private readonly IHttpContextAccessor _http;
        public VendorService(ResquContext context, IWebHostEnvironment hosting, IHttpContextAccessor http)
        {
            _context = context;
            _hosting = hosting;
            _http = http;
        }
        public string CreateVendor(CreateVendorViewModel createVendor)
        {
            var uploadedImage = UploadImage(createVendor);
            Vendor employee = new Vendor
            {
                CustomerRequestServiceId = createVendor.ServiceName,
                FirstName = createVendor.FirstName,
                LastName = createVendor.LastName,
                EmailAddress = createVendor.EmailAddress,
                ContactAddress = createVendor.EmailAddress,
                NextOfKinAddress = createVendor.NextOfKinAddress,
                CompanyName = createVendor.CompanyName,
                IdentificationNumber = createVendor.IdentificationNumber,
                VendorPicture = uploadedImage,
                NextOfKinPhone = createVendor.NextOfKinPhone,
                PhoneNumber = createVendor.PhoneNo,
                MeansOfIdentification = createVendor.MeansOfIdentification,
                NextOfKinName = createVendor.NextOfKinName,
                PhoneNo = createVendor.PhoneNo,
                MiddleName = createVendor.MiddleName,
                NextOfKinRelationship = createVendor.NextOfKinRelationship,
                DateCreated= DateTime.Now,
                VendorCode  = "REZQ-"+ GenerateRandom(10),
                IsBan = false,
                IsFullyVerified = false,
                IsDeleted= false,
                isVerified = false,
                CreatedBy =_http.HttpContext.Session.GetString("userName"),
                Pin = GenerateRandom(8),
                Gender = createVendor.Gender,
                NextOfKinEmail = createVendor.NextOfKinEmail,
                
                
            };
            var getExistingVendor = _context.Vendors.Where(c => c.FirstName == employee.FirstName && c.LastName == employee.LastName &&c.CompanyName == employee.CompanyName && c.MiddleName == employee.MiddleName && c.CustomerRequestServiceId == employee.CustomerRequestServiceId).Any();
            if (!getExistingVendor)
            {
                _context.Vendors.Add(employee);
                _context.SaveChanges();
                return "Successfully Saved";
            }
            return "This Vendor Already Exist in this Service Category";
        }

        public string GenerateCode()
        {
            throw new NotImplementedException();
        }

        public string GenerateRandom(int length)
        {
            string chars = "1234567890";
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

            byte[] data = new byte[length];
            byte[] buffer = null;
            int maxRandom = byte.MaxValue - ((byte.MaxValue + 1) % chars.Length);
            crypto.GetBytes(data);
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                byte value = data[i];
                while (value > maxRandom)
                {
                    if (buffer == null)
                    {
                        buffer = new byte[1];
                    }
                    crypto.GetBytes(buffer);
                    value = buffer[0];
                }
                result[i] = chars[value % chars.Length];
            }
            var res = new string(result);
            return res;
        }


        public string UploadImage(CreateVendorViewModel createVendor)
        {

            string uniqueFileName = null;

            if (createVendor.VendorPicture != null)
            {
                string uploadsFolder = Path.Combine(_hosting.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + createVendor.VendorPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    createVendor.VendorPicture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
            
        }
    }
}
