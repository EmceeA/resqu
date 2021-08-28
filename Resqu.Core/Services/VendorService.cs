using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
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
        public VendorMobileService(ResquContext context)
        {
            _context = context;
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
                return ex.Message;
            }
        }

        public Task<CustomerSignUpResponseDto> RegisterVendor(CustomerSignUpRequestDto signUpModel)
        {
            throw new NotImplementedException();
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
            
            throw new NotImplementedException();
        }
    }
}
