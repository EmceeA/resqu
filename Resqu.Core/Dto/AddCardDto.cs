using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Dto
{

    public class AddCardResponse
    {
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public bool ResponseStatus { get; set; }
    }
    public class AddCardDto
    {
        public string CardNo { get; set; }
        public string Cvv { get; set; }
        public string HolderName { get; set; }
        public string ExpiryYear { get; set; }
        public string ExpiryMonth { get; set; }
        public string Pin { get; set; }
    }


    public class PayVendorDto
    {
        public double Amount { get; set; }
        public string SourceWallet { get; set; }
        public string DestinationWallet { get; set; }
    }
}
