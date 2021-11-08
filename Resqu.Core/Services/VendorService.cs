using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class VendorMobileService : IVendor
    {
        private readonly ResquContext _context;
        private readonly IHttpContextAccessor _http;
        public VendorMobileService(ResquContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
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
        public async Task<AcceptRequestDto> AcceptRequest(string bookingId)
        {
            var getBookingDetails = await _context.ResquServices.Where(e => e.BookingId == bookingId).FirstOrDefaultAsync();
            var mobile  = _http.HttpContext.Session.GetString("mobileNo");
            var getVendorDetails = await _context.Vendors.Where(s => s.PhoneNo == mobile).FirstOrDefaultAsync();
            getBookingDetails.VendorGender = getVendorDetails.Gender;
            getBookingDetails.VendorId = getVendorDetails.Id.ToString();
            getBookingDetails.VendorLocation = getVendorDetails.ContactAddress;
            getBookingDetails.VendorName = getVendorDetails.CompanyName;
            getBookingDetails.VendorPhone = getBookingDetails.VendorPhone;
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
                Status = true,
            };
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
                var mobileNo = _http.HttpContext.Session.GetString("mobileNo");
                start.VendorLocation = _context.Vendors.Where(v => v.PhoneNo == mobileNo).Select(e => e.ContactAddress).FirstOrDefault();
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

        public async Task<EndServiceResponse> EndService(string bookingId)
        {
            try
            {

                var end = await _context.ResquServices.Where(w => w.BookingId == bookingId).FirstOrDefaultAsync();

                if (end.IsVendorAccepted != true)
                {
                    return new EndServiceResponse
                    {
                        Message = "Kindly accept the request before starting",
                        Status = false
                    };
                }

                if (end.IsStarted != true)
                {
                           return new EndServiceResponse
                    {
                        Message = "The Service Needs to be started before it can end",
                        Status = false
                    };
                }

                if (end.IsVendorAccepted == true && end.IsStarted == true)
                {
                    var price = _context.ResquServices.Where(e => e.BookingId == bookingId).Select(e => e.Price).FirstOrDefault();
                    end.DateEnded = DateTime.Now;
                    end.IsEnded = true;
                    var mobileNo = _http.HttpContext.Session.GetString("mobileNo");
                    end.VendorLocation = _context.Vendors.Where(v => v.PhoneNo == mobileNo).Select(e => e.ContactAddress).FirstOrDefault();
                    end.TotalPrice = price.ToString();
                    await _context.SaveChangesAsync();
                    return new EndServiceResponse
                    {
                        Message = "Success",
                        BookingId = bookingId,
                        Price = end.TotalPrice,
                        Status = true
                    };
                }

                return new EndServiceResponse
                {
                    Message = "Outside the condition",
                    Status = false
                };

            }
            catch (Exception ex)
            {
                return new EndServiceResponse
                {
                    Message = $"Error {ex}",
                    Status = false
                };
            }
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
            if (getVendor != null && (getVendor.AvailabilityStatus == "Offline" || getVendor.AvailabilityStatus == null))
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
        public async Task<string> GenerateFirebaseToken()
        {
            try
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(@"C:\Users\HFET\source\repos\Resqu.API\Resqu.Core\File\rezq-project-37b56a01bbe5.json"),
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

      

        public async Task<VendorLoginResponseDto> VendorLogin(VendorLoginRequestDto vendorLogin)
        {
            try
            {
                var getDetails = await _context.Vendors.Where(v => v.PhoneNo == vendorLogin.Phone).FirstOrDefaultAsync();
                if (getDetails == null)
                {
                    return new VendorLoginResponseDto
                    {
                        Response = "The User does not exist",
                        VendorDetails = null
                    };
                }
                else if (getDetails != null)
                {
                    if (getDetails.Pin == vendorLogin.Pin)
                    {
                        return new VendorLoginResponseDto
                        {
                            Response = "Success",
                            VendorDetails = new VendorDetails
                            {
                                AvailabilityStatus = getDetails.AvailabilityStatus,
                                ContactAddress = getDetails.ContactAddress,
                                EmailAddress = getDetails.EmailAddress,
                                NextOfKinAddress = getDetails.NextOfKinAddress,
                                CompanyName = getDetails.CompanyName,
                                CustomerRequestServiceId = getDetails.CustomerRequestServiceId,
                                FirstName = getDetails.FirstName,
                                Gender = getDetails.Gender,
                                IdentificationNumber = getDetails.IdentificationNumber,
                                LastName = getDetails.LastName,
                                MeansOfIdentification = getDetails.MeansOfIdentification,
                                MiddleName = getDetails.MiddleName,
                                NextOfKinEmail = getDetails.NextOfKinEmail,
                                NextOfKinName = getDetails.NextOfKinName,
                                NextOfKinPhone = getDetails.NextOfKinPhone,
                                NextOfKinRelationship = getDetails.NextOfKinRelationship,
                                PhoneNo = getDetails.PhoneNo,
                                PhoneNumber = getDetails.PhoneNumber,
                                VendorCode = getDetails.VendorCode,
                                VendorPicture = getDetails.VendorPicture
                            }
                        };
                    }
                    else if (getDetails.Pin != vendorLogin.Pin)
                    {
                        return new VendorLoginResponseDto
                        {
                            Response = "Phone number and Pin Combination is  not valid",
                            VendorDetails = null
                        };
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return new VendorLoginResponseDto
                {
                    Response = ex.Message,
                    VendorDetails = null
                };
            }
        }
    }
}
