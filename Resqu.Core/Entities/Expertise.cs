namespace Resqu.Core.Entities
{
    public class Expertise :  ExpertiseAudit
    {
        
        public string  Name { get; set; }

        public int?  ExpertiseCategoryId { get; set; }
        public ExpertiseCategory GetExpertiseCategory { get; set; }
    }
}
