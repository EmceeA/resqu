using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Entities
{

    public class Product: CustomerAudit
    {
        public long Id { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductName { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorPhone { get; set; }
    }
    public class ResquService: CustomerAudit
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
        public decimal ProductPrice { get; set; }
        public string VendorGender { get; set; }
        public string BookingId { get; set; }
        public bool IsStarted { get; set; }
        public bool IsEnded { get; set; }
        public long  ServiceId { get; set; }
        public long SubCategoryId { get; set; }
        public string  ServiceName { get; set; }
        public string SubCategoryName { get; set; }
        public DateTime  DateStarted { get; set; }
        public DateTime  DateEnded { get; set; }
        public string CustomerPhone { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorPhone { get; set; }
        public string VendorLocation { get; set; }
        public string CustomerLocation { get; set; }
        public decimal Price { get; set; }
        public bool ProductRequired { get; set; }
        public long? ProductId { get; set; }
        public string Description { get; set; }
        public string TotalPrice { get; set; }
        public string PaymentType { get; set; }
        public bool IsVendorAccepted { get; set; }
        public bool IsVendorArrived { get; set; }
        public bool IsVendorRejected { get; set; }
    }
}
