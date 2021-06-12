using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Dto
{
    public class CustomerSignUpRequestDto
    {
        public string PhoneNumber { get; set; }

        //Put your phone number u get an otp and then put first and last name to set a pin with the Otp
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }

        public string Otp { get; set; }
        public string Pin { get; set; }

    }
}
