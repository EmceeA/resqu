using Microsoft.Extensions.Logging;
using Resqu.Core.Dto;
using Resqu.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Utility
{

    public class ValidateResponse
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }
    public interface IUserService
    {
        ValidateResponse IsValidUserCredentials(CustomerSignInRequest request);
    }

    public class UsersService :IUserService
    {
        private readonly ILogger<UsersService> _logger;


        private readonly ResquContext _db;
        public UsersService(ILogger<UsersService> logger, ResquContext db)
        {
            _logger = logger;
            _db = db;
        }


        public ValidateResponse IsValidUserCredentials(CustomerSignInRequest customer)
        {
            var user = _db.Customers.Where(c => c.PhoneNumber == customer.PhoneNumber).FirstOrDefault();
            if (user == null)
            {
                return new ValidateResponse
                {
                    Message = "Please Enroll to the Platform",
                    Status = false
                };
            }

            var validateCreds = _db.Customers.Where(d => d.PhoneNumber == customer.PhoneNumber && d.Pin == customer.Password).FirstOrDefault();

            if (validateCreds == null)
            {
                return new ValidateResponse
                {
                    Message = "Phone Number  or Pin is not Correct",
                    Status = false
                };
            }
            return new ValidateResponse
            {
                Message = "Success",
                Status = true
            };
        }
    }
}
