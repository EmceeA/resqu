using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Entities
{
    public class Card: CardPaymentAudit
    {
        public string CardNo { get; set; }
        public string Cvv { get; set; }
        public string HolderName { get; set; }
        public string ExpiryYear { get; set; }
        public string ExpiryMonth { get; set; }
        public string CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }
    }
}
