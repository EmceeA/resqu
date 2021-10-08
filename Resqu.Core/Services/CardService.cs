//using Microsoft.AspNetCore.Http;
//using Resqu.Core.Dto;
//using Resqu.Core.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Resqu.Core.Services
//{
//    public interface ICardService
//    {
//        Task<AddCardResponse> RegisterCard(AddCardDto addCard);
//    }
//    public class CardService : ICardService
//    {
//        private readonly ResquContext _context;
//        private readonly IHttpContextAccessor _http;
//        public CardService(ResquContext context, IHttpContextAccessor http)
//        {
//            _context = context;
//            _http = http;
//        }

//    }
//}
