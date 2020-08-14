using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LZY.WebApi.Controllers
{
    [Authorize]
    [Route("identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "2333";
        }
    }
}