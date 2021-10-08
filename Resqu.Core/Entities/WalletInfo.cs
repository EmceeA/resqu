using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Entities
{
    public class WalletAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime DateCreated { get; set; }


    }
    public class WalletInfo :WalletAudit
    {
        public string CustomerName { get; set; }
        public string WalletId { get; set; }
        public double Balance { get; set; }
        public string CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string DedicatedNuban { get; set; }
        public string Bank { get; set; }
        public string AccountName { get; set; }
        public string Email { get; set; }
        public string CustomerCode { get; set; }
    }



    public class CompanyCharge : WalletAudit
    {
        public string CustomerName { get; set; }
        public string WalletId { get; set; }
        public double Amount { get; set; }
        public string CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string DestinationWallet { get; set; }
        public string CompanyWallet { get; set; }
    }



    public class PaymentHistory : WalletAudit
    {
        public string CustomerName { get; set; }
        public string WalletId { get; set; }
        public double Amount { get; set; }
        public string CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string DestinationWallet { get; set; }
        public string Narration { get; set; }
        public string Status { get; set; }
        public string TransactionType { get; set; }
    }


    public class CustomerInflow: WalletAudit
    {

        public string CustomerName { get; set; }
        public string Channel { get; set; }
        public string WalletId { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerId { get; set; }
        public double Amount { get; set; }
        public double CurrentBalance { get; set; }
        public string TransactionType { get; set; }
        public string TransactionTypeDescription { get; set; }
        public string Narration { get; set; }
    }
}
