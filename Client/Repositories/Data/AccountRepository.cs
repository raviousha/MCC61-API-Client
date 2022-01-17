using API.Models;
using API.ViewModels;
using Client.Base.Urls;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAcessor;
        private readonly HttpClient httpClient;
        public AccountRepository(Address address, string Request = "Accounts/") : base(address, Request)
        {
            this.address = address;
            this.request = Request;
            _contextAcessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
    }
}
