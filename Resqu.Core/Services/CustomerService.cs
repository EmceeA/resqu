using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Resqu.Core.Constants;
using Resqu.Core.Dto;
using Resqu.Core.Entities;
using Resqu.Core.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;
namespace Resqu.Core.Services
{
    public class CustomerService : ICustomer
    {
        private readonly ResquContext _context;
        private IConfiguration _config;
        private readonly IOtp _otp;
        private readonly ICacheService _cache;
        public CustomerService(ResquContext context, IOtp otp, ICacheService cache, IConfiguration config)
        {
            _context = context;
            _otp = otp;
            _cache = cache;
            _config = config;
        }

        public async Task<CustomerSignInResponse> SignInCustomer(CustomerSignInRequest signInModel)
        {
            CustomerSignInResponse response = null;
            string custCacheId = ConstantValue.USER_LOGIN_CACHE + signInModel.PhoneNumber + signInModel.Pin;
            var cachedString = _cache.GetCachedValue(custCacheId, ConstantValue.CACHE_KEY_APP_DEFAULT).Result;
            //string cachedString = null;

        ApiCall:
            if (string.IsNullOrEmpty(cachedString))
            {
                response = CustomerSignIn(signInModel).Result;
                if (response == null)
                {
                    return new CustomerSignInResponse
                    {
                        
                    };
                }
                await _cache.CacheValue(custCacheId, SerializeObject(response), ConstantValue.CACHE_KEY_APP_DEFAULT, 60);
                return response;
            }
            else
            {
                try
                {
                    //response = Validate(model).Result;
                    response = DeserializeObject<CustomerSignInResponse>(cachedString);
                    Log.Information($"CacheResponse:{SerializeObject(response)}");
                    if (response == null)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    cachedString = string.Empty;
                    goto ApiCall;
                }
            }
            Log.Information($"Customer Login: {response}");
            return response;
        }

        public async Task<CustomerSignInResponse> CustomerSignIn(CustomerSignInRequest signInModel)
        {
            var getUserName = _context.Customers.Where(d => d.PhoneNumber == signInModel.PhoneNumber && d.IsDeleted == false).FirstOrDefault();
            if (getUserName == null)
            {
                return new CustomerSignInResponse
                {
                    Response = "Invalid Mobile Number, Kindly Enroll on the Platform"
                };
            }
            string pin = DecodePin(getUserName.Pin);

            var getUser = await _context.Customers.Where(c => c.PhoneNumber == signInModel.PhoneNumber && signInModel.Pin == pin).FirstOrDefaultAsync();
            if (getUser == null)
            {
                return new CustomerSignInResponse
                {
                    Response = "Invalid Credentials"
                };
            }
            if (getUser.IsBan == true)
            {
                return new CustomerSignInResponse
                {
                    Response = "Oops, You have been banned; Kindly Contact the Administrator"
                };
            }
            getUser.LastLoginDate = DateTime.Now;
            _context.SaveChanges();
            return new CustomerSignInResponse
            {
                EmailAddress = getUser.EmailAddress,
                FirstName = getUser.FirstName,
                LastName = getUser.LastName,
                PhoneNumber = getUser.PhoneNumber,
                RegulatoryIdentity = getUser.RegulatoryIndentity,
                Status = getUser.isVerified == true ? "Active": "Not Active",

                Response = "Successfully Logged In"
            };
        }

        public async Task<CustomerSignUpResponseDto> CustomerSignUp(CustomerSignUpRequestDto signUpModel)
        {
            try
            {
                var getCustomer = await _context.Customers.Where(d => d.PhoneNumber == signUpModel.PhoneNumber && d.FirstName == signUpModel.FirstName && d.LastName == signUpModel.LastName).AnyAsync();
                if (getCustomer)
                {
                    return new CustomerSignUpResponseDto
                    {
                        Status = "Customer Already Enrolled"
                    };
                }

                //var getOtp = await _otp.SendOtp(new SendOtpRequestDto { MobileNumber = signUpModel.PhoneNumber });

                var getOtpByNumber = await _context.Otps.Where(d => d.Phone == signUpModel.PhoneNumber).Select(c => c.OtpNumber).FirstOrDefaultAsync();

                if (getOtpByNumber != null && getOtpByNumber == signUpModel.Otp)
                {
                    await _context.Customers.AddAsync(new Customer
                    {
                        FirstName = signUpModel.FirstName,
                        LastName = signUpModel.LastName,
                        PhoneNumber = signUpModel.PhoneNumber,
                        Pin = EncodePin(signUpModel.Pin),
                        DateCreated = DateTime.Now 
                    });
                    await _context.SaveChangesAsync();
                   return new CustomerSignUpResponseDto
                    {
                       FirstName = signUpModel.FirstName,
                       LastName = signUpModel.LastName,
                       PhoneNumber = signUpModel.PhoneNumber,
                       Status = "Success"
                    };
                }
                //09064615283
                return new CustomerSignUpResponseDto
                {
                    Status = "Failed"
                };
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public string EncodePin(string pin)
        {
            byte[] requestPin = Encoding.ASCII.GetBytes(pin);
            var encoded = Convert.ToBase64String(requestPin);
            return encoded;
        }

        public string DecodePin(string requestPin)
        {
            var decode = Convert.FromBase64String(requestPin);
            var decoded = Encoding.ASCII.GetString(decode);
            return decoded;
        }

        public async Task<UpdateCustomerResponseDto> ActivateCustomerProfile(UpdateCustomerRequestDto requestDto)
        {
            try
            {
                var getUserDetails = await _context.Customers.Where(c => c.PhoneNumber == requestDto.Phone && c.IsDeleted == false).FirstOrDefaultAsync();
                if (getUserDetails == null)
                {
                    return new UpdateCustomerResponseDto
                    {
                        Message = "User Not Found",
                        Status = false
                    };
                }

                getUserDetails.DateModified = DateTime.Now;
                getUserDetails.EmailAddress = requestDto.Email;
                getUserDetails.RegulatoryIndentity = requestDto.RegulatoryId;
                getUserDetails.isVerified = true;
                getUserDetails.IsFullyVerified = true;
                getUserDetails.IsDeleted = false;
                getUserDetails.IsModified = true;
                getUserDetails.IsBan = false;
                try
                {
                    await _context.SaveChangesAsync();
                    return new UpdateCustomerResponseDto
                    {
                        Message = "Profile Activated Successfully",
                        Status = true
                    };
                }
                catch (Exception ex)
                {
                    return new UpdateCustomerResponseDto
                    {
                        Message = ex.Message,
                        Status = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = ex.Message,
                    Status = false
                };
            }
            
        }
        public string GenerateRandom(int length)
        {
            string chars = "1234567890";
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

            byte[] data = new byte[length];
            byte[] buffer = null;
            int maxRandom = byte.MaxValue - ((byte.MaxValue + 1) % chars.Length);
            crypto.GetBytes(data);
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                byte value = data[i];
                while (value > maxRandom)
                {
                    if (buffer == null)
                    {
                        buffer = new byte[1];
                    }
                    crypto.GetBytes(buffer);
                    value = buffer[0];
                }
                result[i] = chars[value % chars.Length];
            }
            var res = new string(result);
            return res;
        }
        public async Task<ServiceDto> BookService(ServiceDto service)
        {
            var serviceModel = new ResquService
            {
                ServiceName = service.ServiceName,
                ServiceId = service.ServiceId,
                DateStarted = DateTime.Now,
                CustomerPhone = service.CustomerPhone,
                Description = service.Description,
                SubCategoryName = service.SubCategoryName,
                SubCategoryId = service.SubCategoryId,
                VendorPhone = service.VendorPhone,
                VendorName = service.VendorName,
                VendorGender =service.VendorGender,
                Price = service.SubCategoryPrice,
                BookingId = $"{service.ServiceName}-{GenerateRandom(10)}",
                IsStarted = true
            };

            _context.ResquServices.Add(serviceModel);
            _context.SaveChanges();

            return new ServiceDto
            {
                CustomerPhone = service.CustomerPhone,
                Description = service.Description,
                ServiceId = service.ServiceId,
                ServiceName = _context.Expertises.Where(e => e.Id == service.ServiceId).Select(c => c.Name).FirstOrDefault(),
                StartDate = serviceModel.DateStarted,
                SubCategoryId = service.SubCategoryId,
                SubCategoryName = _context.ExpertiseCategories.Where(e => e.Id == service.SubCategoryId).Select(c => c.Name).FirstOrDefault(),
                BookingId = serviceModel.BookingId,
                VendorGender= service.VendorGender,
                VendorName = service.VendorName,
                VendorPhone = service.VendorPhone,
                SubCategoryPrice = _context.ExpertiseCategories.Where(e=>e.Id == service.SubCategoryId).Select(w=>w.Price).FirstOrDefault(),
            };
        }

        public async Task<List<ServiceListDto>> ServiceList()
        {
            var getAllService = await _context.Expertises.Select(d => new ServiceListDto
            {
                ServiceId = d.Id,
                ServiceName = d.Name
            }).ToListAsync();
            return getAllService;
        }

        public async Task<List<ServiceCategoryListDto>> ServiceCategoryList()
        {
            var getAllServiceCategory = await _context.ExpertiseCategories.Select(d => new ServiceCategoryListDto
            {
                ServiceCategoryId = d.Id,
                ServiceCategoryName = d.Name,
                ServiceCategoryPrice = d.Price
            }).ToListAsync();
            return getAllServiceCategory;
        }

        public async Task<EndServiceDto> EndService(string bookingId,string paymentType)
        {
            var getServiceByBookingNumber = await _context.ResquServices.Where(e => e.BookingId == bookingId).FirstOrDefaultAsync();
            if (getServiceByBookingNumber == null)
            {
                return null;
            }
            getServiceByBookingNumber.IsEnded = true;
            getServiceByBookingNumber.DateEnded = DateTime.Now;
            TimeSpan timeSpan = getServiceByBookingNumber.DateEnded - getServiceByBookingNumber.DateStarted;
            var serviceCharge = Convert.ToInt32(_config.GetSection("AmountPerMinute").Value) * timeSpan.TotalMinutes;
            var materialCost = _context.Products.Where(e => e.Id == getServiceByBookingNumber.ProductId).Select(u => u.ProductPrice).FirstOrDefault();
            getServiceByBookingNumber.TotalPrice = (Convert.ToDecimal(serviceCharge) + materialCost).ToString();
            getServiceByBookingNumber.Price = Convert.ToDecimal(serviceCharge);
            getServiceByBookingNumber.ProductPrice = materialCost;
            getServiceByBookingNumber.PaymentType = paymentType;
            _context.SaveChanges();

            return new EndServiceDto
            {
                BookingId = bookingId,
                StartDate = getServiceByBookingNumber.DateStarted.ToString("dd-MM-yyyy hh:MMMM"),
                EndDate = getServiceByBookingNumber.DateEnded.ToString("dd-MM-yyyy hh:MMMM"),
                ServiceCharge = Convert.ToDecimal(serviceCharge),
                MaterialCost = materialCost,
                PaymentType = paymentType,
                Total = Convert.ToDecimal(serviceCharge) + materialCost,
                VendorName = getServiceByBookingNumber.VendorName
            };
        }

        public Task<List<ProductListDto>> ProductList()
        {
            throw new NotImplementedException();
        }
    }
}
