using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    //public class EducationController : ControllerBase
    //{
    //    private readonly EducationsRepository educationRepository;

    //    public EducationController(EducationsRepository educationRepository)
    //    {
    //        this.educationRepository = educationRepository;
    //    }

    //    [HttpGet]
    //    public ActionResult Get()
    //    {
    //        try
    //        {
    //            var code = 0;
    //            var message = "";
    //            var result = Ok(educationRepository.Get());
    //            var count = educationRepository.Get().Count();

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

    //    [Route("Search")]
    //    [HttpGet("{Id}")]
    //    public ActionResult Get(String Id)
    //    {
    //        try
    //        {
    //            var code = 0;
    //            var message = "";
    //            var result = educationRepository.Get(Id);

    //            if (result != null)
    //            {
    //                code = StatusCodes.Status200OK;
    //                message = $"Data with Id {Id} Found";
    //            }
    //            else
    //            {
    //                //code = Response.StatusCode;
    //                code = StatusCodes.Status404NotFound;
    //                message = $"Data with Id {Id} Not Found";
    //            }
    //            return Ok(new { code, result, message });

    //        }
    //        catch (Exception e)
    //        {
    //            return Ok($"{e.Message}");
    //        }
    //    }

    //    [HttpPost]
    //    public ActionResult Post(Education education)
    //    {
    //        try
    //        {
    //            var code = 0;
    //            var message = "";
    //            var result = educationRepository.Insert(education);

    //            if (result == 1)
    //            {
    //                code = StatusCodes.Status200OK;
    //                message = $"Education data with ID {education.Id} saved";
    //            }
    //            else
    //            {
    //                code = StatusCodes.Status400BadRequest;
    //                message = "Employee Update failed";
    //            }
    //            return Ok(new { code, result, message });
    //        }

    //        catch (Exception e)
    //        {
    //            return Ok(e.Message);
    //        }
    //    }

    //    [HttpPut("{Id}")]
    //    public ActionResult Update(String Id, Education education)
    //    {
    //        try
    //        {
    //            var code = 0;
    //            var message = "";
    //            var result = educationRepository.Update(Id, education);

    //            if (result == 1)
    //            {
    //                code = StatusCodes.Status200OK;
    //                message = $"Education data with ID {Id} updated";
    //            }
    //            else
    //            {
    //                code = StatusCodes.Status400BadRequest;
    //                message = "Employee Update failed";
    //            }
    //            return Ok(new { code, result, message });
    //        }

    //        catch (Exception e)
    //        {
    //            throw e;
    //            //return Ok();
    //        }
    //    }

    //    [HttpDelete("{Id}")]
    //    public ActionResult Delete(String Id)
    //    {
    //        try
    //        {
    //            var code = 0;
    //            var message = "";
    //            var result = educationRepository.Delete(Id);

    //            if (result == 1)
    //            {
    //                code = StatusCodes.Status200OK;
    //                message = "Education data deleted";
    //            }
    //            else
    //            {
    //                //code = Response.StatusCode;
    //                code = StatusCodes.Status400BadRequest;
    //                message = $"Education data with Id {Id} not found";
    //            }
    //            return Ok(new { code, result, message });
    //        }
    //        catch (Exception e)
    //        {
    //            return Ok(e.Message);
    //        }
    //    }
    //}

    public class EducationsController : BaseController<Education, EducationRepository, String>
    {
        public EducationsController(EducationRepository educationRepository) : base(educationRepository)
        {

        }
    }
}
