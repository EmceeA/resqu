namespace Resqu.Core.Entities
{

    public class VendorServiceSubCategory : VendorAudit
    {
        public long SubCategoryId { get; set; }
        public ExpertiseCategory ExpertiseCategory { get; set; }
        public string SubCategoryName { get; set; }
        public long VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public string VendorName { get; set; }
        public string ServiceName  { get; set; }
        public long ServiceId  { get; set; }
    }
    public class Vendor:VendorAudit
    {
        
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string IdentificationNumber { get; set; }
        public string MeansOfIdentification { get; set; }
        public string ContactAddress { get; set; }
        public string NextOfKinRelationship { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinAddress { get; set; }
        public string NextOfKinEmail { get; set; }
        public string NextOfKinPhone { get; set; }
        public string VendorCode { get; set; }
        public string AvailabilityStatus { get; set; }
        public string Pin { get; set; }
        public string VendorPicture { get; set; }
        public int? ExpertiseId { get; set; }
        public Expertise Expertise { get; set; }
    }
}
