using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resqu.Core.Entities
{
    public class ExpertiseCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int ExpertiseId { get; set; }
        public int ServiceTypeId { get; set; }
        public int VendorSpecializationId { get; set; }
        public decimal Price { get; set; }
        public Expertise Expertise { get; set; }
    }
}
