using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Dto
{
    public class CustomerSignUpResponseDto
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        public string RegulatoryIdentity { get; set; }

        public string Status { get; set; }
    }


    public class VendorRegistrationDto 
    {
        public string PhoneNumber { get; set; }
        public string VendorName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactAddress { get; set; }
        public string NextOfKin { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinAddress { get; set; }
        public string NextOfKinPhone { get; set; }
        public string VendorCode { get; set; }
        public string Pin { get; set; }
    }
    public class CustomerSignInRequest
    {
        public string PhoneNumber { get; set; }
        public string Pin { get; set; }
    }

    public class UpdateCustomerRequestDto
    {
        public string Phone { get; set; }
        [Required(ErrorMessage ="Regulatory Id is Required to Activate Profile")]
        public string RegulatoryId { get; set; }
        [Required(ErrorMessage = "Email is Required to Activate Profile")]
        public string Email { get; set; }
    }

    public class UpdateCustomerResponseDto
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }


    public class CustomerSignInResponse
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        public string RegulatoryIdentity { get; set; }

        public string Status { get; set; }
        public string Response { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }


    public class VendorListDto
    {
        public string PhoneNumber { get; set; }
        public string VendorName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactAddress { get; set; }
        public string NextOfKin { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinAddress { get; set; }
        public string NextOfKinPhone { get; set; }
        public string VendorCode { get; set; }
    }

    public class CustomerListDto
    {
        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string RegulatoryIndentity { get; set; }
    }


    public class ExpertiseDto
    {
        public string Name { get; set; }
    }

    public class ExpertiseCategoryDto
    {
        public string Name { get; set; }
    }

    public class TransactionListDto
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        public string RegulatoryIdentity { get; set; }

        public string Status { get; set; }
        public string Response { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }




}
