using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, String>
    {
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
