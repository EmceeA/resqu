using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resq.Web.ViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Role Name is required")]
        public string RoleName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
