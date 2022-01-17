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
    [Table("Tb_m_Accounts")]
    public class Account
    {
        [Key]
        public String NIK { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String password { get; set; }

        public System.Nullable<int> OTP { get; set; }

        public System.Nullable<DateTime> expiredToken { get; set; }

        public System.Nullable<bool> isUsed { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        //[JsonIgnore]
        public virtual Profiling Profiling { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
