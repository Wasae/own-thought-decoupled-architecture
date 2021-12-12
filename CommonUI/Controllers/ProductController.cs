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

    public class ProductController : ControllerBase
    {
        private ILogger<PartnerController> _logger;
        private IConfiguration _configuration;
        private ProductCurator _curator;

        public ProductController(ILogger<PartnerController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _curator = new ProductCurator(_configuration);
        }

        public async Task<object> AddProduct(Product_DTO product)
        {
            Response_DTO _dto = new Response_DTO();
            _dto.status = false;
            try
            {
                var t = await _curator.AddProduct(Convert.ToString(product.userid), product.productsku, product.productname, product.price, product.quantity);
                if (t)
                {
                    _dto.status = true;
                    _dto.data = t;
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