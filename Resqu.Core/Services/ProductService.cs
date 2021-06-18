using Microsoft.Extensions.Configuration;
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
    public class ProductService : IProduct
    {

        private readonly ResquContext _context;
        //private IConfiguration _config;
        private readonly IOtp _otp;
        private readonly ICacheService _cache;
        public ProductService(ResquContext context, IOtp otp, ICacheService cache)
        {
            _context = context;
            _otp = otp;
            _cache = cache;
        }
        public async Task<BuyProductResponseDto> BuyProduct(BuyProductRequestDto requestDto)
        {
            var getCustomerInfo = _context.Customers.Where(s => s.Id == requestDto.UserId).FirstOrDefault();

            var getWalletBalance = _context.Wallets.Where(s => s.UserId == requestDto.UserId.ToString()).FirstOrDefault();
            if (getWalletBalance.Balance < requestDto.Price)
            {
                return new BuyProductResponseDto
                {
                    Message = "Insufficient Wallet Balance",
                    Status = false
                };
            }

            else
            {
                getWalletBalance.Balance = getWalletBalance.Balance - requestDto.Price;
                _context.SaveChanges();
                return new BuyProductResponseDto
                {
                    Message = "Product Successfully Purchased",
                    Status = true
                };
            }
        }
    }
}
