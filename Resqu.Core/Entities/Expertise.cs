﻿using System.Collections.Generic;

namespace Resqu.Core.Entities
{
    public class Expertise :  ExpertiseAudit
    {
        
        public string  Name { get; set; }

        public decimal Cost { get; set; }
        public int?  ExpertiseCategoryId { get; set; }
        public List<ExpertiseCategory> GetExpertiseCategory { get; set; }
    }
}
