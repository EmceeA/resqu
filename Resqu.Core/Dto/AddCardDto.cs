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

    public class CardDetails
    {
        public string CardNo { get; set; }
        public string CardName { get; set; }
        public string Cvv { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        
    }


    public class WalletDetails
    {
        public string WalletId { get; set; }
       
        public string Balance { get; set; }
        
        public string AccountName { get; set; }
        
        public string Bank { get; set; }
        

    }


    public class WalletDto
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
    public class AddCardDto
    {
        public string CardNo { get; set; }
        public int Cvv { get; set; }
        public string HolderName { get; set; }
        public int ExpiryYear { get; set; }
        public int ExpiryMonth { get; set; }
        public string Pin { get; set; }
    }


    public class PayVendorDto
    {
        public double Amount { get; set; }
        public string SourceWallet { get; set; }
        public string DestinationWallet { get; set; }
    }
}
