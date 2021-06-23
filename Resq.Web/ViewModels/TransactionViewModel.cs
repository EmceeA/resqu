using Resqu.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resq.Web.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionCount { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }

        public List<TransactionDetail> TransactionDetails { get; set; } 
    }


    public class VendorsViewModel
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public string ServiceCategory { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CompletedRequest { get; set; }
    }


    public class AvailableServiceViewModel
    {
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public int NumberOfVendors { get; set; }
        public int NumberOfUsers { get; set; }
        public int  SubCategory { get; set; }
    }

    public class TopVendor
    {
        public string Picture { get; set; }
        public string VendorName { get; set; }
        public int NumberOfRequest { get; set; }
        public int RatingTotal { get; set; }
    }


    public class Review
    {
        public string Picture { get; set; }
        public string VendorName { get; set; }
        public string SubCategory { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public int CompletedRequest { get; set; }
        public int DaysAgo { get; set; }


    }

    public class Service : IEquatable<Service>
    { 
        public string ServiceType { get; set; }
        public int SubCategory { get; set; }

        public bool Equals(Service other)
        {
            return this.ServiceType.Equals(other.ServiceType);
        }
        public override int GetHashCode()
        {
            return this.ServiceType.GetHashCode();
        }
    }


    public class RoleUrl
    {
        public string PageName { get; set; }
        public string PageNameClass { get; set; }
        public string PageUrl { get; set; }
        public string PageUrlClass { get; set; }

    }
    public class AvailableServiceDetailViewModel :IEquatable<AvailableServiceDetailViewModel>
    {
        public string ServiceType { get; set; }
        public int SubCategory { get; set; }
        public int NumberOfVendors { get; set; }
        public int NumberOfUsers { get; set; }
        public string Description { get; set; }

        public bool Equals(AvailableServiceDetailViewModel other)
        {
            return this.ServiceType.Equals(other.ServiceType);
        }

        public override int GetHashCode()
        {
            return this.ServiceType.GetHashCode();
        }
    }

    public class TransactionDetail
    {
        public decimal VendorAmount { get; set; }
        public decimal PlatformCharge { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PhoneNumber { get; set; }
        public string Status { get; set; }
        public string TransactionRef { get; set; }

        public string ServiceType { get; set; }
        public string TransactionType { get; set; }
        public string SubCategory { get; set; }
        public string ServiceDate { get; set; }
        public string CustomerName { get; set; }
        public string VendorName { get; set; }

        public int? VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public string PaymentType { get; set; }
    }
}
