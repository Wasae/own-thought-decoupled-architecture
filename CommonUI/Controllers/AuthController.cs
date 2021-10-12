using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using AuthLayer.Repository;

namespace CommonUI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController: ControllerBase
    {
        private IAuth _auth;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuth auth,ILogger<AuthController> logger)
        {
            _auth = auth;
            _logger = logger;
        }

        public object login(string username,string password)
        {
            try
            {
                _logger.LogError("Token generation start");
                var token = _auth.GenerateToken(username,password,10);
                _logger.LogError("Token generation completed");
                return token;
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Occurred : "+ ex.Message);
            }
            return "";
        }
    }
}