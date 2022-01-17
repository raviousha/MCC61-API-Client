using API.Models;
using API.Repository;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    //public class EmployeesController : ControllerBase
    //{
    //    private readonly EmployeeRepository employeeRepository;

    //    public EmployeesController(EmployeeRepository employeeRepository)
    //    {
    //        this.employeeRepository = employeeRepository;
    //    }

    //    [HttpGet]
    //    public ActionResult Get()
    //    {
    //        try
    //        {
    //            var code = 0;
    //            var message = "";
    //            var result = Ok(employeeRepository.Get());
    //            var count = employeeRepository.Get().Count();

    //            if (count > 0)
    //            {
    //                code = StatusCodes.Status200OK;
    //                //message = "";
    //            }
    //            else
    //            {
    //                //code = Response.StatusCode;
    //                code = StatusCodes.Status400BadRequest;
    //                message = "Table content is empty";
    //            }
    //            return Ok(new { code, result, message });

    //        }
    //        catch (Exception e)
    //        {
    //            return Ok($"{e.Message}");
    //        }
    //    }

    //    [HttpGet("{nik}")]
    //    public ActionResult Get(String nik)
    //    {
    //        try
    //        {
    //            var code = 0;
    //            var message = "";
    //            var result = employeeRepository.Get(nik);

    //            if (result != null)
    //            {
    //                code = StatusCodes.Status200OK;
    //                message = $"Employee with NIK {nik} Found";
    //            }
    //            else
    //            {
    //                //code = Response.StatusCode;
    //                code = StatusCodes.Status404NotFound;
    //                message = $"Employee with NIK {nik} Not Found";
    //            }
    //            return Ok(new { code, result, message });

    //        }
    //        catch (Exception e)
    //        {
    //            return Ok($"{e.Message}");
    //        }
    //    }

    //    [HttpPost]
    //    public ActionResult Post(Employee employee)
    //    {
    //        try
    //        {
    //            var code = 0;
    //            var message = "";
    //            var result = employeeRepository.Insert(employee);

    //            switch (result)
    //            {
    //                case 1:
    //                    {
    //                        code = StatusCodes.Status200OK;
    //                        message = $"Employee data with NIK {employee.NIK} saved";
    //                        break;
    //                    }
    //                case 2:
    //                    {
    //                        code = StatusCodes.Status400BadRequest;
    //                        message = $"NIK {employee.NIK} not found";
    //                        break;
    //                    }
    //                case 3:
    //                    {
    //                        code = StatusCodes.Status400BadRequest;
    //                        message = $"Email {employee.Email} is already exist";
    //                        break;
    //                    }
    //                case 4:
    //                    {
    //                        code = StatusCodes.Status400BadRequest;
    //                        message = $"Phone number {employee.Phone} is already exist";
    //                        break;
    //                    }
    //                default:
    //                    {
    //                        //code = Response.StatusCode;
    //                        code = StatusCodes.Status400BadRequest;
    //                        message = "Employee Update failed";
    //                        break;
    //                    }
    //            }
    //            return Ok(new { code, result, message });
    //        }
    //        catch (Exception e)
    //        {
    //            return Ok(e.Message);
    //        }
    //    }

    //    [HttpDelete("{nik}")]
    //    public ActionResult Delete(String nik)
    //    {
    //        try
    //        {
    //            var code = 0;
    //            var message = "";
    //            var result = employeeRepository.Delete(nik);

    //            if (result == 1)
    //            {
    //                code = StatusCodes.Status200OK;
    //                message = "Employee data deleted";
    //            }
    //            else
    //            {
    //                //code = Response.StatusCode;
    //                code = StatusCodes.Status400BadRequest;
    //                message = $"Employee data with NIK {nik} not found";
    //            }
    //            return Ok(new { code, result, message });
    //        }
    //        catch (Exception e)
    //        {
    //            return Ok(e.Message);
    //        }
    //    }

    //    [HttpPut("{nik}")]
    //    public ActionResult Update(String nik, Employee employee)
    //    {
    //        try
    //        {
    //            var code = 0;
    //            var message = "";
    //            var result = employeeRepository.Update(nik,employee);

    //            switch (result)
    //            {
    //                case 1:
    //                    {
    //                        code = StatusCodes.Status200OK;
    //                        message = $"Employee data with NIK {employee.NIK} updated";
    //                        break;
    //                    }
    //                case 2:
    //                    {
    //                        code = StatusCodes.Status400BadRequest;
    //                        message = $"NIK {employee.NIK} not found";
    //                        break;
    //                    }
    //                case 3:
    //                    {
    //                        code = StatusCodes.Status400BadRequest;
    //                        message = $"Email {employee.Email} is already exist";
    //                        break;
    //                    }
    //                case 4:
    //                    {
    //                        code = StatusCodes.Status400BadRequest;
    //                        message = $"Phone number {employee.Phone} is already exist";
    //                        break;
    //                    }
    //                default:
    //                    {
    //                        //code = Response.StatusCode;
    //                        code = StatusCodes.Status400BadRequest;
    //                        message = "Employee Update failed";
    //                        break;
    //                    }
    //            }
    //            return Ok(new { code, result, message });
    //        }
    //        catch (Exception e)
    //        {
    //            throw e;
    //            //return Ok();
    //        }
    //    }
    //}
    public class EmployeeController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeeController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost("register")]
        public ActionResult Post(RegisterVM registerVM)
        {
            var code = 0;
            var message = "";
            var result = employeeRepository.Register(registerVM);


            switch (result)
            {
                case 1:
                    {
                        code = StatusCodes.Status400BadRequest;
                        message = $"Email {registerVM.email} is already exist";
                        break;

                    }
                case 2:
                    {
                        code = StatusCodes.Status400BadRequest;
                        message = $"Phone number {registerVM.phone} is already exist";
                        break;
                    }
                case 3:
                    {
                        code = StatusCodes.Status400BadRequest;
                        message = $"Employee Input failed";
                        break;
                    }
                case 5:
                    {
                        code = StatusCodes.Status200OK;
                        message = $"Employee data with Name {registerVM.firstName} {registerVM.lastName} saved";
                        break;
                    }
                default:
                    {
                        //code = Response.StatusCode;
                        code = StatusCodes.Status400BadRequest;
                        message = "Employee Input failed";
                        break;
                    }
            }
            return Ok(result);
        }

        //[Authorize(Roles = "director,manager")]
        [HttpGet("show")]
        public ActionResult GetRegisteredData()
        {
            try
            {
                var code = 0;
                var message = "";
                //var result = ();
                var count = employeeRepository.GetRegisteredData().Count();

                if (count > 0)
                {
                    code = StatusCodes.Status200OK;
                    //message = "";
                }
                else
                {
                    //code = Response.StatusCode;
                    code = StatusCodes.Status400BadRequest;
                    message = "Table content is empty";
                }
                return Ok(employeeRepository.GetRegisteredData());

            }
            catch (Exception e)
            {
                return Ok($"{e.Message}");
            }
        }

        [HttpGet("show/{nik}")]
        [EnableCors("AllowOrigin")]
        public ActionResult GetRegisteredData(String nik)
        {
            try
            {
                var code = 0;
                var message = "";
                var result = Ok(employeeRepository.GetRegisteredData(nik));
                var count = employeeRepository.GetRegisteredData(nik).Count();

                if (count > 0)
                {
                    code = StatusCodes.Status200OK;
                    message = $"Employee with NIK {nik} found";
                }
                else
                {
                    //code = Response.StatusCode;
                    code = StatusCodes.Status400BadRequest;
                    message = "Employee with NIK {nik} not found";
                }
                return Ok(new { code, result, message });

            }
            catch (Exception e)
            {
                return Ok($"{e.Message}");
            }
        }

        [HttpGet("gender")]
        public ActionResult GetCountGenderData()
        {
            try
            {
                var code = 0;
                var message = "";
                var result = Ok(employeeRepository.GetCountGenderData());
                var count = employeeRepository.GetCountGenderData().Count();

                if (count > 0)
                {
                    code = StatusCodes.Status200OK;
                    message = "Employee with NIK {nik} found";
                }
                else
                {
                    //code = Response.StatusCode;
                    code = StatusCodes.Status400BadRequest;
                    message = "Employee with NIK {nik} not found";
                }
                return Ok(employeeRepository.GetCountGenderData());

            }
            catch (Exception e)
            {
                return Ok($"{e.Message}");
            }
        }

        [HttpDelete("delete/{nik}")]
        public ActionResult DeleteRegisteredData(String nik)
        {
            try
            {
                var code = 0;
                var message = "";
                var result = employeeRepository.DeleteRegisteredData(nik);

                if (result == 1)
                {
                    code = StatusCodes.Status200OK;
                    message = "Data deleted";
                }
                else
                {
                    //code = Response.StatusCode;
                    code = StatusCodes.Status400BadRequest;
                    message = $"Data with Id {nik} not found";
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("TestCORS")]
        [EnableCors("AllowOrigin")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS berhasil");
        }

        //[HttpDelete("delete/{nik}")]
        //public ActionResult DeleteRegisteredData(String nik)
        //{
        //    try
        //    {
        //        var code = 0;
        //        var message = "";
        //        var result = employeeRepository.Delete(nik);

        //        if (result == 4)
        //        {
        //            code = StatusCodes.Status200OK;
        //            message = "Employee data deleted";
        //        }
        //        else
        //        {
        //            //code = Response.StatusCode;
        //            code = StatusCodes.Status404NotFound;
        //            message = $"Employee data with NIK {nik} not found";
        //        }
        //        return Ok(new { code, result, message });
        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(e.Message);
        //    }
        //}
    }
}
