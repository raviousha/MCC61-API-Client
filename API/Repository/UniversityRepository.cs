using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class UniversityRepository : GeneralRepository<MyContext, University, String>
    {
        public UniversityRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
