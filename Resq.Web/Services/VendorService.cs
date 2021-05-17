using Microsoft.AspNetCore.Hosting;
using Resq.Web.Interface;
using Resq.Web.ViewModels;
using Resqu.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Resq.Web.Services
{
    public class VendorService : IVendor
    {
        private readonly ResquContext _context;
        private readonly IWebHostEnvironment _hosting;
        public VendorService(ResquContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }
        public string CreateVendor(CreateVendorViewModel createVendor)
        {
            var uploadedImage = UploadImage(createVendor);
            Vendor employee = new Vendor
            {
                FirstName = createVendor.FirstName,
                LastName = createVendor.LastName,
                EmailAddress = createVendor.EmailAddress,
                ContactAddress = createVendor.EmailAddress,
                NextOfKinAddress = createVendor.NextOfKinAddress,
                CompanyName = createVendor.CompanyName,
                IdentificationNumber = createVendor.IdentificationNumber,
                VendorPicture = uploadedImage,
                NextOfKinPhone = createVendor.NextOfKinPhone,
                PhoneNumber = createVendor.PhoneNumber,
                MeansOfIdentification = createVendor.MeansOfIdentification,
                NextOfKinName = createVendor.NextOfKinName,
                PhoneNo = createVendor.PhoneNo,
                MiddleName = createVendor.MiddleName,
                NextOfKinRelationship = createVendor.NextOfKinRelationship,
                DateCreated= DateTime.Now,
                VendorCode  = GenerateCode(),
                IsBan = false,
                IsFullyVerified = false,
                IsDeleted= false,
                isVerified = false,
                CreatedBy ="user",
                Pin = "1234",
            };
            var getExistingVendor = _context.Vendors.Where(c => c.FirstName == employee.FirstName && c.FirstName == employee.FirstName && c.MiddleName == employee.MiddleName && c.ExpertiseId == employee.ExpertiseId).Any();
            if (!getExistingVendor)
            {
                _context.Vendors.Add(employee);
                _context.SaveChanges();
                return "Successfully Saved";
            }
            return "Vendor Already Exist in this Service Category";
        }

        public string GenerateCode()
        {
            return "1234566";
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
