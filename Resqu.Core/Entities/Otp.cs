using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resqu.Core.Entities
{
    public class Otp
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string OtpNumber { get; set; }
        public string Status { get; set; }

        public Customer GetCustomer { get; set; }

        public string DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Phone { get; set; }
    }
}
