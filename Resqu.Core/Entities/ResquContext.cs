using Microsoft.EntityFrameworkCore;

namespace Resqu.Core.Entities
{
    public class ResquContext :DbContext
    {
        public ResquContext(DbContextOptions<ResquContext> options):base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<ResquService> ResquServices { get; set; }
        public DbSet<ServiceToSericeCategory> ServiceToSericeCategorys { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<VendorProcessService> VendorProcessServices { get; set; }
        public DbSet<CustomerRequestService> CustomerRequestServices { get; set; }
        public DbSet<VendorProcessServiceType> VendorProcessServiceTypes { get; set; }
        public DbSet<VendorSpecialization> VendorSpecializations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BackOfficeRole> BackOfficeRoles { get; set; }
        public DbSet<BackOfficeUser> BackOfficeUsers { get; set; }
        public DbSet<Otp> Otps { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorServiceSubCategory> VendorServiceSubCategories { get; set; }
        public DbSet<Expertise> Expertises { get; set; }
        public DbSet<VendorRating> VendorRatings { get; set; }
        public DbSet<ExpertiseCategory> ExpertiseCategories { get; set; }
        public DbSet<WalletPayment> WalletPayments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVendor> ProductVendors { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<CardPayment> CardPayments { get; set; }
        public DbSet<CashPayment> CashPayments { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<ResquProcess> ResquProcesses { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<VendorAccount> VendorAccounts { get; set; }
    }
}
