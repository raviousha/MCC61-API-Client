using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_m_Educations")]
    public class Education
    {
        [Key]
        public String Id { get; set; }

        [Required]
        public String degree { get; set; }

        [Required]
        [Range (0,4, ErrorMessage = "The property {0} should have value {0} minimum and {2} maximum")]
        public float GPA { get; set; }
        [JsonIgnore]
        public virtual ICollection<Profiling> Profiling { get; set; }

        [Required]
        public String UniversityId { get; set; }
        public virtual University University { get; set; }
    }
}
