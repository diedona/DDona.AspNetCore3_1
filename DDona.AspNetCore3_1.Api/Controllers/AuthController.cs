using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDona.AspNetCore3_1.Api.Models;
using DDona.AspNetCore3_1.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DDona.AspNetCore3_1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppSettings _AppSettings;
        private readonly TokenService _TokenService;

        public AuthController(IOptions<AppSettings> appSettings, TokenService tokenService)
        {
            _AppSettings = appSettings.Value;
            _TokenService = tokenService;
        }

        [HttpGet]
        public async Task<ActionResult> GetSecret()
        {
            return Ok(_AppSettings.Secret);
        }

        [HttpPost()]
        public async Task<ActionResult> Authenticate(AuthenticationRequest model)
        {
            if (ValidateLogin(model))
                return Ok(GerarToken(model));
            else 
                return NotFound("Usuário / Senha inválidos.");
        }

        private bool ValidateLogin(AuthenticationRequest model)
        {
            return (
                (model.Username.Equals("admin") && model.Password.Equals("123123"))
                ||
                (model.Username.Equals("dev") && model.Password.Equals("123"))
            );
        }

        private AuthenticationResponse GerarToken(AuthenticationRequest model)
        {
            var user = new User()
            {
                Id = 1,
                Password = string.Empty,
                Role = model.Username.Equals("admin") ? "Administrator" : "Developer",
                Username = model.Username
            };

            string token = _TokenService.GenerateToken(user);
            return new AuthenticationResponse(user, token);
        }
    }
}
