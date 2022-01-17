using API.Models;
using API.Repository;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class RolesController : BaseController<Role, RoleRepository, string>
    {
        private readonly RoleRepository roleRepository;
        public RolesController(RoleRepository roleRepository) : base(roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [Authorize(Roles = "director")]
        [HttpPost]
        [Route("assignmanager")]
        public ActionResult AssignManager(AssignManagerVM assignManagerVM)
        {
            try
            {
                var code = 0;
                var message = "";
                var result = roleRepository.AssignManager(assignManagerVM);

                switch (result)
                {
                    case 1:
                        {
                            code = StatusCodes.Status200OK;
                            message = $"Assign Manager Role to NIK {assignManagerVM.NIK} Success";
                            break;
                        }
                    case 2:
                        {
                            code = StatusCodes.Status400BadRequest;
                            message = $"NIK {assignManagerVM.NIK} Already Assigned as Manager";
                            break;
                        }
                    default:
                        {
                            code = StatusCodes.Status400BadRequest;
                            message = "Error";
                            break;
                        }
                }
                return Ok(new { code, result, message });
            }
            catch (Exception e)
            {
                return Ok($"{e.Message}");
            }
        }
    }
}
