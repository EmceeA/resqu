using Microsoft.AspNetCore.Http;
using Resqu.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resq.Web.ViewModels
{
    public class CreateVendorViewModel
    {
        public int? ServiceName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string IdentificationNumber { get; set; }
        public string MeansOfIdentification { get; set; }
        [Required]
        public string ContactAddress { get; set; }
        [Required]
        public string NextOfKinRelationship { get; set; }
        [Required]
        public string NextOfKinName { get; set; }
        [Required]
        public string NextOfKinAddress { get; set; }
        [Required]
        public string NextOfKinPhone { get; set; }

        public int? ExpertiseId { get; set; }
        public IFormFile VendorPicture { get; set; }
        
    }
}
