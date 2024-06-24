using BRG.libary.APICalling;
using BRG.libary.BusinessService.Common;
using BRG.libary.BusinessService;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api_BRGShop.Controllers
{
    public class ProductDiscountControllers : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/manager/ProductDiscount/get-all")]
        public IActionResult GetAllProductDiscount()
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<ProductDiscountService.ProductDiscountInfo> ListProductDiscountInfo = ProductDiscountService.GetInstance().GetListProductDiscount(connection);
                    return Ok(ListProductDiscountInfo);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500); 
            }

        }
        [HttpGet]
        [Route("api/maneger/ProductDiscount/search")]
        public IActionResult SearchProductDiscount(string search)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<ProductDiscountService.ProductDiscountInfo> ListProductDiscountInfo = ProductDiscountService.GetInstance().GetListProductDiscount(connection, search);
                    return Ok(ListProductDiscountInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500);

            }
        }
        [HttpPost]
        [Route("api/manager/ProductDiscount/insert")]

        public IActionResult InserProductDiscount([FromBody] ProductDiscountService.ProductDiscountInfo infoInsert)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = ProductDiscountService.GetInstance().InsertProductDiscount(connection, infoInsert);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }
        }
        [HttpDelete]
        [Route("api/manager/ProductDiscount/delete")]

        public IActionResult DeleteProductDiscount([FromBody] string ProductID)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = ProductDiscountService.GetInstance().DeleteProductDiscount(connection, ProductID);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }
        }
        [HttpPut]
        [Route("api/manager/ProductDiscount/update")]
        public IActionResult UpdateProductDiscount([FromBody] ProductDiscountService.ProductDiscountInfo infoUpdate)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = ProductDiscountService.GetInstance().UpdateProductDiscount(connection, infoUpdate);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
