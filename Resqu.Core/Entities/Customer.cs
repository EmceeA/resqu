using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resqu.Core.Entities
{

    public class VendorProcessService
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public int Id { get; set; }
        public int CustomerRequestServiceId { get; set; }
        public CustomerRequestService CustomerRequestService { get; set; }
        [Required]
        public string Description { get; set; }
        public int VendorProcessServiceTypeId { get; set; }
        public VendorProcessServiceType VendorProcessServiceType { get; set; }

    }

    public class VendorProcessServiceType
    {
        public int Id { get; set; }
        [Required]

        public string ServiceTypeName { get; set; }
        [Required]
        public decimal Cost { get; set; }
    }

    public class CustomerRequestService
    {
        public int Id { get; set; }
        [Required]

        public string ServiceName { get; set; }
        
    }
    public class Customer : CustomerAudit
    {
        

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string ProfilePicture { get; set; }
        public int AccountId { get; set; }
        public string RegulatoryIndentity { get; set; }
        public string Pin { get; set; }
        public bool IsCustomerCreated  { get; set; }
        public bool IsDedicatedCreated  { get; set; }

        public DateTime LastLoginDate { get; set; }
        public DateTime LastServiceDate { get; set; }


    }
}
