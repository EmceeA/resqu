using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Dto
{
    public class VendorLoginRequestDto
    {
        public string Phone { get; set; }
        public string Pin { get; set; }
    }
    public class VendorDetails
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string IdentificationNumber { get; set; }
        public string MeansOfIdentification { get; set; }
        public string ContactAddress { get; set; }
        public string NextOfKinRelationship { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinAddress { get; set; }
        public string NextOfKinEmail { get; set; }
        public string NextOfKinPhone { get; set; }
        public string VendorCode { get; set; }
        public string AvailabilityStatus { get; set; }
        public string Pin { get; set; }
        public string VendorPicture { get; set; }
        public int? CustomerRequestServiceId { get; set; }
    }
    public class VendorLoginResponseDto
    {
        public string Response { get; set; }
        public VendorDetails VendorDetails { get; set; }

    }
}
