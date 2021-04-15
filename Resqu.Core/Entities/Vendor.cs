namespace Resqu.Core.Entities
{
    public class Vendor:VendorAudit
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
}
