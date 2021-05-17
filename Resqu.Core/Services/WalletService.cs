using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Resqu.Core.Dto;
using Resqu.Core.Entities;
using Resqu.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Services
{
    public class WalletService : IWallet
    {
        private readonly ServiceCaller<WalletRequestDto> _creditWallet;
        private readonly ServiceCaller<CreateWalletRequestDto> _createWallet;
        private readonly IConfiguration _config;
        private readonly ResquContext _context;
        public WalletService(IConfiguration config, ResquContext context)
        {
            _creditWallet = new ServiceCaller<WalletRequestDto>();
            _createWallet = new ServiceCaller<CreateWalletRequestDto>();
            _config = config;
            _context = context;
        }

        public async Task<CreateWalletResponseDto> CreateWallet(CreateWalletRequestDto responseDto)
        {
            var response = _createWallet.PostRequest(responseDto, _config.GetSection("WalletAPI:CreateWallet").Value, _config.GetSection("SECURITY:token").Value);
            var convertToObject = JsonConvert.DeserializeObject<CreateWalletResponseDto>(response.Content);
            var walletObject = new VendorAccount
            {
                Balance = Convert.ToDecimal(convertToObject.Data.AvailableBalance),
                CreatedBy = "",
                Currency = responseDto.currency,
                DateOfBirth = responseDto.dateOfBirth,
                DateCreated = DateTime.Now,
                WalletId = Guid.NewGuid().ToString(),
                MobileNo = convertToObject.Data.PhoneNumber,
                VendorName = convertToObject.Data.AccountName,
            };

            _context.VendorAccounts.Add(walletObject);
            _context.SaveChanges();
            return convertToObject;
        }

        public async Task<WalletResponseDto> CreditWallet(WalletRequestDto requestDto)
        {
            var response = _creditWallet.PostRequest(requestDto, _config.GetSection("WalletAPI:CreditWallet").Value, _config.GetSection("SECURITY:token").Value);
            var convertToObject = JsonConvert.DeserializeObject<WalletResponseDto>(response.Content);
            return convertToObject;
        }
    }
}
