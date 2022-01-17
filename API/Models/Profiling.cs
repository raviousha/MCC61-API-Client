using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_tr_Profilings")]
    public class Profiling
    {
        [Key]
        public String NIK { get; set; }

        [Required]
        public String EducationId { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        //[JsonIgnore]
        public virtual Education Education { get; set; }
    }
}
