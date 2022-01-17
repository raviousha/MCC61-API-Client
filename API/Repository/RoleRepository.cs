using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RoleRepository : GeneralRepository<MyContext, Role, String>
    {
        private readonly MyContext myContext;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        
        public int AssignManager(AssignManagerVM assignManagerVM)
        {
            var acc = myContext.Accounts.FirstOrDefault(a => a.NIK == assignManagerVM.NIK);
            if (acc == null)
            {
                return 0;
            }

            var check = CheckRole(assignManagerVM);
            if (check.Count() >= 1)
            {
                return 2;
            }

            var ar = new AccountRole
            {
                AccountsNIK = acc.NIK,
                RolesId = 2
            };
            myContext.AccountRoles.Add(ar);
            var result = myContext.SaveChanges();

            return result;
        }

        public IEnumerable<object> CheckRole(AssignManagerVM assignManagerVM)
        {
            var empList = myContext.Employees;
            var arList = myContext.AccountRoles;
            var rList = myContext.Roles;

            var query = from emp in empList
                        join ar in arList
                        on emp.NIK equals ar.AccountsNIK
                        where emp.NIK == assignManagerVM.NIK && ar.RolesId == 2
                        select new { ar.RolesId };

            return query;
        }
    }
}
