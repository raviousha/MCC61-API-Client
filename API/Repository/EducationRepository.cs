using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EducationRepository : GeneralRepository<MyContext, Education, String>
    {
        public EducationRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
