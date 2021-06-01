using Microsoft.AspNetCore.Http;
using Resqu.Core.Entities;
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

    public class Vendor
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

    public class UpdateCustomerResponseDto
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }

    public class UserRegistrationDto
    {
       
        public string UserName { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }

    public class UserLoginDto
    {
        [Required(ErrorMessage ="Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    public class UserLoginResponse
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string Response { get; set; }
        public List<RoleUrl> RoleUrls { get; set; }
        public string Phone { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
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
    
    public class RequestListDto
    {

        public string VendorLocation { get; set; }
        public string CustomerLocation { get; set; }

        public string VendorCode { get; set; }
        public string CustomerPhone { get; set; }
        public string RequestType { get; set; }


    }



    //public string Name { get; set; }

    //public int? ExpertiseCategoryId { get; set; }
    //public ExpertiseCategory GetExpertiseCategory { get; set; }

    public class ExpertiseDto
    {
        public string ExpertiseName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int? ExpertiseCategoryId { get; set; }
    }
    public class CustomerListDto
    {
        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string RegulatoryIndentity { get; set; }
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
