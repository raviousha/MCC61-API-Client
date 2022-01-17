using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_m_Employees")]
    public class Employee
    {
        [Key]
        public String NIK { get; set; }

        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 3,
        ErrorMessage = "The property {0} should have {2} minimum characters and {1} maximum characters")]
        public String firstName { get; set; }

        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 3,
        ErrorMessage = "The property {0} should have {2} minimum characters and {1} maximum characters")]
        public String lastName { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}?-?\d{4}?-?\d{3,5}$")]
        [Phone(ErrorMessage = "Enter valid Phone Number")]
        public String Phone { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime birthDate { get; set; }

        //[Range(1000000, 900000000, 
        //    ErrorMessage = "The property {0} should have value {1} minimum and {2} maximum")]
        public int salary { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Enter valid Email address")]
        public String Email { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "The property {0} should have value {1} for Male and {2} for Female")]
        public Gender Gender { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
}

public enum Gender
{
    Male = 0,
    Female = 1
}
