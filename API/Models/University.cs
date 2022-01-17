using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_m_Universities")]
    public class University
    {
        [Key]
        public String Id { get; set; }

        [Required]
        public String name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Education> Education { get; set; }
    }
}
