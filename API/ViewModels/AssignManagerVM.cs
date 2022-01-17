using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class AssignManagerVM
    {
        [Required]
        public string NIK { get; set; }
    }
}
