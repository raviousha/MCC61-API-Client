using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetRegisteredData()
        {
            var result = await employeeRepository.GetRegisteredData();
            return Json(result);
        }

        [HttpPost]
        public JsonResult Register(RegisterVM registerVM)
        {
            var result = employeeRepository.Register(registerVM);
            return Json(result);
        }

        [HttpDelete]
        public JsonResult DeleteRegister(string nik)
        {
            var result = employeeRepository.DeleteRegisteredData(nik);
            return Json(result);
        }
    }
}
