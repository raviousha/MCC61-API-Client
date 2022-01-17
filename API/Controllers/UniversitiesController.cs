﻿using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UniversitiesController : BaseController<University, UniversityRepository, string>
    {
        public UniversitiesController(UniversityRepository universityRepository) : base(universityRepository)
        {

        }
    }
}
