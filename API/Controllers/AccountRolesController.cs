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
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, string>
    {
        public AccountRolesController(AccountRoleRepository accountRoleRepository) : base(accountRoleRepository)
        {

        }
    }
}
