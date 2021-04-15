using System;

namespace Resqu.Core.Entities
{
    public class WalletPayment: WalletPaymentAudit
    {
        public string CustomerWalletId { get; set; }
        public Customer Customer { get; set; }
        public Vendor Vendor { get; set; }
        public string VendorWalletId { get; set; }
        public string VendorPhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionReference { get; set; }
        public string PaymentStatus { get; set; }
        public string ServiceType { get; set; }
    }

    public class WalletPaymentAudit
    {
        public long Id { get; set; }
    }

    public class CardPayment : CardPaymentAudit
    {
        public string CustomerCardNo { get; set; }
        public string Cvv { get; set; }
        public string Pin { get; set; }
        public string VendorWalletId { get; set; }

        public string VendorPhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionReference { get; set; }
        public string PaymentStatus { get; set; }
        public string ServiceType { get; set; }
    }

    public class CardPaymentAudit
    {
        public long Id { get; set; }
    }


    public class CashPayment : CashPaymentAudit
    {

        public string CustomerPhoneNo { get; set; }
        public Customer Customer { get; set; }
        public Vendor Vendor{ get; set; }
        public string VendorCode { get; set; }

        public string VendorPhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionReference { get; set; }
        public string PaymentStatus { get; set; }
        public string ServiceType { get; set; }
    }

    public class CashPaymentAudit
    {
        public long Id { get; set; }
    }
}
