using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Resq.Web.Interface;
using Resqu.Core.Dto;
using Resqu.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resq.Web.Services
{
    public class CustomerWalletService : ICustomerWallet
    {

        private readonly ResquContext _context;
        private readonly IWebHostEnvironment _hosting;
        private readonly IHttpContextAccessor _http;
        public CustomerWalletService(ResquContext context, IWebHostEnvironment hosting, IHttpContextAccessor http)
        {
            _context = context;
            _hosting = hosting;
            _http = http;
        }
        public async Task<List<WalletDto>> GetAllWallets()
        {
            var wallets = await _context.WalletInfos.Select(w => new WalletDto
            {
                AccountName = w.AccountName,
                Balance = w.Balance,
                Bank = w.Bank,
                CustomerCode = w.CustomerCode,
                WalletId= w.WalletId,
                CustomerId = w.CustomerId,
                CustomerName = w.CustomerName,
                DedicatedNuban = w.DedicatedNuban,
                Email = w.Email,
                PhoneNumber = w.PhoneNumber,
            }).ToListAsync();
            return wallets;
        }

        public async Task<List<ResquService>> ServiceRequests()
        {
            var getServe = await _context.ResquServices.ToListAsync();
            return getServe;
        }
    }
}
