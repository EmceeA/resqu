using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Resqu.Core.Constants;
using Resqu.Core.Dto;
using Resqu.Core.Entities;
using Resqu.Core.Interface;
using RestSharp;
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
                var createDedicatedAccount = await CreateCustomerAccount(new DedicatedAccountRequest
                {
                    Email = signUpModel.Email,
                    FirstName = signUpModel.FirstName,
                    LastName = signUpModel.LastName
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

                    if (getOtpByNumber != null && getOtpByNumber == signUpModel.Otp)
                    {
                        await _context.Customers.AddAsync(new Resqu.Core.Entities.Customer
                        {
                            FirstName = signUpModel.FirstName,
                            LastName = signUpModel.LastName,
                            PhoneNumber = signUpModel.PhoneNumber,
                            Pin = EncodePin(signUpModel.Pin),
                            DateCreated = DateTime.Now,
                            AccountId = createDedicatedAccount.data.id,
                            EmailAddress = signUpModel.Email,
                            IsCustomerCreated = true,
                            IsDedicatedCreated = false
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
                IsStarted = true,
                Status = "ONGOING"
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
            getServiceByBookingNumber.Status = "COMPLETED";
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
            restRequest.AddHeader("Cookie", "sails.sid=s%3ACtTz966NlJosAffqG13riHFNP8woUYuc.lrEQgc4u5itWxaAQoMU9zyVPSpDkEOB0vylLHA3HhwU");
            var body = JsonConvert.SerializeObject(request);
                
            restRequest.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse<DedicatedAccountResponse> response = await client.ExecuteAsync<DedicatedAccountResponse>(restRequest);
            return response.Data;
        }

        public async Task<DedicatedNubanAccountResponse> CreateDedicatedNubanAccount(DedicatedNubanAccountRequest request)
        {
            var client = new RestClient(_config.GetSection("WalletAPI:CreateAccount").Value);
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Authorization", $"Bearer {_config.GetSection("WalletAPI:SecretKey").Value}");
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Cookie", "sails.sid=s%3ACtTz966NlJosAffqG13riHFNP8woUYuc.lrEQgc4u5itWxaAQoMU9zyVPSpDkEOB0vylLHA3HhwU");
            var body = JsonConvert.SerializeObject(request);

            restRequest.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse<DedicatedNubanAccountResponse> response = await client.ExecuteAsync<DedicatedNubanAccountResponse>(restRequest);
            return response.Data;
        }
    }
}
