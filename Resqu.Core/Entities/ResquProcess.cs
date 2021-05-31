using System;
using System.ComponentModel.DataAnnotations;

namespace Resqu.Core.Entities
{
    public class ResquProcess : ResquProcessAudit
    {
        public Vendor GetVendor { get; set; }

        public Customer GetCustomer { get; set; }

        public Request GetRequest { get; set; }

        public int? CustomerRating { get; set; }
        public int? VendorRating { get; set; }

        public decimal Amount { get; set; }

        public string ServiceType { get; set; }
        public string ProcessAction { get; set; }
    }
    public class ResquProcessAudit
    {
        [Key]
        public long Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }

    public class Request : RequestAudit
    {

        public ExpertiseCategory GetExpertiseCategory { get; set; }

        public Expertise GetExpertise { get; set; }

        public string VendorLocation { get; set; }
        public string CustomerLocation { get; set; }

        public string VendorCode { get; set; }
        public string CustomerPhone { get; set; }
        public string RequestType { get; set; }

        public Vendor Vendor { get; set; }
        public int?  VendorId { get; set; }
        public string RequestStatus { get; set; }

    }


    public class RequestAudit
    {
        [Key]
        public long Id { get; set; }
        
        public DateTime DateCreated { get; set; }
        public DateTime DateAccepted { get; set; }
        public DateTime DateRejected { get; set; }
    }
}
