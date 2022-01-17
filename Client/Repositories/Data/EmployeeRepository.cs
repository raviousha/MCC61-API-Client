using API.Models;
using API.ViewModels;
using Client.Base.Urls;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAcessor;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string Request = "Employee/") : base(address, Request)
        {
            this.address = address;
            this.request = Request;
            _contextAcessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<RegisterVM>> GetRegisteredData()
        {
            List<RegisterVM> entities = new List<RegisterVM>();

            using (var response = await httpClient.GetAsync(request+"show/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);
            }
            return entities;
        }

        public HttpStatusCode DeleteRegisteredData(string nik)
        {
            var result = httpClient.DeleteAsync(request + "delete/" + nik).Result;
            return result.StatusCode;
        }

        public Object Register(RegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");

            Object entities = new Object();

            using (var response = httpClient.PostAsync(request + "register/", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return entities;
        }
    }
}
