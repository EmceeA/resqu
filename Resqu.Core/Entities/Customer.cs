using System;

namespace Resqu.Core.Entities
{
    public class Customer : CustomerAudit
    {
        

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccountId { get; set; }
        public string RegulatoryIndentity { get; set; }
        public string Pin { get; set; }
        public bool IsCustomerCreated  { get; set; }
        public bool IsDedicatedCreated  { get; set; }

        public DateTime LastLoginDate { get; set; }
        public DateTime LastServiceDate { get; set; }


    }
}
