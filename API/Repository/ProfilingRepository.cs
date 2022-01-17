﻿using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class ProfilingRepository : GeneralRepository<MyContext, Profiling, String>
    {
        public ProfilingRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
