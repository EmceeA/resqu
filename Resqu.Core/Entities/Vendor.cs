namespace Resqu.Core.Entities
{
    public class Vendor:VendorAudit
    {
        
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string IdentificationNumber { get; set; }
        public string MeansOfIdentification { get; set; }
        public string ContactAddress { get; set; }
        public string NextOfKinRelationship { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinAddress { get; set; }
        public string NextOfKinPhone { get; set; }
        public string VendorCode { get; set; }
        public string Pin { get; set; }
        public string VendorPicture { get; set; }
        public int? ExpertiseId { get; set; }
        public Expertise Expertise { get; set; }
    }
}
