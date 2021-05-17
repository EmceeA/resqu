using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Entities
{
    public class Transaction: WalletPaymentAudit
    {
        public decimal Amount { get; set; }
        public decimal PhoneNumber { get; set; }
        public string Status { get; set; }
        public string TransactionRef { get; set; }

        public string ServiceType { get; set; }
        public string TransactionType { get; set; }
        public string SubCategory { get; set; }
        public string ServiceDate { get; set; }
        public string CustomerName { get; set; }
        public string VendorName { get; set; }
        public string PaymentType { get; set; }


    }
}
