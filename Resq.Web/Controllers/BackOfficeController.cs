using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private IBackOffice _back;
        public BackOfficeController(ResquContext context, IBackOffice back)
        {
            _context = context;
            _back = back;
        }

        // GET: BackOffice
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }
       

        [HttpGet]

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var result = await _back.Login(loginDto);
            if (result.Status == true)
            {
                return RedirectToAction("BackOfficeDashboard");
            }
            return RedirectToAction("Login");
        }


        [HttpGet]

        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddService(ExpertiseDto expertiseDto)
        {
            return View();
        }

        public ActionResult BackOfficeDashboard()
        {
            ViewBag.ServiceCategory = _context.Expertises.ToList().Count();
            ViewBag.ServiceSubCategory = _context.ExpertiseCategories.ToList().Count();
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
                    TransactionType = y.TransactionType,
                    Status = y.Status,
                    TransactionRef = y.TransactionRef,
                }).ToList();
            }



            var availableServices = new List<AvailableServiceDetailViewModel>();
            var getAllTransactions = _context.Transactions.ToList();
            var getAllExpertise = _context.Expertises.ToList();
            foreach (var transact in getAllTransactions)
            {
                foreach (var expert in getAllExpertise)
                {
                    if (transact.ServiceType == expert.Name)
                    {
                        var getExpertIdByName = _context.Expertises.Where(w => w.Name == transact.ServiceType).Select(g => g.Id).SingleOrDefault();
                        var getSubCategory = _context.ExpertiseCategories.Where(s => s.ExpertiseId == getExpertIdByName).ToList().Count();
                        var service = new AvailableServiceDetailViewModel
                        {
                            SubCategory = getSubCategory,
                            Description = expert.Description,
                            NumberOfUsers = _context.Transactions.Where(e => e.CustomerName == transact.CustomerName).Distinct().Count(),
                            NumberOfVendors = _context.Vendors.Where(c => c.ExpertiseId == expert.Id).ToList().Count(),
                            ServiceType = expert.Name
                        };

                        availableServices.Add(service);
                    }

                }
            }
            var  Services = availableServices.Select(s=> new AvailableServiceDetailViewModel { Description =s.Description, NumberOfUsers = s.NumberOfUsers,
            NumberOfVendors = s.NumberOfVendors,
            ServiceType = s.ServiceType, SubCategory = s.SubCategory}).ToList();
            HashSet<AvailableServiceDetailViewModel> availableServiceDetails = new HashSet<AvailableServiceDetailViewModel>();

            foreach (var available in Services)
            {
                availableServiceDetails.Add(available);
            }
            ViewBag.AvailableService = availableServiceDetails.ToList();

            var vendorList = _context.Vendors.ToList();
            var topVendorList = new List<TopVendor>();
            foreach (var vendo in vendorList)
            {
                var topVendor = new TopVendor
                {
                    Picture = vendo.VendorPicture,
                    NumberOfRequest = _context.Transactions.Where(c => c.VendorName == vendo.CompanyName).ToList().Count(),
                    VendorName = vendo.CompanyName
                };
                topVendorList.Add(topVendor);
            }


            var reviewList = new List<Review>();

            foreach (var vendo in vendorList)
            {
                var rating = _context.Transactions.Where(e => e.VendorId == vendo.Id && e.ServiceType == vendo.Expertise.Name).Select(s => s.VendorRating).FirstOrDefault();
                var completedRequests = _context.Requests.Where(c => c.VendorId.Value == vendo.Id && c.RequestStatus == "Completed").ToList().Count();
                var ratings = new Review
                {
                    Picture = vendo.VendorPicture,
                    Rating = rating,
                    SubCategory = vendo.Expertise.Name,
                    Description = vendo.Expertise.Description,
                    VendorName = vendo.CompanyName,
                    CompletedRequest = completedRequests
                };
                reviewList.Add(ratings);
            }

            ViewBag.TopVendors = topVendorList.OrderByDescending(e => e.NumberOfRequest).Take(10);
            ViewBag.Reviews = reviewList.OrderByDescending(e => e.Rating).Take(20);
            //ViewBag.Reviews
            //{
            //    new AvailableServiceDetailViewModel
            //    {

            //    }
            //};

            ViewBag.TodaysDate = DateTime.Now.ToString("dd MMM yyyy");
            ViewBag.TotalAmountEarned = _context.Transactions.Select(c => c.PlatformCharge).ToList().Sum();
            //var getTransactions = _context.Transactions.Where()
            return View();

        }
        public ActionResult Services()
        {
            var serviceList = new List<Service>();
            
            var services = _context.Expertises.ToList();
            foreach (var service in services)
            {
                var subCategories = _context.ExpertiseCategories.Where(e => e.ExpertiseId == service.Id).ToList().Count();

                var serv = new Service
                {
                    ServiceType = service.Name,
                    SubCategory = subCategories
                };

                serviceList.Add(serv);
            }

            ViewBag.Services = serviceList;

            ViewBag.ServiceCount = serviceList.Count();
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
    }
}
