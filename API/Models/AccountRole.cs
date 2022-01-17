using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_tr_AccountRoles")]
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public virtual Account Accounts { get; set; }
        public String AccountsNIK { get; set; }
        public virtual Role Roles { get; set; }
        public int RolesId { get; set; }

    }
}
