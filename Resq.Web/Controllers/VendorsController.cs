using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resq.Web.Interface;
using Resq.Web.ViewModels;
using Resqu.Core.Entities;

namespace Resq.Web.Controllers
{
    public class VendorsController : Controller
    {
        private readonly ResquContext _context;
        private readonly IVendor _vendor;

        public VendorsController(ResquContext context, IVendor vendor)
        {
            _context = context;
            _vendor = vendor;
        }

        // GET: Vendors
        public async Task<IActionResult> Index()
        {
            var resquContext = _context.Vendors.Include(v => v.Expertise);
            return View(await resquContext.ToListAsync());
        }

        // GET: Vendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .Include(v => v.Expertise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // GET: Vendors/Create
        public IActionResult Create()
        {
            ViewData["ExpertiseId"] = new SelectList(_context.Expertises, "Id", "Name");
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,FirstName,MiddleName,LastName,VendorPicture,CompanyName,PhoneNo,EmailAddress,IdentificationNumber,MeansOfIdentification,ContactAddress,NextOfKinRelationship,NextOfKinName,NextOfKinAddress,NextOfKinPhone,VendorCode,Pin,ExpertiseId,Id,DateCreated,DateModified,IsDeleted,isVerified,CreatedBy,IsBan,IsFullyVerified")] CreateVendorViewModel vendorCreate)
        {
            if (ModelState.IsValid)
            {
                var result =  _vendor.CreateVendor(vendorCreate);
                if (result == "Successfully Saved")
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
                
            }
            ViewData["ExpertiseId"] = new SelectList(_context.Expertises, "Id", "Name", vendorCreate.ExpertiseId);
            return View(vendorCreate);
        }   

        // GET: Vendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            ViewData["ExpertiseId"] = new SelectList(_context.Expertises, "Id", "Id", vendor.ExpertiseId);
            return View(vendor);
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhoneNumber,FirstName,MiddleName,LastName,CompanyName,PhoneNo,EmailAddress,IdentificationNumber,MeansOfIdentification,ContactAddress,NextOfKinRelationship,NextOfKinName,NextOfKinAddress,NextOfKinPhone,VendorCode,Pin,ExpertiseId,Id,DateCreated,DateModified,IsDeleted,isVerified,CreatedBy,IsBan,IsFullyVerified")] Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.Id))
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
            ViewData["ExpertiseId"] = new SelectList(_context.Expertises, "Id", "Id", vendor.ExpertiseId);
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .Include(v => v.Expertise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
}
