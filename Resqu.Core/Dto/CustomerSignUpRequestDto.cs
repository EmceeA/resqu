using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Dto
{

    public class Ratings
    {
        public int TotalRating { get; set; }
        public DateTime LastCreatedAt { get; set; }
    }

    public class OtpDto
    {
        public string Phone { get; set; }
        public string Otp { get; set; }
    }
    public class CustomerSignUpRequestDto
    {
        [Required(ErrorMessage ="Mobile Number is Required")]
        [RegularExpression(@"^\+\d{1,4}[1-9]\d{0,9}$",ErrorMessage ="Country Code and Mobile Number is Required")]
        [MaxLength(15, ErrorMessage = "Maximum Length is 15")]
        [MinLength(11, ErrorMessage = "Minimum Length is 11")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage ="First Name is Required")]
        [RegularExpression(@"^[a-zA-z]+$",ErrorMessage = "Only alphabets are required")]
        [MaxLength(25, ErrorMessage = "Maximum Length is 25")]
        [MinLength(5, ErrorMessage = "Minimum Length is 5")]
        public string FirstName { get; set; }


        [Required(ErrorMessage ="Email is required")]
        [DataType(DataType.EmailAddress,ErrorMessage ="The Email Format is incorrect")]
        [MaxLength(50, ErrorMessage ="Maximum Length is 50")]
        [MinLength(5, ErrorMessage ="Minimum Length is 5")]
        public string Email { get; set; }



        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression(@"^[a-zA-z]+$", ErrorMessage = "Only alphabets are required")]
        [MaxLength(25, ErrorMessage = "Maximum Length is 25")]
        [MinLength(5, ErrorMessage = "Minimum Length is 5")]
        public string LastName { get; set; }


        [Required(ErrorMessage ="Password is required")]
        [MaxLength,MinLength(4, ErrorMessage = "Minimum and Maximum Length is 4")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
