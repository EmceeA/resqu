﻿using Resqu.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Interface
{
    public interface IProduct
    {
        Task<BuyProductResponseDto> BuyProduct(BuyProductRequestDto requestDto);
    }
}
