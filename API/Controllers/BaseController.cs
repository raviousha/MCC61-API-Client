using API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            try
            {
                var code = 0;
                var message = "";
                //var result = ();
                var count = repository.Get().Count();

                if (count > 0)
                {
                    code = StatusCodes.Status200OK;
                    message = "";
                }
                else
                {
                    //code = Response.StatusCode;
                    code = StatusCodes.Status400BadRequest;
                    message = "Table content is empty";
                }
                return Ok(repository.Get());

            }
            catch (Exception e)
            {
                return Ok($"{e.Message}");
            }
        }

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            try
            {
                var code = 0;
                var message = "";
                var result = repository.Get(key);

                if (result != null)
                {
                    code = StatusCodes.Status200OK;
                    message = $"Data with Id {key} Found";
                }
                else
                {
                    //code = Response.StatusCode;
                    code = StatusCodes.Status404NotFound;
                    message = $"Data with Id {key} Not Found";
                }
                return Ok(repository.Get(key));

            }
            catch (Exception e)
            {
                return Ok($"{e.Message}");
            }
        }

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            try
            {
                var code = 0;
                var message = "";
                var result = repository.Insert(entity);
                if (result == 1)
                {
                    code = StatusCodes.Status200OK;
                    message = $"Data saved";
                }
                else
                {
                    code = StatusCodes.Status400BadRequest;
                    message = "Data input failed";
                }
                return Ok(repository.Insert(entity));
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            try
            {
                var code = 0;
                var message = "";
                var result = repository.Update(entity);

                if (result == 1)
                {
                    code = StatusCodes.Status200OK;
                    message = $"Data updated";
                }
                else
                {
                    code = StatusCodes.Status400BadRequest;
                    message = "Update failed";
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
                //return Ok();
            }
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            try
            {
                var code = 0;
                var message = "";
                var result = repository.Delete(key);

                if (result == 1)
                {
                    code = StatusCodes.Status200OK;
                    message = "Data deleted";
                }
                else
                {
                    //code = Response.StatusCode;
                    code = StatusCodes.Status400BadRequest;
                    message = $"Data with Id {key} not found";
                }
                return Ok(repository.Delete(key));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
