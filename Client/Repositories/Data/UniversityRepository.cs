using API.Models;
using Client.Base.Urls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class UniversityRepository : GeneralRepository<University, string>
    {
        public UniversityRepository(Address address, string request = "Universities/") : base(address, request)
        {

        }
    }
}
