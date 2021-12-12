using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using AuthLayer.Repository;
using CommonUIHelper.Curator;
using UtilityDTO;

namespace CommonUI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController:ControllerBase
    {    
        private ILogger<AuthController> _logger;
        private IConfiguration _configuration;
        private IAuth _auth;
        private AuthCurator _authcurator;
        public AuthController(IAuth auth,ILogger<AuthController> logger,IConfiguration configuration)
        {
            _auth = auth;
            _logger = logger;
            _configuration = configuration;
            _authcurator = new AuthCurator(_configuration);
        }

        [AllowAnonymous]
        [HttpPost]
        async public Task<object> login([FromBody]Auth_DTO usercreds)
        {
            Response_DTO dto =new Response_DTO();
            dto.status = false;
            try
            {
                if(await _authcurator.login(usercreds.username,usercreds.password))
                {
                    string token = _auth.GenerateToken(usercreds.username,usercreds.password,10);
                    dto.status = true;
                    dto.data = new {
                        token = token
                    };
                    return dto;
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Occurred : "+ ex.Message);
            }
            return dto;
        }

        [HttpGet]
        async public Task<object> validateToken()
        {
            Response_DTO response = new Response_DTO();
            response.status = true;
            return response;
        }
    }
}