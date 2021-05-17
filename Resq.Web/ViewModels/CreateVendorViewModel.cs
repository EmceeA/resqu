using Microsoft.AspNetCore.Http;
using Resqu.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resq.Web.ViewModels
{
    public class CreateVendorViewModel
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string IdentificationNumber { get; set; }
        public string MeansOfIdentification { get; set; }
        public string ContactAddress { get; set; }
        public string NextOfKinRelationship { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinAddress { get; set; }
        public string NextOfKinPhone { get; set; }

        public int? ExpertiseId { get; set; }
        public IFormFile VendorPicture { get; set; }
        
    }
}
