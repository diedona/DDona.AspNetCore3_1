using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDona.AspNetCore3_1.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoggedController : ControllerBase
    {
        public ActionResult Get()
        {
            return Ok($"OK - Você está logado, {User.Identity.Name}");
        }

        [Route("admin")]
        [Authorize(Roles = "Administrator")]
        public ActionResult GetAdmin()
        {
            return Ok("OK - APENAS PARA ADMINISTRADORES");
        }

        [Route("dev")]
        [Authorize(Roles = "Administrator,Developer")]
        public ActionResult GetDev()
        {
            return Ok("OK - DEV E ADMINISTRADORES");
        }
    }
}
