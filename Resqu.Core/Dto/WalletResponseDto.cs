using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Dto
{

    public class WalletResponseDto
    {
        public Response Response { get; set; }
        public Data Data { get; set; }
    }

    public class Response
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }
    }

    public class Data
    {
        public float AmountCredited { get; set; }
        public float CustomerWalletBalance { get; set; }
        public float BusinessWalletBalance { get; set; }
    }

    public class CreateWalletResponseDto
    {
        public CreateWalletResponse Response { get; set; }
        public CreateWalletResponseData Data { get; set; }
    }

    public class CreateWalletResponse
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }
    }

    public class CreateWalletResponseData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public object BVN { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public string DateSignedup { get; set; }
        public string AccountNo { get; set; }
        public string Bank { get; set; }
        public string AccountName { get; set; }
        public float AvailableBalance { get; set; }
    }


    public class CreateWalletRequestDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string secretKey { get; set; }
        public string dateOfBirth { get; set; }
        public string currency { get; set; }
    }

    public class WalletRequestDto
    {
        public string transactionReference { get; set; }
        public float amount { get; set; }
        public string phoneNumber { get; set; }
        public string secretKey { get; set; }
    }

}
