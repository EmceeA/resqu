using Microsoft.AspNetCore.Http;
using Resqu.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UpdateCustomerRequestDto
    {
        public string Phone { get; set; }
        [Required(ErrorMessage = "Regulatory Id is Required to Activate Profile")]
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
        public IFormFile VendorPicture { get; set; }
        public string VendorCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNo { get; set; }
        public string IdentificationNumber { get; set; }
        public string MeansOfIdentification { get; set; }
        public int ExpertiseId { get; set; }
        public string Pin { get; set; }
    }

    public class TransferToWalletRequestDto
    {
        public string SourceWallet { get; set; }
        public string DestinationWallet { get; set; }
        public decimal Amount { get; set; }
    }

    public class WalletBalanceRequestDto
    {
        public string WalletNo { get; set; }
        public string PhoneNo { get; set; }
        public string Pin { get; set; }

    }

    public class PayoutRequestDto
    {
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public decimal Amount { get; set; }
        public string AccountNo { get; set; }
        public decimal Pin { get; set; }
        public string Phone { get; set; }

    }
    public class PayoutResponseDto
    {
        public string Response { get; set; }
        public bool Status { get; set; }
    }

    public class WalletBalanceResponseDto
    {
        public string WalletNo { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; }
        public string Response { get; set; }
        public bool Status { get; set; }


    }
    public class TransferToWalletResponseDto
    {
        public string Response { get; set; }
        public bool Status { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class DedicatedAccountData
    {
        public List<object> transactions { get; set; }
        public List<object> subscriptions { get; set; }
        public List<object> authorizations { get; set; }
        public string email { get; set; }
        public int integration { get; set; }
        public string domain { get; set; }
        public string customer_code { get; set; }
        public string risk_action { get; set; }
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool identified { get; set; }
        public object identifications { get; set; }
    }

    public class DedicatedAccountResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public DedicatedAccountData data { get; set; }
    }


    public class DedicatedAccountRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Bank
    {
        public string name { get; set; }
        public int id { get; set; }
        public string slug { get; set; }
    }

    public class Assignment
    {
        public int integration { get; set; }
        public int assignee_id { get; set; }
        public string assignee_type { get; set; }
        public bool expired { get; set; }
        public string account_type { get; set; }
        public DateTime assigned_at { get; set; }
    }

    public class Customer
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string customer_code { get; set; }
        public string phone { get; set; }
        public string risk_action { get; set; }
    }

    public class DedicatedNubanAccount
    {
        public Bank bank { get; set; }
        public string account_name { get; set; }
        public string account_number { get; set; }
        public bool assigned { get; set; }
        public string currency { get; set; }
        public object metadata { get; set; }
        public bool active { get; set; }
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public Assignment assignment { get; set; }
        public Customer customer { get; set; }
    }

    public class DedicatedNubanAccountResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public DedicatedNubanAccount data { get; set; }
    }
    public class BuyProductRequestDto
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int VendorId { get; set; }
        public int UserId { get; set; }
    }

    public class BuyProductResponseDto
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }

    public class RateVendorResponseDto
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }


    public class RateVendorDto
    {
        
        [Range(1,5,ErrorMessage ="The Star Rating Cannot be less than 1 and greater than 5")]
        public int StarRating { get; set; }
    }
    public class DedicatedNubanAccountRequest
    {
        public string bankName { get; set; }
        public int customer { get; set; }
    }

    public class OtpConfirmationResponseDto
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }

    public class OtpGenerateResponseDto
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
    public class ServiceDto
    {
        public string CustomerAddress { get; set; }
    }


    public class ConnectRequestDto
    {
        public string ServiceName { get; set; }
        public string SubCategoryName { get; set; }
        public decimal SubCategoryPrice { get; set; }
        public string Description { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class PlusCode
    {
        public string compound_code { get; set; }
        public string global_code { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public PlusCode plus_code { get; set; }
        public List<string> types { get; set; }
    }

    public class Root
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }




    public class VendorDistanceResponseDto
    {
        public long VendorId { get; set; }
        public string VendorName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public double Distance { get; set; }
        public double Time { get; set; }
        public string ServiceName { get; set; }
        public string ServiceSubCategory { get; set; }
        public decimal Price { get; set; }
    }


    public class VendorDistanceRequestDto
    {
        public long VendorId { get; set; }
        public string VendorName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
    public class ConnectResponseDto
    {
        public string ServiceName { get; set; }
        public string SubCategoryName { get; set; }
        public decimal SubCategoryPrice { get; set; }
        public string Description { get; set; }
    }

    public class EstimatePriceRequestDto {
        public int SubCategoryId { get; set; }
        public int ServiceId { get; set; }
        public int IssueId { get; set; }
        public string Description { get; set; }
        public string CustomerAddress { get; set; }

    }
    public class EstimatePriceResponseDto
    {
        public string BookingId { get; set; }
        public string EstimatedPrice { get; set; }
    }

    public class ServiceResponseDto
    {
        public string ServiceName { get; set; }
        public string SubCategoryName { get; set; }
        //public decimal SubCategoryPrice { get; set; }
        public double Distance { get; set; }
        public double Time { get; set; }
        public string Description { get; set; }
        public string BookingId { get; set; }
        public string VendorName { get; set; }
        public string VendorGender { get; set; }
        public string VendorPhone { get; set; }
        public string ServiceAmount { get; set; }
    }


    public class CustomerRequestResponseDto
    {
        public string ServiceName { get; set; }
        public string SubCategoryName { get; set; }
        //public decimal SubCategoryPrice { get; set; }
        public double Distance { get; set; }
        public double Time { get; set; }
        public string Description { get; set; }
        public string BookingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string VendorName { get; set; }
        public string VendorGender { get; set; }
        public string VendorPhone { get; set; }
        public string ServiceAmount { get; set; }
    }
    public class MakeCardRequest
    {
        public string CardNo { get; set; }
        public string Pin { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public decimal Cvv { get; set; }
        public string BookingId { get; set; }

        public string WalletId { get; set; }
    }
    public class MakeWalletRequest
    {
        public string SourceWallet { get; set; }
        public string DestinationWallet { get; set; }
        public decimal Amount { get; set; }
        public string BackOfficeWallet { get; set; }
        public decimal VendorCost { get; set; }
        public decimal BackOfficeCost { get; set; }
    }


    public class MakePaymentResponse
    {
        public string BookingId { get; set; }
        public string Reference { get; set; }
        public string Response { get; set; }
        public bool Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; }
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

    public class GetServiceCategoryByServiceDto
    {
        public string ServiceName { get; set; }
        public List<ServiceCategorysDto> ServiceCategorysDtos { get; set; }
    }


    public class GetIssueDto
    {
        public string IssueDescription { get; set; }
        public decimal Price { get; set; }
    }


    public class GetAllServiceDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
    }

    public class GetServiceByNameRequest
    {
        public string ServiceName { get; set; }
    }



    public class ServiceCategorysDto
    {

        public int Id { get; set; }
        public string ServiceCategoryName { get; set; }
        public decimal Price { get; set; }
    }



    public class AddServiceCategoryToService
    {
        public int? ServiceId { get; set; }

        public int? ServiceTypeId { get; set; }


        public DateTime DateCreated { get; set; }
    }

    public class IssueDto
    {
        public int IssueId{ get; set; }

        public int? ServiceTypeId { get; set; }

    }


    public class IssuesDto
    {
       
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? ServiceTypeId { get; set; }


    }
    public class ServiceCatDto
    {
        
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    public class ServiceCategoryListDto
    {
        public int ServiceCategoryId { get; set; }
        public string ServiceCategoryName { get; set; }
        public decimal ServiceCategoryPrice { get; set; }
    }

    public class ServiceCategoryDto
    {
        public int ServiceCategoryId { get; set; }
        public List<ServiceCatDto> ServiceCategoryName { get; set; }
    }
    public class UpdateCustomerResponseDto
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }

    public class AcceptRequestDto
    {
        public string BookingId { get; set; }
        public string CustomerName { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string Location { get; set; }
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
        [Required(ErrorMessage = "Username is required")]
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



    public class SubCategoryList
    {
        public string SubCategory { get; set; }

        public string Price { get; set; }

    }

    public class CustomerProcId
    {
        public string SubCategory { get; set; }

        public decimal Cost { get; set; }

    }

    public class ServiceDetail
    {
        public int Id { get; set; }
        public string ServiceCategory { get; set; }
        public string DateCreated { get; set; }

        public string CreatedBy { get; set; }
        public int SubCategories { get; set; }

        public List<SubCategoryList> SubCategoryList { get; set; }
        public string Description { get; set; }
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

    public class ProductDto
    {
        [Required]
        public decimal ProductPrice { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductCategory { get; set; }
        public IFormFile ProductImage { get; set; }
        [Required]
        public string VendorName { get; set; }
        public int Quantity { get; set; }
    }


    public class ProductListDtos
    {
        public long Id { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductImage { get; set; }
        public string VendorName { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class VendorListDto
    {
        public string Gender { get; set; }
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
    public class HistoryDto
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ServiceName { get; set; }
        public string SubCategory { get; set; }
        public decimal Amount { get; set; }
        public string CustomerName { get; set; }
        public string Location { get; set; }
    }
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
