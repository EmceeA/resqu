using FirebaseAdmin;
using FirebaseAdmin.Auth;
using GeoCoordinatePortable;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Resqu.Core.Constants;
using Resqu.Core.Dto;
using Resqu.Core.Entities;
using Resqu.Core.Interface;
using RestSharp;
using Serilog;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
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
        private readonly IHttpContextAccessor _http;
        private readonly string jsonCredential = "";
        public CustomerService(ResquContext context, IOtp otp, ICacheService cache, IConfiguration config, IHttpContextAccessor http)
        {
            _context = context;
            _otp = otp;
            _cache = cache;
            _config = config;
            _http = http;
            jsonCredential = @"{
                                  'type': 'service_account',
                                  'project_id': 'rezq-project',
                                  'private_key_id': '37b56a01bbe57f084dbd583a1220a721dfe4da10',
                                  'private_key': '-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCxXeb+EnAUSs3o\nVd/AvaqFflQ7h0CxtBoFy/5/SYYbX4YtbqhsLPV9BWiBWu0rUXk0u0itxHG4tqo4\nZtHT2OcrgrsIvLdsvSNds0FOAlU2c/qNRZ86otPcBV6h4knKLzGWT3xiSR2iXkDg\nF25L5hshvmW+rCzsAN/ZGIReNdhlRE4g3dk/yHGKh6P7bX6O6Ci81CAvZfhdrnph\nVYp89eGMVBQoPjEBYsPdTyClQZPc/dPTam0MddLaalePzbPbYqctA5rFjofx9WHx\nQ/inBumn6UAPH+CvDUlTBsIm/dkAEp8SUkQKLwpps/TC85te79KSVIJpKhSGsNWG\n9JvR8CAfAgMBAAECggEAKV/2Wp6Cdrlcfy5JG/UiqjFmBNzwQoP47hFANQE5v3jx\nG/gMYe+69IcmccZ8PPash5iIw8Bnpuc1niKeIAUhKA0/BDLBtyQH8/u06DWzGxjD\nAeGT22HQeirmgG0BOaD4uE+ifGsB9rZ1+9B7ckkjWyPHKEuRzxwFG+eUOUXmTPjW\ntXK5njrce+kyzSTT1K5SJM1W1NKWnn6c6fBk8aKZ1P3/YyJMHfvYAYGtFIyMNans\nbUMMH7ikyRPct+jRpPWhh0ofSIIcQ/cwEdyfFucc9t1nVi3wxFUXbGFqwCBOfYqM\nTQ3IstVmNfo4bA9qknyH+7I8BfX6CaVl7cG6WwCzgQKBgQDW5avrm4tIRxzBn8rl\nStWrC5tckCw0isyD9ywtoMx+3VgTPBBW5uwoNHyPIC78+HmBNCbVO5oF4+QpUVyt\n3tsFKW2tg/w1xHReQK0fRr6MlhMz+xxaA4Q41EI8v75EzBwT1kV3zyOIPugD8V20\nZP4XdJSlHrSEcp98Oc/Ye6a/9wKBgQDTSpOaCKKGQMbv6nGvvwM9NGDkRbc6oRht\nfz0S4SA30p72DYHbtMmQUS8Gf2tCU3fUT26SRu6/Z3FR0IAijrGWi37Gf03rusZ5\nBbXHO3kx1YOOoWN1wLy16aeZmwpJFjTOFoGEA+J/swctqni4ulPiGFqYKd+GtRlC\neZ/fHy1nGQKBgQCoerY36OHhD8eu0g2riOlNybLLdkkCBJPlHoMnXrsU94pWwi95\nQmCCAOxq9euA73mrX2zWnnzaA1pdPmmv81m5iUpw0FqK+PPW9VQFglxgMkTp6wTG\nWZ1vRJKNuDJ/E5srvkEGdoLADbmvZat2g/tl/kOP1Svn70m0hkq6ye7o7wKBgCGd\n3OwF8a3AgqZLO7lFf8NuIPhQqz1VwigxtUrarKCVDxuAvKdzFRqz//JdtXgBBbCu\nzp3RUUz8rUfiD3DwGQsluI7mVERsHAXHRcukB71JBjxcKxHiD2Q1/6dtxm4obVBY\nvlR9tbyxhDPcyoZBhvUTAN7y0pCBIbq01R3PvQ6JAoGANOSy9iyykq8LNt4L+3aG\ndgt38+UKkFYFrrWMylINuHxMaslOYxhCcwYh9XhdcyyvTRE3VhOVdyn/3dP74PNA\naGrtXJhrSLPY8Hf/5CDhAelX73tc4Vz4n+7PymlIhNqrz9NElSpQJBanBGPdDwHW\nPG/rjy4O6jk/gYOla7brgV0=\n-----END PRIVATE KEY-----\n',
                                  'client_email': 'firebase-adminsdk-xo7nw@rezq-project.iam.gserviceaccount.com',
                                  'client_id': '116866592224203665923',
                                  'auth_url': 'https://accounts.google.com/o/oauth2/auth',
                                  'token_uri': 'https://oauth2.googleapis.com/token',
                                  'auth_provider_x509_cert_url': 'https://www.googleapis.com/oauth2/v1/certs',
                                  'client_x509_cert_url': 'https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-xo7nw%40rezq-project.iam.gserviceaccount.com'
                                }";
        }


        public async Task<AddCardResponse> RegisterCard(AddCardDto addCard)
        {
            try
            {
                var card = new Card
                {
                    Active = false,
                    CardNo = addCard.CardNo,
                    CustomerId = _http.HttpContext.Session.GetString("customerPhone"),
                    Cvv = addCard.Cvv,
                    DateCreated = DateTime.Now,
                    ExpiryMonth = addCard.ExpiryMonth,
                    ExpiryYear = addCard.ExpiryYear,
                    HolderName = addCard.HolderName,
                };

                var checkExist = _context.Cards.Where(e => e.CustomerId == card.CustomerId && e.CardNo == card.CardNo).Any();
                if (checkExist)
                {
                    return new AddCardResponse
                    {
                        ResponseCode = "23",
                        ResponseMessage = "Card Already Added",
                        ResponseStatus = false
                    };
                }
                _context.Add(card);

                await _context.SaveChangesAsync();
                return new AddCardResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Card Successfully Added",
                    ResponseStatus = true
                };
            }
            catch (Exception ex)
            {
                return new AddCardResponse
                {
                    ResponseCode = "99",
                    ResponseMessage = "Error Card Not Added",
                    ResponseStatus = true
                };
                throw;
            }
        }

        public async Task<CustomerSignInResponse> SignInCustomer(CustomerSignInRequest signInModel)
        {
            CustomerSignInResponse response = null;
            string custCacheId = ConstantValue.USER_LOGIN_CACHE + signInModel.UserName + signInModel.Password;
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
            var getUserDetail = _context.Customers.Where(s => s.PhoneNumber == signInModel.UserName || s.EmailAddress == signInModel.UserName).FirstOrDefault();

            if (getUserDetail == null)
            {
                return new CustomerSignInResponse
                {
                    Response = "User Not Found",
                    Status = "Valid User is Required"
                };
            }

            if (getUserDetail.IsDeleted)
            {
                return new CustomerSignInResponse
                {
                    Response = "User does not exist",
                    Status = "Valid User is Required"
                };
            }
            var getUserName = _context.Customers.Where(d =>d.PhoneNumber == signInModel.UserName || (d.EmailAddress == signInModel.UserName) && d.IsOtpVerified == true && d.IsDeleted == false).FirstOrDefault();

            if (getUserDetail.IsOtpVerified == false)
            {
                return new CustomerSignInResponse
                {
                    Response = "Kindly Validate Otp and Proceed to Login",
                    Status = "Unable to Login"
                };
            }
            _http.HttpContext.Session.SetString("customerPhone", signInModel.UserName);
            if (getUserName == null)
            {
                return new CustomerSignInResponse
                {
                    Response = "Invalid Mobile Number, Kindly Enroll on the Platform"
                };
            }
            string pin = DecodePin(getUserName.Pin);
            if (signInModel.Password != pin)
            {
                return new CustomerSignInResponse
                {
                    Response = "Invalid Username or Password"
                };
            }
            //var getUser = await _context.Customers.Where(c =>c.PhoneNumber == signInModel.UserName || (c.EmailAddress == signInModel.UserName) && signInModel.Password == pin).FirstOrDefaultAsync();
            //if (getUser == null)
            //{
            //    return new CustomerSignInResponse
            //    {
            //        Response = "Invalid Credentials"
            //    };
            //}

            if (getUserDetail.IsBan == true)
            {
                return new CustomerSignInResponse
                {
                    Response = "Oops, You have been banned; Kindly Contact the Administrator"
                };
            }
            
            getUserDetail.LastLoginDate = DateTime.Now;
            _context.SaveChanges();
            return new CustomerSignInResponse
            {
                CustomerGuid = getUserDetail.CustomerGuid,
                EmailAddress = getUserDetail.EmailAddress,
                FirstName = getUserDetail.FirstName,
                LastName = getUserDetail.LastName,
                PhoneNumber = getUserDetail.PhoneNumber,
                //RegulatoryIdentity = getUserDetail.RegulatoryIndentity,
                Status = getUserDetail.isVerified == true ? "Active": "Not Active",
                Response = "Successfully Logged In"
            };
        }

        public async Task<CustomerSignUpResponseDto> CustomerSignUp(CustomerSignUpRequestDto signUpModel)
        {
            try
            {
                //var length = signUpModel.PhoneNumber.Length - 1;
                signUpModel.PhoneNumber = "0"+signUpModel.PhoneNumber.Substring(4, 10);
                _http.HttpContext.Session.SetString("phone", signUpModel.PhoneNumber);
                var getCustomer = await _context.Customers.Where(d => d.PhoneNumber == signUpModel.PhoneNumber && d.FirstName == signUpModel.FirstName && d.LastName == signUpModel.LastName).AnyAsync();
                if (getCustomer)
                {
                    return new CustomerSignUpResponseDto
                    {
                        Status = "Customer Already Enrolled"
                    };
                }
                var createDedicatedAccount = await CreateCustomerAccount(new DedicatedAccountRequest
                {
                    Email = signUpModel.Email,
                    FirstName = signUpModel.FirstName,
                    LastName = signUpModel.LastName,
                    Phone = signUpModel.PhoneNumber
                });
                if (createDedicatedAccount.message != "Customer created" && createDedicatedAccount.status != true)
                {
                    return new CustomerSignUpResponseDto
                    {
                        Status = "Customer Profile Was not Created on Paystack"
                    };
                }
                if (createDedicatedAccount.message == "Customer created" && createDedicatedAccount.status == true)
                {

                    var getToken = _context.Otps.Where(d => d.Phone == signUpModel.PhoneNumber).FirstOrDefault();
                    var onetime = GenerateRandom(6);
                    if (getToken != null)
                    {
                        getToken.DateCreated = DateTime.Now;
                        getToken.ExpiryDate = getToken.DateCreated.AddMinutes(5);
                        getToken.OtpNumber = onetime;
                        getToken.Status = "UPDATED";
                        _context.SaveChanges();
                    }
                    else if (getToken == null)
                    {
                        var createdDate = DateTime.Now;
                        var otps = new Otp
                        {
                            OtpNumber = onetime,
                            Phone = signUpModel.PhoneNumber,
                            DateCreated = createdDate,
                            ExpiryDate = createdDate.AddMinutes(5),
                            Status = "CREATED"
                        };
                        _context.Otps.Add(otps);
                        _context.SaveChanges();
                    }



                    //Email(signUpModel.Email, $"Your One Time Password is {oneTimePassword.OtpNumber}");
                    //var getOtpByNumber = await _context.Otps.Where(d => d.Phone == signUpModel.PhoneNumber).Select(c => c.OtpNumber).FirstOrDefaultAsync();
                    //if (getOtpByNumber != null && getOtpByNumber == signUpModel.Otp)
                    //{
                    var req = new DedicatedNubanAccountRequest
                    {
                        preferred_bank = _config.GetSection("WalletAPI:DedicatedBank").Value,
                        customer = createDedicatedAccount.data.id
                    };
                    var createDedicatedNuban = await CreateDedicatedNubanAccount(req);


                    if (createDedicatedNuban.status == false)
                    {
                        return new CustomerSignUpResponseDto
                        {
                            Status = "Unable to Create a Dedicated Nuban Account",
                        };
                    }

                    
                    var input = new Resqu.Core.Entities.Customer
                    {
                        FirstName = signUpModel.FirstName,
                        LastName = signUpModel.LastName,
                        PhoneNumber = signUpModel.PhoneNumber,
                        Pin = EncodePin(signUpModel.Password),
                        DateCreated = DateTime.Now,
                        AccountId = createDedicatedAccount.data.id,
                        EmailAddress = signUpModel.Email,
                        IsCustomerCreated = true,
                        IsDedicatedCreated = false,
                        CustomerGuid = Guid.NewGuid().ToString(),
                        WalletId = "REZQ" + Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                        CustomerCode = createDedicatedAccount.data.customer_code
                    };
                    await _context.Customers.AddAsync(input);

                    if (createDedicatedNuban.status == false)
                    {
                        var walletInfom = new WalletInfo
                        {
                            Balance = 0.00,
                            CustomerId = input.AccountId.ToString(),
                            CustomerName = input.FirstName + " " + input.LastName,
                            DateCreated = DateTime.Now,
                            WalletId = input.WalletId,
                            PhoneNumber = input.PhoneNumber,
                            DedicatedNuban = "",
                            AccountName = "",
                            Bank = "",
                            CustomerCode = "",
                            Email = ""

                        };
                        await _context.WalletInfos.AddAsync(walletInfom);
                        await _context.SaveChangesAsync();
                        return new CustomerSignUpResponseDto
                        {
                            FirstName = signUpModel.FirstName,
                            LastName = signUpModel.LastName,
                            PhoneNumber = signUpModel.PhoneNumber,
                            EmailAddress = signUpModel.Email,
                            Status = "Success"
                        };
                    }



                    var walletInfo = new WalletInfo
                    {
                        Balance = 0.00,
                        CustomerId = input.AccountId.ToString(),
                        CustomerName = input.FirstName + " " + input.LastName,
                        DateCreated = DateTime.Now,
                        WalletId = input.WalletId,
                        PhoneNumber = input.PhoneNumber,
                        DedicatedNuban = createDedicatedNuban.data.account_number,
                        AccountName = createDedicatedNuban.data.customer.first_name + " " + createDedicatedNuban.data.customer.last_name,
                        Bank = createDedicatedNuban.data.bank.name,
                        CustomerCode = createDedicatedNuban.data.customer.customer_code,
                        Email = createDedicatedNuban.data.customer.email
                    };

                    var checkWalletInfo = _context.WalletInfos.Where(e => e.Email == input.EmailAddress && e.PhoneNumber == input.PhoneNumber).Any();
                    if (checkWalletInfo)
                    {
                        return new CustomerSignUpResponseDto
                        {
                            Status = "User Wallet Already Exist"
                        };
                    }
                    await _context.WalletInfos.AddAsync(walletInfo);
                    await _context.SaveChangesAsync();

                    return new CustomerSignUpResponseDto
                    {
                        FirstName = signUpModel.FirstName,
                        LastName = signUpModel.LastName,
                        PhoneNumber = signUpModel.PhoneNumber,
                        EmailAddress = signUpModel.Email,
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
                return new CustomerSignUpResponseDto
                {
                    Status = ex.Message
                };
            }
        }

        




        public static void Email(string email, string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("adetop99@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("adetop99@gmail.com", "OLUWAseun");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
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

        public async Task<EstimatePriceResponseDto> EstimatePrice(EstimatePriceRequestDto service)
        {
            var getRequest = _context.CustomerRequestServices.Where(e => e.Id == service.ServiceId).FirstOrDefault();
            if (getRequest == null)
            {
                return null;
            }
            var getEstimatedPrice = _context.VendorProcessServiceTypes.Where(e => e.Id == service.SubCategoryId).Select(w => w.Cost).FirstOrDefault();
            

            var phoneNumber = _http.HttpContext.Session.GetString("customerPhone");
            var serviceName = _context.CustomerRequestServices.Where(e => e.Id == service.ServiceId).Select(p => p.ServiceName).FirstOrDefault();
            var serviceSubCategoryName = _context.VendorProcessServiceTypes.Where(e => e.Id == service.SubCategoryId).FirstOrDefault();
            ////var getNearestVendor = await CalculateShortestDistance(service.CustomerAddress, null, serviceSubCategoryName.ServiceTypeName, serviceName);
            //if (getNearestVendor == null)
            //{
            //    return null;
            //}
            //if (getNearestVendor.Distance > 5)
            //{
            //    return null;
            //}
            //var getExpertisePrice = _context.VendorProcessServiceTypes.Where(e => e.Id == service.SubCategoryId).Select(u => u.Cost).FirstOrDefault();
            var issues = _context.Issues.Where(e => e.Id == service.IssueId).FirstOrDefault();
            var serviceModel = new ResquService
            {
                ServiceId = service.ServiceId,
                IssueId = service.IssueId,
                IssuePrice = issues.Price.ToString(),
                IssueDescription = issues.Description,
                ServiceName = serviceName,
                SubCategoryId = serviceSubCategoryName.Id,
                SubCategoryName = serviceSubCategoryName.ServiceTypeName,
                SubCategoryPrice = serviceSubCategoryName.Cost,
                Price = serviceSubCategoryName.Cost + issues.Price,
                BookingId = $"{serviceSubCategoryName.ServiceTypeName.Substring(0, 3).ToUpper()}/{GenerateRandom(10)}",
                IsStarted = false,
                Description = service.Description,
                Status = "Price Estimated",
                DateCreated = DateTime.Now,
                //CustomerLocation = service.CustomerAddress,
                CustomerPhone = phoneNumber,
                //VendorId = getNearestVendor.VendorId.ToString(),
                //VendorName = getNearestVendor.VendorName,
                //VendorPhone = getNearestVendor.Phone,
                CustomerId = _context.Customers.Where(w => w.PhoneNumber == phoneNumber).Select(e => e.Id).FirstOrDefault().ToString(),
                CustomerName = _context.Customers.Where(w => w.PhoneNumber == phoneNumber).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault()
            };
            _http.HttpContext.Session.SetString("bookingId", serviceModel.BookingId);
            _context.ResquServices.Add(serviceModel);
            _context.SaveChanges();
            return new EstimatePriceResponseDto
            {
                EstimatedPrice = getEstimatedPrice.ToString(),
                BookingId = serviceModel.BookingId
            };
        }
        public async Task<ServiceResponseDto> BookService(ServiceDto service)
        {
            string bookingId = null;
            if (string.IsNullOrEmpty(service.BookingId))
            {
             bookingId = _http.HttpContext.Session.GetString("bookingId");

            }
            else
            {
                bookingId = service.BookingId;
            }
            var getBookingDetails = _context.ResquServices.Where(b => b.BookingId == bookingId).FirstOrDefault();
            var phoneNumber = _http.HttpContext.Session.GetString("customerPhone");
            var serviceName = _context.CustomerRequestServices.Where(e => e.Id == getBookingDetails.ServiceId).Select(p => p.ServiceName).FirstOrDefault();
            var serviceSubCategoryName = _context.VendorProcessServiceTypes.Where(e => e.Id == getBookingDetails.SubCategoryId).FirstOrDefault();
            var getNearestVendor = await CalculateShortestDistance(service.CustomerAddress, null, serviceSubCategoryName.ServiceTypeName, serviceName);
            if (getNearestVendor == null)
            {
                return null;
            }
            if (getNearestVendor.Distance > 5)
            {
                return null;
            }
            var getExpertisePrice = _context.VendorProcessServiceTypes.Where(e => e.Id == getBookingDetails.SubCategoryId).Select(u => u.Cost).FirstOrDefault();
            var issues = _context.Issues.Where(e => e.Id == getBookingDetails.IssueId).FirstOrDefault();
            var serviceModel = new ResquService
            {
                ServiceId = getBookingDetails.ServiceId,
                IssueId = getBookingDetails.IssueId,
                IssuePrice = issues.Price.ToString(),
                IssueDescription = issues.Description,
                ServiceName = serviceName,
                SubCategoryId = serviceSubCategoryName.Id,
                SubCategoryName = serviceSubCategoryName.ServiceTypeName,
                SubCategoryPrice = serviceSubCategoryName.Cost,
                Price = serviceSubCategoryName.Cost + issues.Price,
                BookingId = bookingId,
                IsStarted = false,
                Description = getBookingDetails.Description,
                Status = "Service Booked",
                DateCreated = DateTime.Now,
                CustomerLocation = service.CustomerAddress,
                CustomerPhone = phoneNumber,
                VendorId = getNearestVendor.VendorId.ToString(),
                VendorName = getNearestVendor.VendorName,
                VendorPhone = getNearestVendor.Phone,
                CustomerId = _context.Customers.Where(w => w.PhoneNumber == phoneNumber).Select(e => e.Id).FirstOrDefault().ToString(),
                CustomerName = _context.Customers.Where(w => w.PhoneNumber == phoneNumber).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault()
            };

            _context.ResquServices.Add(serviceModel);
            _context.SaveChanges();
            return new ServiceResponseDto
            {
                Description = getBookingDetails.Description,
                ServiceName = serviceModel.ServiceName,
                SubCategoryName = serviceModel.SubCategoryName,
                BookingId = bookingId,
                VendorName = getNearestVendor.VendorName,
                VendorGender = getNearestVendor.Gender,
                VendorPhone = getNearestVendor.Phone,
                Distance = getNearestVendor.Distance,
                Time = Math.Round(getNearestVendor.Time),
                ServiceAmount = serviceModel.Price.ToString()
            };
        }

        public async Task<List<ServiceListDto>> ServiceList()
        {
            var getAllService = await _context.CustomerRequestServices.Select(d => new ServiceListDto
            {
                ServiceId = d.Id,
                ServiceName = d.ServiceName
            }).ToListAsync();
            return getAllService;
        }

        public async Task<List<ServiceCategoryListDto>> ServiceCategoryList()
        {
            var getAllServiceCategory = await _context.VendorProcessServiceTypes.Select(d => new ServiceCategoryListDto
            {
                ServiceCategoryId = d.Id,
                ServiceCategoryName = d.ServiceTypeName,
                ServiceCategoryPrice = d.Cost
            }).ToListAsync();
            return getAllServiceCategory;
        }

        public async Task<GetServiceCategoryByServiceDto> GetServiceCategoryByService(int serviceId)
        {
            var getServiceCategoryByService =  _context.ServiceToSericeCategorys.Where(c => c.ServiceId == serviceId)
                .Select(w => new GetServiceCategoryByServiceDto
                {
                    ServiceName = _context.CustomerRequestServices.Where(s=>s.Id == serviceId).Select(x=>x.ServiceName).FirstOrDefault(),
                    ServiceCategorysDtos = _context.ServiceToSericeCategorys.Where(e => e.ServiceId == serviceId).Select(s => new ServiceCategorysDto
                    {
                        Id = s.ServiceTypeId.Value,
                        ServiceCategoryName = _context.VendorProcessServiceTypes.Where(g=>g.Id == s.ServiceTypeId).Select(x => x.ServiceTypeName).FirstOrDefault(),
                        Price = _context.VendorProcessServiceTypes.Where(g => g.Id == s.ServiceTypeId).Select(x => x.Cost).FirstOrDefault()
                    }).ToList()
                }).FirstOrDefault();
            return getServiceCategoryByService;
        }


        public async Task<List<GetIssueDto>> GetIssueByServiceTypeId(int serviceTypeId)
        {
            var getServiceCategoryByService = await _context.Issues.Where(c => c.ServiceTypeId == serviceTypeId)
                .Select(w => new GetIssueDto
                {
                    Id = w.Id,
                    IssueDescription = w.Description,
                    Price = w.Price
                }).ToListAsync();
            return getServiceCategoryByService;
        }



        public async Task<List<GetAllServiceDto>> GetAllServices()
        {
            var getServiceCategoryByService = _context.CustomerRequestServices.Select(x => new GetAllServiceDto
            {
                Id = x.Id,
                ServiceName = x.ServiceName
            }).ToList();
            return getServiceCategoryByService;
        }


        public async Task<Response> AddServiceCategoryToService(AddServiceCategoryToService categoryToService)
        {
            try
            {
                var service = new ServiceToSericeCategory
                {
                    ServiceId = categoryToService.ServiceId,
                    ServiceTypeId = categoryToService.ServiceTypeId,
                    DateCreated = DateTime.Now,
                };
                await _context.ServiceToSericeCategorys.AddAsync(service);
                _context.SaveChanges();
                return new Response
                {
                    Message = "Added Successfully",
                    ResponseCode = "00"
                };
            }
            catch (Exception ex)
            {

                throw;
            }



            

        }

        public async Task<Response> AddIssue(IssuesDto issue)
        {
            try
            {
                var service = new Issue
                {
                    Description = issue.Description,
                    ServiceTypeId = issue.ServiceTypeId,
                    Price = issue.Price,
                    DateCreated = DateTime.Now,
                };
                await _context.Issues.AddAsync(service);
                _context.SaveChanges();
                return new Response
                {
                    Message = "Added Successfully",
                    ResponseCode = "00"
                };
            }
            catch (Exception ex)
            {

                throw;
            }





        }

        public async Task<EndServiceDto> EndService(string bookingId,string paymentType)
        {
            var getServiceByBookingNumber = await _context.ResquServices.Where(e => e.BookingId == bookingId).FirstOrDefaultAsync();
            if (getServiceByBookingNumber == null)
            {
                return null;
            }
            TimeSpan ts = DateTime.Now - getServiceByBookingNumber.DateStarted;
            if ( ts.TotalMinutes > Convert.ToInt32(_config.GetSection("FixedMinute").Value))
            {
                getServiceByBookingNumber.IsEnded = true;
                getServiceByBookingNumber.DateEnded = DateTime.Now;

                TimeSpan timeSpan = getServiceByBookingNumber.DateEnded - getServiceByBookingNumber.DateStarted;
                var minuteCharge = _config.GetSection("AmountPerMinute").Value;
                var doubleCharge = Convert.ToDouble(minuteCharge);
                var serviceCharge = doubleCharge * Convert.ToInt32(timeSpan.TotalMinutes);
                var materialCost = _context.Products.Where(e => e.Id == getServiceByBookingNumber.ProductId).Select(u => u.ProductPrice).FirstOrDefault();
                getServiceByBookingNumber.TotalPrice = (Convert.ToDecimal(serviceCharge) + materialCost).ToString();
                getServiceByBookingNumber.Price = Convert.ToDecimal(serviceCharge);
                getServiceByBookingNumber.ProductPrice = materialCost;
                getServiceByBookingNumber.PaymentType = paymentType;
                getServiceByBookingNumber.Status = "COMPLETED";
                _context.SaveChanges();

               
            }
            getServiceByBookingNumber.TotalPrice = (Convert.ToDouble(getServiceByBookingNumber.IssuePrice) + Convert.ToDouble(getServiceByBookingNumber.SubCategoryPrice)).ToString();
            getServiceByBookingNumber.Status = "COMPLETED";
            getServiceByBookingNumber.IsEnded = true;
            getServiceByBookingNumber.DateEnded = DateTime.Now;
            getServiceByBookingNumber.PaymentType = paymentType;
            _context.SaveChanges();
            return new EndServiceDto
            {
                BookingId = bookingId,
                StartDate = getServiceByBookingNumber.DateStarted.ToString("dd-MM-yyyy hh:mm tt"),
                EndDate = getServiceByBookingNumber.DateEnded.ToString("dd-MM-yyyy hh:mm tt"),
                ServiceCharge = 0,
                MaterialCost = 0,
                PaymentType = paymentType,
                Total = Convert.ToDecimal(getServiceByBookingNumber.TotalPrice),
                VendorName = getServiceByBookingNumber.VendorName
            };

        }

        public async Task<List<ProductListDto>> ProductList()
        {
            var productList = await _context.Products.Select(d => new ProductListDto
            {
                ProductId  = Convert.ToInt32(d.Id),
                ProductName = d.ProductName,
                ProductPrice = d.ProductPrice
            }).ToListAsync();
            return productList;
        }

        public async Task<MakePaymentResponse> MakeCashPayment(string bookingId, string paymentType)
        {
            var getPaymentDetails = _context.ResquServices.Where(w => w.BookingId == bookingId).FirstOrDefault();

            //Call Payment API here

            
            if (paymentType == "CASH")
            {
                var obj = new
                {
                    amount = getPaymentDetails.TotalPrice,
                    bookingId = getPaymentDetails.BookingId,
                    date = DateTime.Now,
                    customerName = _context.Customers.Where(c => c.PhoneNumber == getPaymentDetails.CustomerPhone).Select(r => r.FirstName + " " + r.LastName).FirstOrDefault(),
                    customerPhone = getPaymentDetails.CustomerPhone,
                    vendorId = getPaymentDetails.VendorId,
                    vendorName = getPaymentDetails.VendorName,
                    vendorPhone = getPaymentDetails.VendorPhone,
                    paymentType = paymentType

                };
                var cashPay = new CashPayment
                {
                    Amount = Convert.ToDecimal(obj.amount),
                    CustomerPhoneNo = obj.customerPhone,
                    PaymentDate = obj.date,
                    PaymentStatus = "Pending",
                    ServiceType = getPaymentDetails.ServiceName,
                    TransactionReference = "FROM PAYMENT GATEWAY",
                    VendorPhoneNumber = getPaymentDetails.VendorPhone,
                };
                _context.CashPayments.Add(cashPay);
                _context.SaveChanges();

                return new MakePaymentResponse
                {
                    Amount = cashPay.Amount,
                    BookingId = cashPay.BookingId,
                    PaymentDate = cashPay.PaymentDate,
                    PaymentType = "CASH",
                    Reference = cashPay.TransactionReference
                };
            }
            return null;
          
            


           
        }

        public async Task<MakePaymentResponse> MakeCardPayment(string bookingId, string paymentType, MakeCardRequest cardRequest)
        {
            var getPaymentDetails = _context.ResquServices.Where(w => w.BookingId == bookingId).FirstOrDefault();

            //Call Payment API here


            if (paymentType == "CARD")
            {
                var obj = new
                {
                    amount = getPaymentDetails.TotalPrice,
                    bookingId = getPaymentDetails.BookingId,
                    date = DateTime.Now,
                    customerName = _context.Customers.Where(c => c.PhoneNumber == getPaymentDetails.CustomerPhone).Select(r => r.FirstName + " " + r.LastName).FirstOrDefault(),
                    customerPhone = getPaymentDetails.CustomerPhone,
                    vendorId = getPaymentDetails.VendorId,
                    vendorName = getPaymentDetails.VendorName,
                    vendorPhone = getPaymentDetails.VendorPhone,
                    paymentType = paymentType,
                    cardNo  = cardRequest.CardNo,
                    pin = cardRequest.Pin,
                    cvv = cardRequest.Cvv, 
                    expMonth = cardRequest.ExpiryMonth,
                    expYear = cardRequest.ExpiryYear

                };

                //Call API 
                var cashPay = new CardPayment
                {
                    Amount = Convert.ToDecimal(obj.amount),
                    CustomerCardNo = obj.cardNo,
                    PaymentDate = obj.date,
                    PaymentStatus = "Pending",
                    ServiceType = getPaymentDetails.ServiceName,
                    TransactionReference = "FROM PAYMENT GATEWAY",
                    VendorPhoneNumber = getPaymentDetails.VendorPhone,
                    VendorWalletId =  cardRequest.ExpiryMonth
                };
                 await _context.CardPayments.AddAsync(cashPay);
                _context.SaveChanges();

                return new MakePaymentResponse
                {
                    Amount = cashPay.Amount,
                    BookingId = cashPay.BookingId,
                    PaymentDate = cashPay.PaymentDate,
                    PaymentType = "CASH",
                    Reference = cashPay.TransactionReference
                };
            }
            return null;


        }

        public bool DebitCredit(decimal backOfficeCost, decimal vendorCost, string backOfficeWallet, string destinationWallet,string sourceWallet, decimal totalAmount)
        {

            
            //var getSourceWallet = _context.Wallets.FirstOrDefault(f => f.WalletNo == sourceWallet);

            var getSourceWallet = _context.Wallets.FirstOrDefault(f => f.WalletNo == sourceWallet);

            getSourceWallet.Balance = getSourceWallet.Balance - totalAmount;

            var getVendorWallet = _context.Wallets.FirstOrDefault(f => f.WalletNo == destinationWallet);

            getVendorWallet.Balance = getVendorWallet.Balance + vendorCost;

            var getBackOfficeWallet = _context.Wallets.FirstOrDefault(f => f.WalletNo == backOfficeWallet);

            getBackOfficeWallet.Balance = getBackOfficeWallet.Balance + backOfficeCost;

            _context.SaveChanges();
            return true;
        }

        

        public async Task<MakePaymentResponse> MakePayment(string bookingId)
        {
            var payment = _context.ResquServices.Where(r => r.BookingId == bookingId).FirstOrDefault();
            var vendorCost = Convert.ToDecimal(payment.TotalPrice) * (Convert.ToInt32(_config.GetSection("ResqPercentage").Value) / 100);

            var getCustomerId = _context.Customers.Where(c => c.PhoneNumber == payment.CustomerPhone).Select(e => e.Id).FirstOrDefault().ToString();
            var checkCustomerBalance = _context.Wallets.Where(g => g.UserId == getCustomerId).Select(e => e.Balance).FirstOrDefault();
            if (checkCustomerBalance < Convert.ToDecimal(payment.TotalPrice))
            {
                return new MakePaymentResponse
                {
                    Response = "Insufficient Fund, Kindly fund your wallet",
                    Status = false
                };
            }
            var makeWallet = new MakeWalletRequest
            {
                VendorCost = vendorCost,
                BackOfficeCost = Convert.ToDecimal(payment.TotalPrice) - vendorCost,
                BackOfficeWallet = _context.Wallets.Where(e => e.UserId == "BackOffice").Select(v => v.WalletNo).FirstOrDefault(),
                DestinationWallet = _context.Wallets.Where(e=>e.UserId == payment.VendorId).Select(v=>v.WalletNo).FirstOrDefault(),
                Amount = Convert.ToDecimal(payment.TotalPrice),
                SourceWallet = _context.Wallets.Where(e => e.UserId == getCustomerId).Select(v => v.WalletNo).FirstOrDefault(),
            };

            var makePayment = DebitCredit(makeWallet.BackOfficeCost, makeWallet.VendorCost, makeWallet.BackOfficeWallet, makeWallet.DestinationWallet, makeWallet.SourceWallet, makeWallet.Amount);
            if (makePayment == false)
            {
                return new MakePaymentResponse
                {
                    Response = "Failed",
                    Status = false
                };
            }
            var transaction = new Transaction
            {
                TotalAmount = makeWallet.Amount,
                CustomerName = _context.Customers.Where(s => s.PhoneNumber == payment.CustomerPhone).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                VendorAmount = makeWallet.VendorCost,
                PlatformCharge = makeWallet.BackOfficeCost,
                PaymentType = "WALLET",

                ServiceDate = DateTime.Now.ToString("yyyy-MM-ddd hh:MMMM"),
                VendorName = payment.VendorName,
                Status = "Completed",
                VendorId = int.Parse(payment.VendorId),
                VendorTransactionType = "CR",
                CustomerTransactionType = "DR",
                BackOfficeTransactionType = "CR",
                ServiceType = payment.ServiceName,
                SubCategory = payment.SubCategoryId.ToString(),
                BookingId = bookingId
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return new MakePaymentResponse
            {
                Response = "Successful",
                Status = true,
                Amount  = makeWallet.Amount,
                BookingId = payment.BookingId,
                PaymentDate = DateTime.Now,
                PaymentType = "WALLET",
                Reference = Guid.NewGuid().ToString()
            };

        }

        public async Task<TransferToWalletResponseDto> TransferToWallet(TransferToWalletRequestDto transfer)
        {
            var checkSourceWallet = await _context.Wallets.Where(e => e.WalletNo == transfer.SourceWallet).FirstOrDefaultAsync();
            if (checkSourceWallet == null)
            {
                return new TransferToWalletResponseDto
                {
                    Response = "The Wallet is not Found, Kindly use the right wallet",
                    Status = false
                };
            }

            var checkDestinationWallet = await _context.Wallets.Where(d => d.WalletNo == transfer.DestinationWallet).FirstOrDefaultAsync();
            if (checkDestinationWallet == null)
            {
                return new TransferToWalletResponseDto
                {
                    Response = "The Wallet is not Found, Kindly use the right wallet",
                    Status = false
                };
            }

            checkDestinationWallet.Balance = checkDestinationWallet.Balance - transfer.Amount;
            checkSourceWallet.Balance = checkSourceWallet.Balance + transfer.Amount;

            _context.SaveChanges();

            return new TransferToWalletResponseDto
            {
                Response = $"A sum of {transfer.Amount} was Successfully Transfered from {transfer.DestinationWallet} to {transfer.SourceWallet}",
                Status = true
            };
        }

        public async Task<WalletBalanceResponseDto> GetWalletBalance(WalletBalanceRequestDto walletBalance)
        {
            var validatePin = await _context.Customers.Where(s => s.PhoneNumber == walletBalance.PhoneNo).SingleOrDefaultAsync();
            if (validatePin.Pin != EncodePin(walletBalance.Pin))
            {
                return new WalletBalanceResponseDto
                {
                    Response = "Invalid Pin",
                    Status = false
                };
            }

            var balance = _context.Wallets.Where(c => c.UserId == validatePin.Id.ToString() && c.WalletNo == walletBalance.WalletNo).Select(e=>new WalletBalanceResponseDto { 
            Balance = e.Balance,
            FullName = validatePin.FirstName + " " + validatePin.LastName,
            Response = "Success",
            Status = true,
            UserId = validatePin.Id.ToString(),
            WalletNo = walletBalance.WalletNo
            }).FirstOrDefault();
            return balance;
        }


        public async Task<WalletBalanceResponseDto> GetCustomerWalletBalance(CustomerWalletBalanceRequestDto walletBalance)
        {
            var customerDetails = await _context.Customers.Where(s => (s.EmailAddress == walletBalance.UserName || s.PhoneNumber == walletBalance.UserName) && s.Pin == EncodePin(walletBalance.Password)).SingleOrDefaultAsync();
            if (customerDetails == null)
            {
                return new WalletBalanceResponseDto
                {
                    Response = "Wallet does not exist",
                    Status = false
                };
            }

            var balance =  await _context.WalletInfos.Where(c => c.PhoneNumber == customerDetails.PhoneNumber).Select(e => new WalletBalanceResponseDto
            {
                Balance = Convert.ToDecimal(e.Balance),
                FullName = e.CustomerName,
                Response = "Success",
                Status = true,
                UserId = e.CustomerId,
                WalletNo = e.WalletId,
                MobileNo = e.PhoneNumber
            }).FirstOrDefaultAsync();
            return balance;
        }

        public async Task<PayoutResponseDto> PayOut(PayoutRequestDto payout)
        {

            throw new NotImplementedException();
        }

        public async Task<DedicatedAccountResponse> CreateCustomerAccount(DedicatedAccountRequest request)
        {
            var client = new RestClient(_config.GetSection("WalletAPI:CreateAccount").Value);
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Authorization", $"Bearer {_config.GetSection("WalletAPI:SecretKey").Value}");
            restRequest.AddHeader("Content-Type", "application/json");
            var reqestParams = new
            {
                email = request.Email,
                first_name = request.FirstName,
                last_name = request.LastName,
                phone = request.Phone
            };
            var body = JsonConvert.SerializeObject(reqestParams);
            restRequest.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse<DedicatedAccountResponse> response = await client.ExecuteAsync<DedicatedAccountResponse>(restRequest);
            return response.Data;
        }

        public async Task<DedicatedNubanAccountResponse> CreateDedicatedNubanAccount(DedicatedNubanAccountRequest request)
        {
            var client = new RestClient(_config.GetSection("WalletAPI:CreateDedicatedNuban").Value);
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Authorization", $"Bearer {_config.GetSection("WalletAPI:SecretKey").Value}");
            restRequest.AddHeader("Content-Type", "application/json");

            var body = JsonConvert.SerializeObject(request);

            restRequest.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse<DedicatedNubanAccountResponse> response = await client.ExecuteAsync<DedicatedNubanAccountResponse>(restRequest);
            return response.Data;
        }

        public async Task<RateVendorResponseDto> RateVendor(RateVendorDto rateVendor)
        {
            try
            {
                string bookingId = _http.HttpContext.Session.GetString("bookingId");
                var getVendorByBooking = await _context.ResquServices.Where(e => e.BookingId == bookingId).FirstOrDefaultAsync();
                if (getVendorByBooking == null)
                {
                    return new RateVendorResponseDto
                    {
                        Message = "Invalid Transaction",
                        Status = false
                    };
                }
                if (getVendorByBooking.IsVendorAccepted != true)
                {
                    return new RateVendorResponseDto
                    {
                        Message = "You can't rate a vendor that have not accepted your request",
                        Status = false
                    };
                }
                var rate = new VendorRating
                {
                    Rating = rateVendor.StarRating,
                    UserId = Convert.ToInt32(getVendorByBooking.CustomerId),
                    VendorId = Convert.ToInt32(getVendorByBooking.VendorId),
                    BookingId = bookingId,
                    ServiceType = getVendorByBooking.ServiceName,
                    CreatedAt = DateTime.Now
                };
                _context.VendorRatings.Add(rate);
                _context.SaveChanges();
                return new RateVendorResponseDto
                {
                    Message = "Rated Successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new RateVendorResponseDto
                {
                    Message = $"{ex.Message}",
                    Status = false
                };
            }
        }

        public async Task<OtpConfirmationResponseDto> ConfirmOtp(OtpDto otp)
        {
            //otp.Phone = otp.Phone.Substring(4, 10);
            
            var validateOtp = await _context.Otps.Where(e => e.Phone == otp.Phone && e.OtpNumber == otp.Otp).FirstOrDefaultAsync();

            //var otpExpiry = validateOtp.ExpiryDate;
            //var currentDate = DateTime.Now;
            var check1 = DateTime.Now > validateOtp.ExpiryDate;
            var check2 = validateOtp.ExpiryDate < DateTime.Now;
            if (DateTime.Now > validateOtp.ExpiryDate)
            {
                validateOtp.Status = "EXPIRED";
                _context.SaveChanges();
                return new OtpConfirmationResponseDto
                {
                    Message = "The Token has expired",
                    Status = false
                };
            }
            if (validateOtp != null && DateTime.Now < validateOtp.ExpiryDate)
            {
                validateOtp.Status = "USED";
                var getCustomer = _context.Customers.Where(w => w.PhoneNumber == validateOtp.Phone).FirstOrDefault();
                getCustomer.IsOtpVerified = true;
                _context.SaveChanges();
                return new OtpConfirmationResponseDto
                {
                    Message = "Success",
                    Status = true
                };
            }

            if (validateOtp == null)
            {
                return new OtpConfirmationResponseDto
                {
                    Message = "Invalid Otp",
                    Status = false
                };
            }

            return new OtpConfirmationResponseDto
            {
                Message = "An Error Occurred",
                Status = false
            };
        }

        public async Task<OtpGenerateResponseDto> GenerateOtp(string phoneNo)
        {
            var getCustomer = await _context.Customers.Where(e => e.PhoneNumber == phoneNo).FirstOrDefaultAsync();
            if (getCustomer == null)
            {
                return new OtpGenerateResponseDto
                {
                    Status = false,
                    Token = null,
                    Message = "Invalid user"
                };
            }

            if (getCustomer != null)
            {
                var tokenizer = _context.Otps.Where(o => o.Phone == phoneNo).FirstOrDefault();
                if (tokenizer == null)
                {

                    var otps = GenerateRandom(6);
                    var toks = new Otp
                    {
                        DateCreated = DateTime.Now,
                        ExpiryDate = DateTime.Now.AddMinutes(5),
                        OtpNumber = otps,
                        Phone = phoneNo,
                        Status = "CREATED",
                    };
                    _context.Otps.Add(toks);
                    _context.SaveChanges();
                    return new OtpGenerateResponseDto
                    {
                        Status = true,
                        Token = tokenizer.OtpNumber,
                        Message = "Otp Generated Successfully"
                    };
                }
                if (tokenizer != null)
                {
                    var otpss = GenerateRandom(6);
                    tokenizer.OtpNumber = otpss;
                    tokenizer.DateCreated = DateTime.Now;
                    tokenizer.ExpiryDate = tokenizer.DateCreated.AddMinutes(5);
                    tokenizer.Status = "UPDATED";
                    _context.SaveChanges();
                    return new OtpGenerateResponseDto
                    {
                        Status = true,
                        Token = tokenizer.OtpNumber,
                        Message = "Otp Generated Successfully"
                    };
                }
            }
            return null;
        }

        public async Task<ConnectResponseDto> Connect(ConnectRequestDto connect)
        {
            
            throw new NotImplementedException();
        }

        public async Task<RootObject> GetAddress(double lat, double lon)
        {
          

                WebClient webClient = new WebClient();

                webClient.Headers.Add("user-agent", "Mozilla/4.0(compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                webClient.Headers.Add("Referer", "http://www.microsoft.com");

                var jsonData = webClient.DownloadData("http://nominatim.openstreetmap.org/reverse?format=json&lat=" + lat + "&lon=" + lon);

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));

                RootObject rootObject = (RootObject)ser.ReadObject(new MemoryStream(jsonData));

                return rootObject;
        }

        public async Task<double> CalculateDistance(double slat, double slon, double dlat, double dlon)
        {

            var sourceCord = new  GeoCoordinate(slat,slon);
            var destCord = new GeoCoordinate(dlat, dlon);

            return sourceCord.GetDistanceTo(destCord);
        }

        public async Task<VendorDistanceResponseDto> CalculateShortestDistance(string customerLocation, string vendorLocation, string subCategory, string serviceName)
        {
            List<VendorDistanceRequestDto> destination = new List<VendorDistanceRequestDto>();
            var getCustomerLatLong = await GetLatitudeLongitudeByAddress(customerLocation);
            var getExpertiseIdByName = _context.CustomerRequestServices.Where(e => e.ServiceName == serviceName).Select(d=>d.Id).FirstOrDefault();
            //var getSubCategory = _context.VendorServiceSubCategories.Where(e => e.ServiceName == serviceName).ToList();
            var linqQuery = (from cats in _context.CustomerRequestServices
                            where cats.ServiceName == serviceName
                            join ven in _context.Vendors.Where(r=>r.CustomerRequestServiceId == getExpertiseIdByName)
                            on cats.Id equals ven.CustomerRequestServiceId
                            select new
                            {
                                ven.Id,
                                cats.ServiceName,
                            }).ToList();
            var getVendorAddresses = new List<Entities.Vendor>();
            foreach (var subCat in linqQuery)
            {
                var getVendorInfo = _context.Vendors.Where(v => v.CustomerRequestServiceId == getExpertiseIdByName && v.AvailabilityStatus == "Online" && v.Id == subCat.Id).FirstOrDefault();
                if (getVendorInfo != null)
                {
                    getVendorAddresses.Add(getVendorInfo);
                }
            }
            foreach (var contactAddress in getVendorAddresses)
            {
                var getVendorLatLong = await GetLatitudeLongitudeByAddress(contactAddress.ContactAddress);
                destination.Add(new VendorDistanceRequestDto { Latitude = getVendorLatLong.lat, Longitude = getVendorLatLong.lng, VendorId = contactAddress.Id, VendorName = contactAddress.FirstName + " " + contactAddress.LastName });
            }
            List<VendorDistanceResponseDto> vendors = new List<VendorDistanceResponseDto>();
            foreach (var dest in destination)
            {
                var sourceDestination = new GeoCoordinate(getCustomerLatLong.lat, getCustomerLatLong.lng);
                var vendorDestination = new GeoCoordinate(dest.Latitude, dest.Longitude);
                var distance = sourceDestination.GetDistanceTo(vendorDestination);
                var getTime = await GetTravelTime(Convert.ToSingle(distance));
                var vend = new VendorDistanceResponseDto
                {
                    Distance = sourceDestination.GetDistanceTo(vendorDestination)/1000,
                    VendorId = dest.VendorId,
                    VendorName = dest.VendorName,
                    Price = _context.ExpertiseCategories.Where(s => s.ExpertiseId == getExpertiseIdByName).Select(p => p.Price).FirstOrDefault(),
                    ServiceName = serviceName,
                    ServiceSubCategory = subCategory,
                    Gender = _context.Vendors.Where(v => v.Id == dest.VendorId).Select(d => d.Gender).FirstOrDefault(),
                    Phone = _context.Vendors.Where(v => v.Id == dest.VendorId).Select(d => d.PhoneNo).FirstOrDefault(),
                    Time = Math.Round(getTime)
                };
                vendors.Add(vend);

                //var secondVendor = new GeoCoordinate(dest.Latitude, dest.Longitude);
                //vendors.Add(new VendorDistanceResponseDto
                //{
                //    Distance = sourceDestination.GetDistanceTo(secondVendor),
                //    VendorId = dest.VendorId,
                //    VendorName = dest.VendorName
                //});


                //var thirdVendor = new GeoCoordinate(dest.Latitude, dest.Longitude);
                //vendors.Add(new VendorDistanceResponseDto
                //{
                //    Distance = sourceDestination.GetDistanceTo(thirdVendor),
                //    VendorId = dest.VendorId,
                //    VendorName = dest.VendorName
                //});
            }
            var pickNearestDistance = vendors.OrderBy(e => e.Distance).FirstOrDefault();
            return pickNearestDistance;
        }

        public async Task<Location> GetLatitudeLongitudeByAddress(string address)
        {
            var client = new 
                RestClient(_config.GetSection("Location:ApiUrl").Value.Replace("@apikey",_config.GetSection("Location:Api_key").Value).Replace("@address",address));
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse<Root> response = await client.ExecuteAsync<Root>(request);
            var loc = new Location();
            foreach (var resp in response.Data.results)
            {

                loc.lat = resp.geometry.location.lat;
                loc.lng = resp.geometry.location.lng;
            }
            return loc;
        }

        public  Task<float> GetTravelTime(float distance)
        {
            int speed = Convert.ToInt32(_config.GetSection("TravelTime").Value);
            float time = distance/speed;
            var timeInMinutes = (time % 3600) / 60;
            return Task.FromResult<float>(timeInMinutes);
        }

        public async Task<AcceptRequestDto> AcceptRequest(string bookingId)
        {
            var getBookingDetails = await _context.ResquServices.Where(e => e.BookingId == bookingId).FirstOrDefaultAsync();
            _http.HttpContext.Session.SetString("bookingId", bookingId);
            getBookingDetails.IsVendorAccepted = true;
            _context.SaveChanges();
            return new AcceptRequestDto
            {
                BookingId = getBookingDetails.BookingId,
                CustomerName = getBookingDetails.CustomerName,
                Location = getBookingDetails.CustomerLocation,
                ServiceDescription = getBookingDetails.Description,
                ServiceName = getBookingDetails.ServiceName,
                Message = "Accepted",
                Status = true
            };
        }

        public async Task<UpdateCustomerResponseDto> RejectRequest(string bookingId)
        {
            var getBookingDetails = await _context.ResquServices.Where(e => e.BookingId == bookingId).FirstOrDefaultAsync();
            getBookingDetails.IsVendorRejected = true;
            _context.SaveChanges();
            return new UpdateCustomerResponseDto
            {
                Message = "Rejected",
                Status = true
            };
        }

        public async Task<UpdateCustomerResponseDto> GoOnline(string mobileNo)
        {
            var getVendor = await _context.Vendors.Where(x => x.PhoneNo == mobileNo).FirstOrDefaultAsync();
            if (getVendor.AvailabilityStatus == "Online")
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "Availability Status already Updated to Online",
                    Status = false
                };
            }
            if (getVendor  != null && (getVendor.AvailabilityStatus == "Offline" || getVendor.AvailabilityStatus == null))
            {
                getVendor.AvailabilityStatus = "Online";
                _context.SaveChanges();
                return new UpdateCustomerResponseDto
                {
                    Message = "Availability Status Updated to Online",
                    Status = true
                };
            }
            return new UpdateCustomerResponseDto
            {
                Message = "An Error Occurred",
                Status = false
            };
            
        }

        public async Task<UpdateCustomerResponseDto> GoOffline(string mobileNo)
        {
            var getVendor = await _context.Vendors.Where(x => x.PhoneNo == mobileNo).FirstOrDefaultAsync();
            if (getVendor != null && getVendor.AvailabilityStatus == "Offline")
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "Availability Status already Updated to Offline",
                    Status = false
                };
            }
            if (getVendor != null && getVendor.AvailabilityStatus == "Online")
            {
                getVendor.AvailabilityStatus = "Offline";
                _context.SaveChanges();
                return new UpdateCustomerResponseDto
                {
                    Message = "Availability Status Updated to Offline",
                    Status = true
                };
            }
            return new UpdateCustomerResponseDto
            {
                Message = "An Error Occurred",
                Status = false
            };
        }

        public async Task<List<ServiceCategoryDto>> ServiceCategoryByExpertise(int expertiseId)
        {

            var serviceCats =  await 
                _context.Expertises.Where(d => d.Id == expertiseId).Select(w => new ServiceCategoryDto
                {
                ServiceCategoryId = w.ExpertiseCategoryId.Value,
                ServiceCategoryName = _context.ExpertiseCategories.Where(e => e.ExpertiseId == expertiseId).Select(s => new ServiceCatDto { 
                Name= s.Name,
                Price = s.Price
                }).ToList(),
                
            }).ToListAsync();
            if(serviceCats == null)
            {
                return null;
            }
            return serviceCats;
        }

        public async Task<OtpConfirmationResponseDto> StartService(string bookingId)
        {
            try
            {
                var start = await _context.ResquServices.Where(w => w.BookingId == bookingId).FirstOrDefaultAsync();

                if (start.IsVendorAccepted != true)
                {
                    return new OtpConfirmationResponseDto
                    {
                        Message = "Kindly accept the request before starting",
                        Status = false
                    };
                }


                start.DateStarted = DateTime.Now;
                start.IsStarted = true;
                await _context.SaveChangesAsync();
                return new OtpConfirmationResponseDto
                {
                    Message = "Started Successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new OtpConfirmationResponseDto
                {
                    Message = $"Error {ex}",
                    Status = false
                };
            }
            
        }

        public async Task<OtpConfirmationResponseDto> EndService(string bookingId)
        {
            try
            {
                var end = await _context.ResquServices.Where(w => w.BookingId == bookingId).FirstOrDefaultAsync();

                if (end.IsVendorAccepted != true)
                {
                    return new OtpConfirmationResponseDto
                    {
                        Message = "Kindly accept the request before starting",
                        Status = false
                    };
                }

                if (end.IsStarted != true)
                {
                    return new OtpConfirmationResponseDto
                    {
                        Message = "The Service Needs to be started before it can end",
                        Status = false
                    };
                }

                if (end.IsVendorAccepted == true && end.IsStarted == true)
                {
                    end.DateEnded = DateTime.Now;
                    end.IsEnded = true;
                    await _context.SaveChangesAsync();
                    return new OtpConfirmationResponseDto
                    {
                        Message = "Ended Successfully",
                        Status = true
                    };
                }

                return new OtpConfirmationResponseDto
                {
                    Message = "Outside the condition",
                    Status = false
                };
               
            }
            catch (Exception ex)
            {
                return new OtpConfirmationResponseDto
                {
                    Message = $"Error {ex}",
                    Status = false
                };
            }
        }

        //public async Task<double> CalculateServiceCost(string bookingId)
        //{
        //    var getService = _context.ResquServices.Where(r => r.BookingId == bookingId).FirstOrDefault();
        //    if (getService == null)
        //    {
        //        return 0;
        //    }
        //    var getPrice = getService.TotalPrice;
        //    if (getService.DateEnded.Minute - getService.DateStarted.Minute > Convert.ToInt32(_config.GetSection("").Value))
        //    {
        //        var extraTime = (getService.DateEnded.Minute + getService.DateStarted.Minute) - (getService.DateEnded.Minute - getService.DateStarted.Minute);

        //    }
        //}
        public async Task<List<GetAllServiceDto>> GetServiceByName(GetServiceByNameRequest request)
        {
            var getServiceCategoryByService = await _context.CustomerRequestServices.Where(e=>e.ServiceName.Contains(request.ServiceName)).Select(x => new GetAllServiceDto
            {
                Id = x.Id,
                ServiceName = x.ServiceName
            }).ToListAsync();
            return getServiceCategoryByService;
        }

        public async Task<CustomerRequestResponseDto> CustomerRequestDetails(string vendorId)
        {

            var getTodaysDateMinutes = DateTime.Now.Minute;
            CustomerRequestResponseDto service = new CustomerRequestResponseDto
            {

            };
            var getCustomerRequestDetails = _context.ResquServices.Where(e => e.VendorId == vendorId && e.IsVendorAccepted == false).ToList();
            foreach (var request in getCustomerRequestDetails)
            {
                service.BookingId = request.BookingId;
                service.Description = request.Description;
                service.ServiceName = request.ServiceName;
                service.SubCategoryName = request.SubCategoryName;
                service.CustomerName = request.CustomerName;
                service.CustomerPhone = request.CustomerPhone;
            }
            return service;
        }

        public Task<GetNearestVendorByLocationResponse> GetLatitudeLongitudeByAddress(GetNearestVendorByLocationRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> BookNow(string bookingId)
        {
            var getBookingById = await _context.ResquServices.Where(e => e.BookingId == bookingId).FirstOrDefaultAsync();
            if (getBookingById == null)
            {
                return false;
            }
            getBookingById.Status = "Booked";
            _context.SaveChanges();
            return true;
        }

        public async Task<string> SendNotification(string message)
        {
            try
            {
                var name = FirebaseApp.DefaultInstance;

                var dev = new FirebaseAdmin.Messaging.Notification();
                dev.Body = message;
                dev.Title = "SERVICE REQUEST";
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(jsonCredential),
                    ServiceAccountId = "rezq-project@appspot.gserviceaccount.com",
                });

                var uid = Guid.NewGuid().ToString();
                string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
                return customToken;
            }
            catch (Exception ex)
            {
                var uid = Guid.NewGuid().ToString();
                string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
                return customToken;
            }
        }
        public async Task<string> GenerateFirebaseToken(string customerGuid)
        {
            try
            {
                var name  = FirebaseApp.DefaultInstance;
                var dev = new FirebaseAdmin.Messaging.Notification();
                
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(jsonCredential),
                    ServiceAccountId = "rezq-project@appspot.gserviceaccount.com",
                });

                string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(customerGuid);
                return customToken;
            }
            catch (Exception ex)
            {
                var uid = Guid.NewGuid().ToString();
                string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
                return customToken;
            }

        }

        public async Task<AddCardResponse> DeleteCard(long id)
        {
            var checkCard = _context.Cards.Where(e => e.Id == id).FirstOrDefault();
            if (checkCard == null)
            {
                return new AddCardResponse
                {
                    ResponseCode = "99",
                    ResponseMessage = "Card Does not Exist",
                    ResponseStatus = false
                };
            }

            _context.Cards.Remove(checkCard);
            await _context.SaveChangesAsync();
            return new AddCardResponse
            {
                ResponseCode = "00",
                ResponseMessage = "Card Deleted Successfully",
                ResponseStatus = true
            };
        }

        public async Task<UpdateCustomerResponseDto> UpdateWalletBalance(UpdateWalletBalanceDto balanceDto)
        {

            var getWalletDetails =await _context.WalletInfos.Where(e => e.CustomerId == balanceDto.CustomerId).FirstOrDefaultAsync();
            if (getWalletDetails == null)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "Account does not exist",
                    Status = false
                };
            }

            var customerInflow = new CustomerInflow
            {
                Amount = balanceDto.Amount,
                Channel = balanceDto.Channel,
                CustomerId = balanceDto.CustomerId,
                CustomerName = getWalletDetails.CustomerName,
                DateCreated = DateTime.Now,
                PhoneNumber = getWalletDetails.PhoneNumber,
                TransactionType = TransactionTypes.INFLOW,
                TransactionTypeDescription = TransactionTypes.CREDIT,
                Narration = balanceDto.Channel + "-" + balanceDto.Amount.ToString() + "-" +balanceDto.CustomerId + "-"+ Guid.NewGuid().ToString().Replace("-","").ToUpper(),
                WalletId = getWalletDetails.WalletId
            };
            customerInflow.CurrentBalance += balanceDto.Amount;
            await _context.CustomerInflows.AddAsync(customerInflow);


            await _context.SaveChangesAsync();

            getWalletDetails.Balance += balanceDto.Amount;

            await _context.SaveChangesAsync();

            return new UpdateCustomerResponseDto
            {
                Message = "Success",
                Status = true
            };


        }

        

        public async Task<AddCardResponse> PayVendor(PayVendorDto payVendor)
        {
            var sourceWallet = await _context.WalletInfos.Where(e => e.WalletId == payVendor.SourceWallet).FirstOrDefaultAsync();

            if (sourceWallet == null)
            {
                return new AddCardResponse
                {
                    ResponseCode = ResponseCodes.Response33Code,
                    ResponseMessage = ResponseCodes.Response33Message.Replace("@object","Source Wallet"),
                };
            }


            if (sourceWallet.Balance < payVendor.Amount)
            {
                return new AddCardResponse
                {
                    ResponseCode = ResponseCodes.Response88Code,
                    ResponseMessage = ResponseCodes.Response88Message,
                };
            }


            var destinationWallet = await _context.WalletInfos.Where(e => e.WalletId == payVendor.DestinationWallet).FirstOrDefaultAsync();


            if (destinationWallet == null)
            {
                return new AddCardResponse
                {
                    ResponseCode = ResponseCodes.Response33Code,
                    ResponseMessage = ResponseCodes.Response33Message.Replace("@object", "Destination Wallet"),
                };
            }

            //
            //Call PayStack API here to do debit and credit
            
            sourceWallet.Balance -= payVendor.Amount;
            destinationWallet.Balance += payVendor.Amount;
            var customer = new CustomerInflow
            {
                Amount = payVendor.Amount,
                Channel = "REZQ",
                CurrentBalance = sourceWallet.Balance,
                CustomerId = sourceWallet.CustomerId,
                CustomerName = sourceWallet.CustomerName,
                DateCreated = DateTime.Now,
                Narration = "REZQ" + "-" + $"{Guid.NewGuid().ToString().Replace("-", "").ToUpper()}-{sourceWallet.CustomerId}",
                PhoneNumber = sourceWallet.PhoneNumber,
                TransactionType = TransactionTypes.CREDIT,
                TransactionTypeDescription = TransactionTypes.OUTFLOW,
                WalletId = sourceWallet.WalletId
            };


            var vendor = new CustomerInflow
            {
                Amount = payVendor.Amount,
                Channel = "REZQ",
                CurrentBalance = destinationWallet.Balance,
                CustomerId = destinationWallet.CustomerId,
                CustomerName = destinationWallet.CustomerName,
                DateCreated = DateTime.Now,
                Narration = "REZQ" + "-" + $"{Guid.NewGuid().ToString().Replace("-", "").ToUpper()}-{sourceWallet.CustomerId}",
                PhoneNumber = destinationWallet.PhoneNumber,
                TransactionType = TransactionTypes.DEBIT,
                TransactionTypeDescription = TransactionTypes.INFLOW,
                WalletId = destinationWallet.WalletId
            };

            List<CustomerInflow> customers = new List<CustomerInflow>();
            customers.Add(customer);
            customers.Add(vendor);

            await _context.CustomerInflows.AddRangeAsync(customers);
            await _context.SaveChangesAsync();

            return new AddCardResponse
            {
                ResponseCode = ResponseCodes.Response00Code,
                ResponseMessage = ResponseCodes.Response00Message
            };

        }
    }
}
