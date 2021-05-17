using Microsoft.EntityFrameworkCore;
using Resqu.Core.Constants;
using Resqu.Core.Dto;
using Resqu.Core.Entities;
using Resqu.Core.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;
namespace Resqu.Core.Services
{
    public class CustomerService : ICustomer
    {
        private readonly ResquContext _context;

        private readonly IOtp _otp;
        private readonly ICacheService _cache;
        public CustomerService(ResquContext context, IOtp otp, ICacheService cache)
        {
            _context = context;
            _otp = otp;
            _cache = cache;
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

       
    }
}
