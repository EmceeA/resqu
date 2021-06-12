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

    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public int SubCategoryId { get; set; }
        public string ServiceName { get; set; }
        public string BookingId { get; set; }
        public string SubCategoryName { get; set; }
        public decimal SubCategoryPrice { get; set; }
        public DateTime StartDate { get; set; }
        public string CustomerPhone { get; set; }
        public string VendorPhone { get; set; }
        public string VendorName { get; set; }
        public string VendorGender { get; set; }
        public string ProductName { get; set; }
        public long? ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public string Description { get; set; }
    }

    public class EndServiceDto
    {
        public string VendorName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Total { get; set; }
        public string PaymentType { get; set; }
        public string BookingId { get; set; }
        
    }

    public class ServiceListDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
    }

    public class ProductListDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
    public class ServiceCategoryListDto
    {
        public int ServiceCategoryId { get; set; }
        public string ServiceCategoryName { get; set; }
        public decimal ServiceCategoryPrice { get; set; }
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

    public class AddVendorDto
    {
        [Required]
        public int? VendorService { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public IFormFile Passport { get; set; }
       
        public string MiddleName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ContactAddress { get; set; }
        public string MeansOfIdentification { get; set; }
        public string IdentificationNo { get; set; }
        [Required]
        public string NextOfKinFullName { get; set; }
        [Required]
        public string NextOfKinPhone { get; set; }
        [Required]  
        public string NextOfKinAddress { get; set; }
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
        public int? ExpertiseId { get; set; }
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
