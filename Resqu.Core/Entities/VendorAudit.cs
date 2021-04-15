using System;
using System.ComponentModel.DataAnnotations;

namespace Resqu.Core.Entities
{
    public class VendorAudit
    {
        [Key]
        public int Id { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public bool IsDeleted { get; set; }

        public bool isVerified { get; set; }

        public string CreatedBy { get; set; }

        public bool IsBan { get; set; }

        public bool IsFullyVerified { get; set; }
    }
}
