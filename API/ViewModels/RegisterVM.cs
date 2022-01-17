using API.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class RegisterVM
    {

        //firstname,lastname, phonenumber, birthdate, salary, email, password, degree, gpa dan universityid

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public DateTime birthDate { get; set; }
        public int salary { get; set; }
        public string email { get; set; }

        [Range(0, 1, ErrorMessage = "The property {0} should have value {1} for Male and {2} for Female")]
        public int gender { get; set; }

        public string password { get; set; }

        public string degree { get; set; }
        public float gpa { get; set; }
        public string universityId { get; set; }
    }
}
