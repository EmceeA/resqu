using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Resqu.Core.Dto;
using Resqu.Core.Entities;
using Resqu.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Resqu.Core.Services
{
    public class BackOfficeService : IBackOffice
    {
        private readonly ResquContext _context;
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _hosting;
        public BackOfficeService(ResquContext context, IWebHostEnvironment hosting, IHttpContextAccessor http)
        {
            _context = context;
            _hosting = hosting;
         
            _http = http;
        }


        
        public async Task<UpdateCustomerResponseDto> BanCustomer(string phone)
        {
            var customer = await _context.Customers.Where(d => d.PhoneNumber == phone && d.IsDeleted == false).FirstOrDefaultAsync();
            if (customer == null)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "The Customer does not Exist",
                    Status = false
                };  
            }
            if (customer.IsBan == true)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "The Customer has been banned before",
                    Status = false
                };
            }
            customer.IsBan = true;
            await _context.SaveChangesAsync();
            return new UpdateCustomerResponseDto
            {
                Message = "Customer Banned Successfully",
                Status = true
            };
        }

        public async Task<UpdateCustomerResponseDto> UnBanCustomer(string phone)
        {
            var customer = await _context.Customers.Where(d => d.PhoneNumber == phone && d.IsDeleted == false).FirstOrDefaultAsync();

            if (customer == null)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "The Customer does not Exist",
                    Status = false
                };
            }
            if (customer.IsBan == false)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "The Customer has been Unbanned before",
                    Status = false
                };
            }
            customer.IsBan = false;
            await _context.SaveChangesAsync();
            return new UpdateCustomerResponseDto
            {
                Message = "Customer UnBanned Successfully",
                Status = true
            };
        }

        public async Task<List<CustomerSignInResponse>> UnbannedCustomers()
        {
            var unbanned = await _context.Customers.Where(c => c.IsBan && c.IsDeleted == false).Select(c => new CustomerSignInResponse
            {
                EmailAddress = c.EmailAddress,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
            }).ToListAsync();
            if (unbanned.Count > 0)
            {
                return unbanned;
            }
            return null;
        }

        public async Task<List<CustomerSignInResponse>> BannedCustomers()
        {

            var banned = await _context.Customers.Where(c => c.IsBan == false && c.IsDeleted == false).Select(c => new CustomerSignInResponse
            {
                EmailAddress = c.EmailAddress,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                
            }).ToListAsync();
            if (banned.Count > 0)
            {
                return banned;
            }
            return null;
        }

        public async Task<UpdateCustomerResponseDto> DeleteCustomer(string phone)
        {
            var delete = await _context.Customers.Where(d => d.PhoneNumber == phone).FirstOrDefaultAsync();
            if (delete.IsDeleted == true)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "This Customer has already been deleted before",
                    Status = false
                };
            }

            delete.IsDeleted = true;
            await _context.SaveChangesAsync();
            return new UpdateCustomerResponseDto
            {
                Message = "Deleted",
                Status = false
            };
        }

        public async Task<List<VendorListDto>> VendorList()
        {
            var vendors = await _context.Vendors.Where(c=>c.IsDeleted == false
            ).Select(c => new VendorListDto
            {
                ContactAddress = c.ContactAddress,
                EmailAddress = c.EmailAddress,
                NextOfKinAddress = c.NextOfKinAddress,
                NextOfKin = c.NextOfKinRelationship,
                NextOfKinName = c.NextOfKinName,
                NextOfKinPhone = c.NextOfKinPhone,
                PhoneNumber = c.PhoneNo,
                VendorCode = c.VendorCode,
                VendorName = c.CompanyName,
                Gender = c.Gender
            }).ToListAsync();
            if (vendors == null)
            {
                return null;
            }
            return vendors;
        }

        public async Task<List<CustomerListDto>> CustomerList()
        {
            var customers = await _context.Customers.Where(c=>c.IsDeleted == false).Select(c => new CustomerListDto
            {
                EmailAddress = c.EmailAddress,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                RegulatoryIndentity = c.RegulatoryIndentity
            }).ToListAsync();
            if (customers == null)
            {
                return null;
            }
            return customers;
        }

        public async Task<UpdateCustomerResponseDto> AddExpertiseCategory(ExpertiseCategoryDto expertise)
        {
            try
            {
                var expertiseCate = new ExpertiseCategory
                {
                    Name = expertise.Name
                };
                 _context.ExpertiseCategories.Add(expertiseCate);
                await _context.SaveChangesAsync();
                return new UpdateCustomerResponseDto
                {
                    Message = "ExpertiseCategory Added Successfully",
                    Status = true
                };
            }
            catch (System.Exception ex)
            {
                return new UpdateCustomerResponseDto
                {
                    Message =ex.Message,
                    Status = false
                };
            }
        }

        public async Task<List<RequestListDto>> RequestList()
        {
            var allRequest = await _context.Requests.Select(d => new RequestListDto
            {
                VendorLocation = d.VendorLocation,
                CustomerLocation = d.CustomerLocation,
                CustomerPhone = d.CustomerPhone,
                RequestType = d.RequestType,
                VendorCode = d.RequestType
            }).ToListAsync();
            if (allRequest == null)
            {
                return null;
            }
            return allRequest;
        }

        public async Task<UpdateCustomerResponseDto> AddExpertise(ExpertiseDto expertiser)
        {
            try
            {
                var exp = new Expertise
                {
                    CreatedBy = "",
                    DateCreated = DateTime.Now,
                    Name = expertiser.ExpertiseName,
                };
                _context.Expertises.Add(exp);
                await _context.SaveChangesAsync();
                return new UpdateCustomerResponseDto
                {
                    Message = "Successfully Added",
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

        

        public async Task<UserLoginResponse> Login(UserLoginDto loginDto)
        {
            try
            {
                var usersLogin = _context.BackOfficeUsers.Where(c => c.UserName == loginDto.UserName).FirstOrDefault();
                if (usersLogin == null)
                {
                    return new UserLoginResponse
                    {
                        Response = "User does not exist",
                        Status = false
                    };
                }
                if (usersLogin.Password != loginDto.Password)
                {
                    return new UserLoginResponse
                    {
                        Response = "Username or password is not correct",
                        Status = false
                    };
                }

                else
                {
                    if (usersLogin.Password == loginDto.Password)
                    {
                        List<RoleUrl> getRoleName = _context.BackOfficeRoles.Where(d => d.Id == usersLogin.RoleId).Select(e => new RoleUrl
                        {
                            PageName = e.PageName,
                            PageUrl = e.PageUrl,
                            PageNameClass = e.PageNameClass,
                            PageUrlClass = e.PageUrlClass,
                            ActionName = e.ActionName,
                            ControllerName = e.ControllerName
                        }).ToList();
                        return new UserLoginResponse 
                        { 
                            Email = usersLogin.Email,
                            FirstName = usersLogin.FirstName,
                            LastName = usersLogin.LastName,
                            Response = "Successful",
                            RoleName = _context.BackOfficeRoles.Where(i => i.Id == usersLogin.RoleId).Select(w => w.RoleName).FirstOrDefault(),
                            RoleUrls = getRoleName,
                            Token = "jajsjhsjs",
                            UserName = usersLogin.UserName,
                            Status = true,
                            Phone = usersLogin.Phone,
                            RoleId = usersLogin.RoleId
                            
                        };
                    }
                }

                return new UserLoginResponse
                {
                    Response = "An Error Occurred",
                    Status = false
                };
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<UpdateCustomerResponseDto> AddService(ExpertiseDto expertiseDto)
        {
            try
            {
                var checkExistence = _context.VendorProcessServices.Where(s => s.CustomerRequestServiceId == expertiseDto.ExpertiseId && s.VendorProcessServiceTypeId == expertiseDto.ExpertiseCategoryId).Any();
                if (checkExistence)
                {
                    return new UpdateCustomerResponseDto
                    {
                        Message = "Product Already Exists",
                        Status = false
                    };
                }
                var vendorProcessService = new VendorProcessService
                {
                    Cost = expertiseDto.Cost,
                    CreatedBy = _http.HttpContext.Session.GetString("userName"),
                    CustomerRequestServiceId = expertiseDto.ExpertiseId.Value,
                    DateCreated = DateTime.Now,
                    Description = expertiseDto.Description,
                    VendorProcessServiceTypeId = expertiseDto.ExpertiseCategoryId.Value,
                };
                _context.VendorProcessServices.Add(vendorProcessService);
                await _context.SaveChangesAsync();
                return new UpdateCustomerResponseDto
                {
                    Message = "Successful",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = $"Error, {ex.Message}, {ex.StackTrace}",
                    Status = true
                };
            }
           
        }

        public Task<UpdateCustomerResponseDto> AddVendor(AddVendorDto addVendorDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateCustomerResponseDto> BanVendor(int  id)
        {
            var getVendor = await _context.Vendors.FindAsync(id);
            if (getVendor == null)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "Vendor not found",
                    Status = false
                };
            }
            if (getVendor.IsBan == true)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "Vendor Already Banned",
                    Status = false
                };
            }
            getVendor.IsBan = true;
           await _context.SaveChangesAsync();
            return new UpdateCustomerResponseDto
            {
                Message = "Vendor Banned Successfully",
                Status = true
            };
        }

        public async Task<UpdateCustomerResponseDto> UnBanVendor(int id)
        {
            var getVendor = await _context.Vendors.FindAsync(id);
            if (getVendor == null)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "Vendor not found",
                    Status = false
                };
            }
            if (getVendor.IsBan == false)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "Vendor Already UnBanned",
                    Status = false
                };
            }
            getVendor.IsBan = false;
            await _context.SaveChangesAsync();
            return new UpdateCustomerResponseDto
            {
                Message = "Vendor UnBanned Successfully",
                Status = true
            };
        }

        public async Task<UpdateCustomerResponseDto> DeleteVendor(int id)
        {
            var getVendor = await _context.Vendors.FindAsync(id);
            if (getVendor == null)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "Vendor not found",
                    Status = false
                };
            }
            if (getVendor.IsDeleted == true)
            {
                return new UpdateCustomerResponseDto
                {
                    Message = "Vendor Already Deleted",
                    Status = false
                };
            }
            getVendor.IsDeleted = true;
            await _context.SaveChangesAsync();
            return new UpdateCustomerResponseDto
            {
                Message = "Vendor Deleted Successfully",
                Status = true
            };
        }

        public string UploadImage(Core.Dto.Vendor createVendor)
        {

            string uniqueFileName = null;

            if (createVendor.VendorPicture !=  null)
            {
                string uploadsFolder = Path.Combine(_hosting.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + createVendor.VendorPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    createVendor.VendorPicture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;

        }

        public async Task<UpdateCustomerResponseDto> UpdateVendorProfile(int? id,Core.Dto.Vendor vendor)
        {
            var getCurrentVendor = await _context.Vendors.FindAsync(id);
            if (getCurrentVendor == null)
            {
                return new UpdateCustomerResponseDto { Message = "Vendor Profile was not found", Status = false };

            }
            getCurrentVendor.VendorPicture = UploadImage(vendor);
            
            getCurrentVendor.FirstName = vendor.FirstName;
            getCurrentVendor.MiddleName = vendor.MiddleName;
            getCurrentVendor.LastName = vendor.LastName;
            getCurrentVendor.PhoneNo = vendor.PhoneNo;
            getCurrentVendor.ContactAddress = vendor.ContactAddress;
            getCurrentVendor.IdentificationNumber = vendor.IdentificationNumber;
            getCurrentVendor.NextOfKinPhone = vendor.NextOfKinPhone;
            getCurrentVendor.EmailAddress = vendor.EmailAddress;
            getCurrentVendor.NextOfKinAddress = vendor.NextOfKinAddress;
            getCurrentVendor.MeansOfIdentification = vendor.MeansOfIdentification;
            getCurrentVendor.NextOfKinName = vendor.NextOfKinName;
            getCurrentVendor.NextOfKinAddress = vendor.NextOfKinAddress;
            await _context.SaveChangesAsync();
            return new UpdateCustomerResponseDto
            {
                Message = "Vendor Profile Updated Successfully",
                Status = true
            };
        }

        public async Task<ServiceDetail> GetDetailsById(int? id)
        {
            try
            {
                var experts = await _context.ExpertiseCategories.Where(d => d.ExpertiseId == id).Select(c => new SubCategoryList
                {
                    SubCategory = c.Name,
                    Price = c.Price.ToString()
                }).ToListAsync();

                var getDetails = _context.Expertises.Where(e => e.Id == Convert.ToInt32(id)).Select(s => new ServiceDetail
                {
                    CreatedBy = s.CreatedBy,
                    DateCreated = s.DateCreated.Value.ToString("yyyy-MM-dd hh:mm tt"),
                    Description = s.Description,
                    ServiceCategory = s.Name,
                    SubCategories = experts.Count(),
                    SubCategoryList = experts
                }).FirstOrDefault();
                return getDetails;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
