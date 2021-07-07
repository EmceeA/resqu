using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resq.Web.ViewModels;
using Resqu.Core.Dto;
using Resqu.Core.Entities;
using Resqu.Core.Interface;

namespace Resq.Web.Controllers
{
    public class BackOfficeController : Controller
    {
        private readonly ResquContext _context;
        private readonly IHttpContextAccessor _http;
        private IBackOffice _back;
        private readonly Resq.Web.Interface.IVendor _vendor;
        private readonly IWebHostEnvironment _hosting;
        public BackOfficeController(ResquContext context, IBackOffice back, Resq.Web.Interface.IVendor vendor, IWebHostEnvironment hosting,IHttpContextAccessor http)
        {
            _context = context;
            _back = back;
            _vendor = vendor;
            _hosting = hosting;
            _http = http;
            //_http = http;
        }

        [HttpGet]
        public async Task<IActionResult> AddVendorServiceType()
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();

            ViewData["VendorProcessServiceTypeId"] = new SelectList(_context.VendorProcessServiceTypes, "Id", "ServiceTypeName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddVendorServiceType(VendorProcessServiceType vendor)
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();

            ViewData["VendorProcessServiceTypeId"] = new SelectList(_context.VendorProcessServiceTypes, "Id", "ServiceTypeName");
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

             await _context.VendorProcessServiceTypes.AddAsync(vendor);
            _context.SaveChanges();
            return RedirectToAction("BackOfficeDashboard");
        }



        [HttpGet]
        public async Task<IActionResult> AddVendorService()
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            ViewData["CustomerRequestServiceId"] = new SelectList(_context.CustomerRequestServices, "Id", "ServiceName");
            ViewData["VendorProcessServiceTypeId"] = new SelectList(_context.VendorProcessServiceTypes, "Id", "ServiceTypeName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddVendorService(VendorProcessService vendor)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();

            await _context.VendorProcessServices.AddAsync(vendor);
            _context.SaveChanges();
            ViewData["CustomerRequestServiceId"] = new SelectList(_context.CustomerRequestServices, "Id", "ServiceName",vendor.CustomerRequestServiceId);
            ViewData["VendorProcessServiceTypeId"] = new SelectList(_context.VendorProcessServiceTypes, "Id", "ServiceTypeName", vendor.VendorProcessServiceTypeId);
            return RedirectToAction("BackOfficeDashboard");
        }







        // GET: BackOffice
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
       



        [HttpGet]

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _back.Login(loginDto);
            if (result.Status == false && result.Response == "User does not exist")
            {
                ViewBag.Message = result.Response;
                return View("Login");
            }

            if (result.Status == false && result.Response == "Username or password is not correct")
            {
                ViewBag.Message = result.Response;
                return View("Login");
            }
            if (result.Status == true)
            {
                HttpContext.Session.SetString("firstName", result.FirstName);
                HttpContext.Session.SetString("lastName", result.LastName);
                HttpContext.Session.SetString("role", result.RoleName);
                HttpContext.Session.SetString("userName", result.UserName);
                HttpContext.Session.SetString("phone", result.Phone);
                HttpContext.Session.SetString("email", result.Email);
                HttpContext.Session.SetString("roleId", result.RoleId.ToString());
                ViewBag.ProfileImage = _context.BackOfficeUsers.Where(x => x.UserName == HttpContext.Session.GetString("userName")).Select(d => d.ProfilePicture).FirstOrDefault();
                return RedirectToAction("BackOfficeDashboard");
            }
            return RedirectToAction("Login");
        }


        [HttpGet]

        public IActionResult AddService()
        {
            ViewData["ExpertiseId"] = new SelectList(_context.VendorProcessServiceTypes, "Id", "ServiceTypeName");
            ViewData["ServiceName"] = new SelectList(_context.CustomerRequestServices, "Id", "ServiceName");
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            return View();
        }

        [HttpGet]

        public IActionResult AddServices()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddService(ExpertiseDto expertiseDto)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var serve  = await _back.AddService(expertiseDto);
            if (serve.Message == "Successful" && serve.Status == true)
            {
                ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
                {
                    PageName = p.PageName,
                    PageUrl = p.PageUrl,
                    PageNameClass = p.PageNameClass,
                    PageUrlClass = p.PageUrlClass,
                    ActionName = p.ActionName,
                    ControllerName = p.ControllerName
                }).ToList();
                ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
                ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
                return RedirectToAction("ServiceList");
            }
            ViewData["ExpertiseId"] = new SelectList(_context.VendorProcessServiceTypes, "Id", "ServiceTypeName", expertiseDto.ExpertiseCategoryId);
            ViewData["ServiceName"] = new SelectList(_context.CustomerRequestServices, "Id", "ServiceName", expertiseDto.ExpertiseId);
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            ViewBag.Message = serve.Message;
            return View();
        }


        [HttpPost]
        public IActionResult AddServices(ExpertiseDto expertiseDto)
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult AddRoleToPage()
        {
            ViewData["RoleName"] = new SelectList(_context.Roles, "Id", "RoleName");
            return View();
        }

        [HttpPost]
        public IActionResult AddRoleToPage(RoleToPage page)
        {
            ViewData["RoleName"] = new SelectList(_context.Roles, "Id", "RoleName", page.RoleId);
            return View();
        }


        [HttpGet]
        public IActionResult EmptyService()
        {
            return View();
        }
        

        [HttpGet]
        public IActionResult AddVendor()
        {
            ViewData["ServiceName"] = new SelectList(_context.CustomerRequestServices, "Id", "ServiceName");
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();

            return View();
        }


        [HttpGet]
        public IActionResult RoleList()
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            var roles = _context.Roles.ToList();
            return View(roles);
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public IActionResult AddRole(RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var roler = new Role
            {
                IsDeleted = false,
                RoleName = role.RoleName
            };

            _context.Roles.Add(roler);
            _context.SaveChanges();
            //ViewData["ServiceName"] = new SelectList(_context.Expertises, "Id", "Name");
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            return RedirectToAction("BackOfficeDashboard");
        }

        [HttpPost]
        public IActionResult AddVendor(CreateVendorViewModel addVendor)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var addVend = _vendor.CreateVendor(addVendor);
            if (addVend == "Successfully Saved")
            {
                ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
                {
                    PageName = p.PageName,
                    PageUrl = p.PageUrl,
                    PageNameClass = p.PageNameClass,
                    PageUrlClass = p.PageUrlClass,
                    ActionName = p.ActionName,
                    ControllerName = p.ControllerName
                }).ToList();
                return RedirectToAction("Success");
            }
            ViewBag.Response = addVend;
            ViewData["ServiceName"] = new SelectList(_context.CustomerRequestServices, "Id", "ServiceName",addVendor.ServiceName);
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            return View();
        }

        public IActionResult Success()
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            return View();
        }

        public IActionResult CreateProduct()
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            ViewData["ProductVendorList"] = new SelectList(_context.ProductVendors, "Id", "VendorName");
            ViewData["ProductCategory"] = new SelectList(_context.CustomerRequestServices, "Id", "ServiceName");
            return View();
        }


        public IActionResult ProductList()
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            //var category = _context.CustomerRequestServices.Where(e => e.Id == ).Select(s => s.ServiceName).FirstOrDefault();
            var products = _context.Products.Select(d=> new ProductListDtos { 
                DateCreated = d.DateCreated.Value,
                ProductCategory = _context.CustomerRequestServices.Where(e=>e.Id == Convert.ToInt32(d.ProductCategory)).Select(s=>s.ServiceName).FirstOrDefault(),
                VendorName = _context.ProductVendors.Where(e => e.Id == Convert.ToInt32(d.VendorName)).Select(s => s.VendorName).FirstOrDefault(),
                ProductImage= d.ProductImage,
                ProductName = d.ProductName,
                ProductPrice = d.ProductPrice,
                Quantity = d.Quantity,
                Id = d.Id
            }).ToList();
            return View(products);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductDto productDto)
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var models = new Product
            {
                CreatedBy = _http.HttpContext.Session.GetString("userName"),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                ProductCategory = productDto.ProductCategory,
                ProductImage = UploadImage(productDto),
                ProductName = productDto.ProductName,
                ProductPrice = productDto.ProductPrice,
                VendorName = productDto.VendorName,
                Quantity = productDto.Quantity
            };
            _context.Products.Add(models);
            _context.SaveChanges();
            ViewData["ProductVendorList"] = new SelectList(_context.ProductVendors, "Id", "VendorName", productDto.VendorName);
            ViewData["ProductCategory"] = new SelectList(_context.CustomerRequestServices, "Id", "ServiceName", productDto.ProductCategory);
            return RedirectToAction("ProductList");
        }

        public string UploadImage(ProductDto createVendor)
        {

            string uniqueFileName = null;

            if (createVendor.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(_hosting.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + createVendor.ProductImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    createVendor.ProductImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;

        }


        [HttpGet]

        public IActionResult GetAllCustomers()
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();

            var allCustomer = _context.Customers.ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            return View(allCustomer);
        }

        public ActionResult VendorRequestHistory(int? id)
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();

            var allCustomer = _context.Customers.ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            var history = (from hist in _context.ResquServices
                           where hist.VendorId == id.ToString()
                           join trans in _context.Transactions 
                           on hist.BookingId equals trans.BookingId
                           select new HistoryDto
                           {
                               Amount = trans.TotalAmount,
                               CustomerName = trans.CustomerName,
                               StartDate = hist.DateStarted.ToString("dd-MM-yyyy hh:mm tt"),
                               EndDate = hist.DateEnded.ToString("dd-MM-yyyy hh:mm tt"),
                               Location = hist.CustomerLocation,
                               ServiceName = hist.ServiceName,
                               SubCategory = hist.SubCategoryName
                           }).ToList();

            ViewBag.Counter = history.Count();
            return View(history);
        }

        public ActionResult WalletPage()
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();

            var allCustomer = _context.Customers.ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            ViewBag.AllTransaction = _context.Transactions.ToList();
            ViewBag.DebitTransaction = _context.Transactions.Where(r=>r.CustomerTransactionType == "DR").ToList();
            ViewBag.CreditTransaction = _context.Transactions.Where(r => r.CustomerTransactionType == "CR").ToList();
            ViewBag.OverallWalletBalance = _context.Wallets.Select(d => d.Balance).ToList().Sum();
            return View();
        }
        public ActionResult TransactionList()
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();

            var allCustomer = _context.Customers.ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();

            var transactions = _context.Transactions.ToList();
            ViewBag.Transactions = transactions.Where(c => c.Status == "Completed").Count();
            return View(transactions);
        }

        public ActionResult BackOfficeDashboard()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.ServiceCategory = _context.CustomerRequestServices.ToList().Count();
            ViewBag.ServiceSubCategory = _context.VendorProcessServiceTypes.ToList().Count();
            ViewBag.Vendors = _context.Vendors.Where(d => d.IsBan == false && d.IsDeleted == false).ToList().Count();
            ViewBag.Customers = _context.Customers.Where(e => e.IsBan == false && e.IsDeleted == false).ToList().Count();
            List<Resqu.Core.Dto.Vendor> vendors = new List<Resqu.Core.Dto.Vendor>();
            var getVendors = _context.Vendors.ToList();

            var getListOfTransactions = _context.Transactions.ToList();
            var tranDetails = new TransactionViewModel
            {

            };

            foreach (var vendorr in getVendors)
            {

                tranDetails.VendorId = vendorr.Id;
                tranDetails.VendorName = vendorr.CompanyName;
                tranDetails.TransactionCount = _context.Transactions.Where(d => d.VendorId == vendorr.Id).ToList().Count();
                tranDetails.TransactionDetails = _context.Transactions.Where(d => d.VendorId == vendorr.Id).Select(y => new TransactionDetail
                {
                    TotalAmount = y.TotalAmount,
                    VendorAmount = y.VendorAmount,
                    CustomerName = y.CustomerName,
                    PaymentType = y.PaymentType,
                    PhoneNumber = y.PhoneNumber,
                    PlatformCharge = y.PlatformCharge,
                    ServiceDate = y.ServiceDate,
                    ServiceType = y.ServiceType,
                    SubCategory = y.SubCategory,
                    VendorName = y.VendorName,
                    TransactionType = y.VendorTransactionType,
                    Status = y.Status,
                    TransactionRef = y.TransactionRef,
                }).ToList();
            }



            var availableServices = new List<AvailableServiceDetailViewModel>();
            var getAllTransactions = _context.Transactions.ToList();
            //var getAllServices = _context.Expertises.ToList();
            //var 
            var getAllExpertise = _context.CustomerRequestServices.ToList();

            foreach (var expertise in getAllExpertise)
            {
                var getSubCategory = _context.ServiceToSericeCategorys.Where(c => c.ServiceId == expertise.Id).Select(e=>e.ServiceTypeId).ToList().Count();
                var getAllVendors = _context.Vendors.Where(c => c.CustomerRequestServiceId == expertise.Id).ToList().Count();
                var getNoOfUsers = _context.Transactions.Where(c => c.ServiceType == expertise.ServiceName).ToList().Count();
                var description = _context.CustomerRequestServices.Where(e => e.Id == expertise.Id).Select(d => d.Description).FirstOrDefault();
                var expertiser = new AvailableServiceDetailViewModel
                {
                    Description = description,
                    NumberOfUsers = getNoOfUsers,
                    NumberOfVendors = getAllVendors,
                    ServiceType = expertise.ServiceName,
                    SubCategory = getSubCategory
                };
                availableServices.Add(expertiser);
            }
            ViewBag.ProfileImage = _context.BackOfficeUsers.Where(x => x.UserName == HttpContext.Session.GetString("userName")).Select(d => d.ProfilePicture).FirstOrDefault();
            //foreach (var transact in getAllTransactions)
            //{
            //    foreach (var expert in getAllExpertise)
            //    {
            //        if (transact.ServiceType == expert.Name)
            //        {
            //            var getExpertIdByName = _context.Expertises.Where(w => w.Name == transact.ServiceType).Select(g => g.Id).SingleOrDefault();
            //            var getSubCategory = _context.ExpertiseCategories.Where(s => s.ExpertiseId == getExpertIdByName).ToList().Count();
            //            var service = new AvailableServiceDetailViewModel
            //            {
            //                SubCategory = getSubCategory,
            //                Description = expert.Description,
            //                NumberOfUsers = _context.Transactions.Where(e => e.CustomerName == transact.CustomerName).Distinct().Count(),
            //                NumberOfVendors = _context.Vendors.Where(c => c.ExpertiseId == expert.Id).ToList().Count(),
            //                ServiceType = expert.Name
            //            };

            //            availableServices.Add(service);
            //        }

            //    }
            //}
            var Services = availableServices.Select(s => new AvailableServiceDetailViewModel
            {
                Description = s.Description,
                NumberOfUsers = s.NumberOfUsers,
                NumberOfVendors = s.NumberOfVendors,
                ServiceType = s.ServiceType,
                SubCategory = s.SubCategory
            }).ToList();
            HashSet<AvailableServiceDetailViewModel> availableServiceDetails = new HashSet<AvailableServiceDetailViewModel>();

            foreach (var available in Services)
            {
                availableServiceDetails.Add(available);
            }
            ViewBag.AvailableService = availableServiceDetails.ToList();
            ViewBag.UserName = HttpContext.Session.GetString("userName");
            ViewBag.FirstName = HttpContext.Session.GetString("firstName");
            ViewBag.LastName = HttpContext.Session.GetString("lastName");
            ViewBag.Role = HttpContext.Session.GetString("role");
            ViewBag.Phone = HttpContext.Session.GetString("phone");
            ViewBag.Email = HttpContext.Session.GetString("email");
            
            var vendorList = _context.Vendors.ToList();
            var topVendorList = new List<TopVendor>();
            foreach (var vendo in vendorList)
            {
                var getTotalRating = _context.VendorRatings.Where(e => e.VendorId == vendo.Id).Select(d => d.Rating).ToList().Sum();

                var topVendor = new TopVendor
                {
                    Picture = vendo.VendorPicture,
                    NumberOfRequest = _context.Requests.Where(c => c.VendorId == vendo.Id).ToList().Count(),
                    VendorName = vendo.CompanyName,
                    RatingTotal = getTotalRating
                };
                topVendorList.Add(topVendor);
            }


            var reviewList = new List<Review>();

            foreach (var vendo in vendorList)
            {
                var serviceName = _context.Expertises.Where(s => s.Id == vendo.Id).Select(u => u.Name).FirstOrDefault();
                int nullrating = 0;
                int Fullrating = 0;
                var rating = _context.VendorRatings.Where(e => e.VendorId == vendo.Id && e.ServiceType == serviceName).Select(s => new Ratings {TotalRating = s.Rating, LastCreatedAt = _context.VendorRatings.Where(r=>r.VendorId == vendo.Id).Select(e=>e.CreatedAt).Max()}).FirstOrDefault();
                if (rating != null)
                {
                    if (rating == null)
                    {
                        rating.TotalRating = 0;
                        nullrating = 0;
                    }
                    if (rating.TotalRating == 0)
                    {
                        nullrating = 0;
                    }
                    else if (rating.TotalRating > 0)
                    {
                        Fullrating = _context.VendorRatings.Where(e => e.VendorId == vendo.Id && e.ServiceType == serviceName).Select(s => s.Rating).FirstOrDefault();
                    }
                    rating.TotalRating = rating.TotalRating == nullrating ? nullrating : Fullrating;
                    var completedRequests = _context.Requests.Where(c => c.VendorId.Value == vendo.Id && c.RequestStatus == "Completed").ToList().Count();
                    var dates = rating.LastCreatedAt;
                    var minusDate = DateTime.Now - dates;
                    var ratings = new Review
                    {
                        Picture = vendo.VendorPicture,
                        Rating = rating.TotalRating,
                        SubCategory = vendo.CustomerRequestService.ServiceName,
                        Description = vendo.CustomerRequestService.Description,
                        VendorName = vendo.CompanyName,
                        CompletedRequest = completedRequests,
                        DaysAgo = minusDate.Days
                    };
                    reviewList.Add(ratings);
                }
            }
                
            var vendorLists = topVendorList.OrderByDescending(e => e.NumberOfRequest).Distinct();
            ViewBag.TopVendors = vendorLists.OrderByDescending(e=>e.RatingTotal).Take(10);
            ViewBag.Reviews = reviewList.OrderByDescending(e => e.Rating).Take(10);
            //ViewBag.Reviews
            //{
            //    new AvailableServiceDetailViewModel
            //    {

            //    }
            //}
            string roleId = HttpContext.Session.GetString("roleId");
            ViewBag.PageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();

            ViewBag.TodaysDate = DateTime.Now.ToString("dd MMM yyyy");
            ViewBag.TotalAmountEarned = _context.Transactions.Select(c => c.PlatformCharge).ToList().Sum();
            //var getTransactions = _context.Transactions.Where()
            return View();

        }
        [HttpGet]
        public ActionResult VendorDetails(int id)
        {
            var vemdor = _context.Vendors.Find(id);
            ViewBag.Expertise = _context.CustomerRequestServices.Where(c => c.Id == vemdor.CustomerRequestServiceId).Select(e => e.ServiceName).FirstOrDefault();
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            var vendorList = _context.Vendors.Where(e=>e.IsDeleted == false).ToList();
            var getTotalVendorRating = _context.VendorRatings.Where(e => e.VendorId == id).Select(d => d.Rating).ToList().Sum();
            ViewBag.Rating = getTotalVendorRating;

            var topVendorList = new List<TopVendor>();

            foreach (var vendo in vendorList)
            {
                var getTotalRating = _context.VendorRatings.Where(e => e.VendorId == vendo.Id).Select(d => d.Rating).ToList().Sum();

                    var topVendor = new TopVendor
                {
                    Picture = vendo.VendorPicture,
                    NumberOfRequest = _context.Requests.Where(c => c.VendorId == vendo.Id).ToList().Count(),
                    VendorName = vendo.CompanyName,
                    RatingTotal = getTotalRating
                };
                topVendorList.Add(topVendor);
            }
            var reviewList = new List<Review>();

            foreach (var vendo in vendorList)
            {
                var serviceName = _context.Expertises.Where(s => s.Id == vendo.Id).Select(u => u.Name).FirstOrDefault();
                int nullrating = 0;
                int Fullrating = 0;
                var rating = _context.VendorRatings.Where(e => e.VendorId == vendo.Id && e.ServiceType == serviceName).Select(s => new Ratings { TotalRating = s.Rating, LastCreatedAt = _context.VendorRatings.Where(r => r.VendorId == vendo.Id).Select(e => e.CreatedAt).Max() }).FirstOrDefault();
                if (rating != null)
                {
                    if (rating == null)
                    {
                        rating.TotalRating = 0;
                        nullrating = 0;
                    }
                    if (rating.TotalRating == 0)
                    {
                        nullrating = 0;
                    }
                    else if (rating.TotalRating > 0)
                    {
                        Fullrating = _context.VendorRatings.Where(e => e.VendorId == vendo.Id && e.ServiceType == serviceName).Select(s => s.Rating).FirstOrDefault();
                    }
                    rating.TotalRating = rating.TotalRating == nullrating ? nullrating : Fullrating;
                    var completedRequests = _context.Requests.Where(c => c.VendorId.Value == vendo.Id && c.RequestStatus == "Completed").ToList().Count();
                    var dates = rating.LastCreatedAt;
                    var minusDate = DateTime.Now - dates;
                    var ratings = new Review
                    {
                        Picture = vendo.VendorPicture,
                        Rating = rating.TotalRating,
                        SubCategory = _context.Vendors.Where(v=>v.Id == vendo.Id).Select(s=>s.CustomerRequestService.ServiceName).FirstOrDefault(),
                        Description = _context.Vendors.Where(v => v.Id == vendo.Id).Select(s => s.CustomerRequestService.Description).FirstOrDefault(),
                        VendorName = vendo.CompanyName,
                        CompletedRequest = completedRequests,
                        DaysAgo = minusDate.Days
                    };
                    reviewList.Add(ratings);
                }
            }

            var vendorLists = topVendorList.OrderByDescending(e => e.NumberOfRequest).Distinct();
            ViewBag.TopVendors = vendorLists.OrderByDescending(e => e.RatingTotal).Take(10);
            ViewBag.Reviews = reviewList.OrderByDescending(e => e.Rating).Take(10);
            ViewBag.BanStatus = _context.Vendors.Where(c => c.Id == id).Select(e => e.IsBan).FirstOrDefault();
            ViewBag.Completed = _context.Transactions.Where(c => c.VendorId == id && c.Status == "Completed").Count();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            return View(vemdor);
        }
        public ActionResult DeleteVendor(int id)
        {
            var delete = _context.Vendors.Find(id);
            if (delete == null)
            {
                ViewBag.CannotDelete = "This Vendor does not exist";
                return View();
            }
            if (delete.IsDeleted == true)
            {
                ViewBag.AlreadyDeleted = "This Vendor has already been deleted";
                return View();
            }
            if (delete.IsDeleted == false)
            {
                delete.IsDeleted = true;
                _context.SaveChanges();
                return RedirectToAction("Vendors");
            }
            return View();
        }


        public ActionResult UnBanVendor(int id)
        {
            var delete = _context.Vendors.Find(id);
            if (delete == null)
            {
                ViewBag.CannotUnBan = "This Vendor does not exist";
                return View();
            }
            if (delete.IsBan == false)
            {
                ViewBag.AlreadyUnBanned = "This Vendor has already been Unbanned";
                return View();
            }
            if (delete.IsBan == true)
            {
                delete.IsBan = false;
                _context.SaveChanges();
                return RedirectToAction("Vendors");
            }
            return View();
        }
        
        public ActionResult UpdateVendorProfile(int id)
        {
            var getVendor = _context.Vendors.Find(id);
            ViewData["ServiceName"] = new SelectList(_context.Expertises, "Id", "Name");
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            return View(getVendor);
        }

        [HttpPost]
        public ActionResult UpdateVendorProfile(int id,Resqu.Core.Dto.Vendor vendor)
        { 
            try
            {
                var update = _back.UpdateVendorProfile(id, vendor).Result;
                if (update.Status == true)
                {
                    return RedirectToAction("Vendors");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            
        }

        [HttpGet]
        public ActionResult BanVendor(int id)
        {
            var delete = _context.Vendors.Find(id);
            if (delete == null)
            {
                ViewBag.CannotBan = "This Vendor does not exist";
                return View();
            }
            if (delete.IsBan == true)
            {
                ViewBag.AlreadyBanned = "This Vendor has already been banned";
                return View();
            }
            if (delete.IsBan == false)
            {
                delete.IsBan = true;
                _context.SaveChanges();
                return RedirectToAction("Vendors");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Vendors()
        {
            var getAllVendors = _context.Vendors.Where(c=>c.IsDeleted == false).Select(d => new VendorsViewModel
            {
                Id = d.Id,
                EmailAddress = d.EmailAddress,
                Gender = d.Gender,
                PhoneNumber = d.PhoneNo,
                ServiceCategory = _context.CustomerRequestServices.Where(e=>e.Id == d.CustomerRequestServiceId).Select(s=>s.ServiceName).FirstOrDefault(),
                VendorName = d.FirstName + " "+ d.LastName,
                CompletedRequest = _context.Transactions.Where(c=>c.VendorId == d.Id && c.Status == "Completed").Count().ToString()
            }).ToList();
            if (getAllVendors.Count == 0)
            {
                return RedirectToAction("EmptyVendor");
            }
            ViewBag.Vendors = getAllVendors;
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName +" "+ e.LastName).FirstOrDefault();
            return View();
        }


        [HttpGet]
        public ActionResult VendorsHistory()
        {
            var getAllVendors = _context.Vendors.Where(c => c.IsDeleted == false).Select(d => new VendorsViewModel
            {
                Id = d.Id,
                EmailAddress = d.EmailAddress,
                Gender = d.Gender,
                PhoneNumber = d.PhoneNo,
                ServiceCategory = _context.CustomerRequestServices.Where(e => e.Id == d.CustomerRequestServiceId).Select(s => s.ServiceName).FirstOrDefault(),
                VendorName = d.FirstName + " " + d.LastName,
                CompletedRequest = _context.Transactions.Where(c => c.VendorId == d.Id && c.Status == "Completed").Count().ToString()
            }).ToList();
            if (getAllVendors.Count == 0)
            {
                return RedirectToAction("EmptyVendor");
            }
            ViewBag.Vendors = getAllVendors;
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            return View();
        }


        
        public PartialViewResult ServiceDetails(int serviceId)
        {
            

            var experts = _context.VendorProcessServices.Where(e => e.CustomerRequestServiceId == serviceId).Select(u => new SubCategoryList
            {
                SubCategory = _context.VendorProcessServiceTypes.Where(w=>w.Id == u.VendorProcessServiceTypeId).Select(r=>r.ServiceTypeName).FirstOrDefault(),
                Price = _context.VendorProcessServiceTypes.Where(w => w.Id == u.VendorProcessServiceTypeId).Select(r => r.Cost).FirstOrDefault().ToString()
            }).ToList();


            ViewBag.SubCategoryList = experts;
            
            var serviceList = new HashSet<Service>();
            var services = _context.Expertises.ToList();
            int subCategories = 0;
            foreach (var service in services)
            {
               // subCategories = _context.CustomerRequestServices.Where(e => e.Id == service.Id).Select().ToList().Count();
                var serv = new Service
                {
                    ServiceId = service.Id,
                    ServiceType = service.Name,

                    SubCategory = _context.ServiceToSericeCategorys.Where(e=>e.ServiceId == service.Id).ToList().Count()
                };
                serviceList.Add(serv);
            }
            var getDetails = _context.CustomerRequestServices.Where(e => e.Id == Convert.ToInt32(serviceId)).Select(s => new ServiceDetail
            {
                Id = s.Id,
                CreatedBy = _http.HttpContext.Session.GetString("userName"),
                DateCreated = _context.Products.Where(r => r.Id == s.Id).Select(e => e.DateCreated.Value.ToString("yyyy-MM-dd hh:mm tt")).FirstOrDefault(),
                Description = _context.VendorProcessServices.Where(r=>r.CustomerRequestServiceId == s.Id).Select(e=>e.Description).FirstOrDefault(),
                ServiceCategory = s.ServiceName,
                SubCategories = experts.Count(),
                SubCategoryList = experts
            }).FirstOrDefault();
            //return getDetails;
            return PartialView(getDetails);
        }


        [HttpGet]
        public IActionResult EmptyVendor()
        {
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            return View();
        }
        public ActionResult ServiceList()
        {

            var getAllServices = _context.CustomerRequestServices.ToList();
            var serviceList = new HashSet<Service>();
            foreach (var server in getAllServices)
            {
                var subCategories = _context.ServiceToSericeCategorys.Where(d => d.ServiceId == server.Id).Select(e=>e.ServiceTypeId).ToList().Count();
                var serv = new Service
                {
                    ServiceId = server.Id,
                    ServiceType = server.ServiceName,
                    SubCategory = subCategories
                };
                serviceList.Add(serv);
            }
            //var serviceList = new HashSet<Service>();
            //var services = _context.Expertises.ToList();
            //foreach (var service in services)
            //{
            //    var subCategories = _context.Expertises.Where(e => e.Name == service.Name).Select(s => s.VendorSpecializationId).ToList().Count();
            //    var serv = new Service
            //    {
            //        ServiceId = service.Id,
            //        ServiceType = service.Name,
            //        SubCategory = subCategories
            //    };
            //    serviceList.Add(serv);
            //}
            ViewBag.ServiceCount = serviceList.Count();
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            return View(serviceList);
        }

        public ActionResult Services()
        {
            var serviceList = new HashSet<Service>();
            
            var services = _context.Expertises.ToList();
            foreach (var service in services)
            {
              
                var subCategories = _context.Expertises.Where(e => e.Name == service.Name).Select(s=>s.ExpertiseCategoryId).ToList().Count();

                var serv = new Service
                {
                    ServiceId =service.Id,
                    ServiceType = service.Name,
                    SubCategory = subCategories
                };

                serviceList.Add(serv);
            }

            ViewBag.Services = serviceList.ToList();

            ViewBag.ServiceCount = serviceList.Count();
            if (serviceList.Count == 0)
            {
                ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
                {
                    PageName = p.PageName,
                    PageUrl = p.PageUrl,
                    PageNameClass = p.PageNameClass,
                    PageUrlClass = p.PageUrlClass,
                    ActionName = p.ActionName,
                    ControllerName = p.ControllerName
                }).ToList();
                ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
                ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();

                return RedirectToAction("EmptyService");
            }
            ViewBag.ServicePageUrls = _context.BackOfficeRoles.Where(b => b.RoleName == HttpContext.Session.GetString("role")).Select(p => new Resqu.Core.Entities.RoleUrl
            {
                PageName = p.PageName,
                PageUrl = p.PageUrl,
                PageNameClass = p.PageNameClass,
                PageUrlClass = p.PageUrlClass,
                ActionName = p.ActionName,
                ControllerName = p.ControllerName
            }).ToList();
            ViewBag.ProfilePicture = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.ProfilePicture).FirstOrDefault();
            ViewBag.FullName = _context.BackOfficeUsers.Where(d => d.UserName == HttpContext.Session.GetString("userName")).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
            return View();
        }



            // GET: BackOffice/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }


        public async Task<IActionResult> EditService(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Expertises.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: BackOffice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(long id, [Bind("VendorAmount,PlatformCharge,TotalAmount,PhoneNumber,Status,TransactionRef,ServiceType,TransactionType,SubCategory,ServiceDate,CustomerName,VendorName,PaymentType,Id")] Expertise expertise)
        {
            if (id != expertise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expertise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpertiseExists(expertise.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expertise);
        }

        // GET: BackOffice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorAmount,PlatformCharge,TotalAmount,PhoneNumber,Status,TransactionRef,ServiceType,TransactionType,SubCategory,ServiceDate,CustomerName,VendorName,PaymentType,Id")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: BackOffice/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: BackOffice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("VendorAmount,PlatformCharge,TotalAmount,PhoneNumber,Status,TransactionRef,ServiceType,TransactionType,SubCategory,ServiceDate,CustomerName,VendorName,PaymentType,Id")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: BackOffice/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: BackOffice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(long id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }

        private bool ExpertiseExists(long id)
        {
            return _context.Expertises.Any(e => e.Id == id);
        }
    }
}
