namespace Resqu.Core.Entities
{
    public class Customer : CustomerAudit
    {
        

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string RegulatoryIndentity { get; set; }
        public string Pin { get; set; }
    }
}
