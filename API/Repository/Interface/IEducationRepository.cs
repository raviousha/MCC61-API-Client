using API.Models;
using System;
using System.Collections.Generic;

namespace API.Repository.Interface
{
    interface IEducationRepository
    {
        IEnumerable<Education> Get();
        Education Get(String Id);
        int Insert(Education education);
        int Update(String Id, Education education);
        int Delete(String Id);
    }
}
