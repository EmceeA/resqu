using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Entities
{
    public class BackOfficeUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfilePicture { get; set; }
        public long RoleId { get; set; }

        public BackOfficeRole BackOfficeRole { get; set; }


    }

    public class RoleUrl
    {
        public string PageName { get; set; }
        public string PageNameClass { get; set; }
        public string PageUrl { get; set; }
        public string PageUrlClass { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

    }

    public class BackOfficeRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        public string RoleName { get; set; }

        public long? RoleId { get; set; }


        public string PageName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string PageNameClass { get; set; }
        public string PageUrl { get; set; }
        public string PageUrlClass { get; set; }


    }

    public class RoleToPage
    {
        public string RoleName { get; set; }
        public long? RoleId { get; set; } 
        public string PageName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string PageNameClass { get; set; }
        public string PageUrl { get; set; }
        public string PageUrlClass { get; set; }
    }

    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long Id { get; set; }

        public string RoleName { get; set; }
        public bool  IsDeleted { get; set; }
    }
}
