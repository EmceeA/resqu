namespace Resqu.Core.Entities
{
    public class VendorAccount: CustomerAudit
    { 
        public string VendorName { get; set; }
        public decimal Balance { get; set; }

        public string WalletId { get; set; }

        public string MobileNo { get; set; }
        public string DateOfBirth { get; set; }
        public string Currency { get; set; }
    }
}
