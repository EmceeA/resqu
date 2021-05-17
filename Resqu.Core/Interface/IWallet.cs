using Resqu.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Interface
{
    public interface IWallet
    {
        Task<WalletResponseDto> CreditWallet(WalletRequestDto requestDto);
        Task<CreateWalletResponseDto> CreateWallet(CreateWalletRequestDto responseDto);
    }
}
