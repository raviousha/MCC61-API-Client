﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class JWTokenVM
    {
        public HttpStatusCode code { get; set; }
        public string token { get; set; }
        public string message { get; set; }
    }
}
