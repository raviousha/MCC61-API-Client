using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class ChangePassVM
    {
        [Required(ErrorMessage = "Email cannot be empty")]
        [EmailAddress(ErrorMessage = "Enter valid Email address")]
        public string email { get; set; }
        public int OTP { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
