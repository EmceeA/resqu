using Microsoft.EntityFrameworkCore;

namespace Resqu.Core.Entities
{
    public class ResquContext :DbContext
    {
        public ResquContext(DbContextOptions<ResquContext> options):base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Otp> Otps { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Expertise> Expertises { get; set; }
        public DbSet<ExpertiseCategory> ExpertiseCategories { get; set; }
        public DbSet<WalletPayment> WalletPayments { get; set; }
        public DbSet<CardPayment> CardPayments { get; set; }
        public DbSet<CashPayment> CashPayments { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<ResquProcess> ResquProcesses { get; set; }
    }
}
