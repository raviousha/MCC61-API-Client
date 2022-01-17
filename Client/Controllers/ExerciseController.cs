using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }
        
        public IActionResult ex1()
        {
            return View();
        }
        
        public IActionResult ex2()
        {
            return View();
        }

        public IActionResult ex3()
        {
            return View();
        }

        public IActionResult ex4()
        {
            return View();
        }

        public IActionResult ex5()
        {
            return View();
        }
    }
}
