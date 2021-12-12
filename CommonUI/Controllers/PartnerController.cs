using System;
using System.Threading.Tasks;
using CommonUIHelper.Curator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UtilityDTO;

namespace CommonUI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class PartnerController : ControllerBase
    {
        private ILogger<PartnerController> _logger;
        private IConfiguration _configuration;
        private PartnerCurator _curator;

        public PartnerController(ILogger<PartnerController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _curator = new PartnerCurator(_configuration);
        }

        [HttpGet]
        public async Task<object> GetUerProducts(string userid)
        {
            Response_DTO _dto = new Response_DTO();
            _dto.status = false;
            try
            {
                if (!String.IsNullOrEmpty(userid))
                {
                    var t = await _curator.GetUerProducts(Convert.ToInt32(userid));
                    if (t != null)
                    {
                        _dto.status = true;
                        _dto.data = t;
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Occurred : " + ex.Message);
            }
            return _dto;
        }

        [HttpGet]
        public async Task<object> GetUserCustomers(string userid)
        {
            Response_DTO _dto = new Response_DTO();
            _dto.status = false;
            try
            {
                if (!String.IsNullOrEmpty(userid))
                {
                    var t = await _curator.GetUserCustomers(Convert.ToInt32(userid));
                    if (t != null)
                    {
                        _dto.status = true;
                        _dto.data = t;
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Occurred : " + ex.Message);
            }
            return _dto;
        }

        [HttpGet]
        public async Task<object> GetUserRFQ(string userid)
        {
            Response_DTO _dto = new Response_DTO();
            _dto.status = false;
            try
            {
                if (!String.IsNullOrEmpty(userid))
                {
                    var t = await _curator.GetUserRFQ(Convert.ToInt32(userid));
                    if (t != null)
                    {
                        _dto.status = true;
                        _dto.data = t;
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Occurred : " + ex.Message);
            }
            return _dto;
        }

        [HttpGet]
        public async Task<object> GetUserRFQDetails(string rfqid, string userid)
        {
            Response_DTO _dto = new Response_DTO();
            _dto.status = false;
            try
            {
                if (!String.IsNullOrEmpty(userid))
                {
                    var t = await _curator.GetUserRFQDetails(rfqid, Convert.ToInt32(userid));
                    if (t != null)
                    {
                        _dto.status = true;
                        _dto.data = t;
                    }
                }
            }   
            catch (System.Exception ex)
            {
                _logger.LogError("Error Occurred : " + ex.Message);
            }
            return _dto;
        }
    }
}