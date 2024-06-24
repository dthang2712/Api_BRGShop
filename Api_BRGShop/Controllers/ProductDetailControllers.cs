using BRG.libary.APICalling;
using BRG.libary.BusinessService.Common;
using BRG.libary.BusinessService;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api_BRGShop.Controllers
{
    public class ProductDetailControllers : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/manager/ProductDetail/get-all")]
        public IActionResult GetAllProductDetail()
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<ProductDetailService.ProductDetailInfo> ListProductDetailInfo = ProductDetailService.GetInstance().GetListProductDetail(connection);
                    return Ok(ListProductDetailInfo);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }

        }
        [HttpGet]
        [Route("api/maneger/ProductDetail/search")]
        public IActionResult SearchProductDetail(string search)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<ProductDetailService.ProductDetailInfo> ListProductDetailInfo = ProductDetailService.GetInstance().GetListProductDetail(connection, search);
                    return Ok(ListProductDetailInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500);

            }
        }
        [HttpPost]
        [Route("api/manager/ProductDetail/insert")]

        public IActionResult InserProductDetail([FromBody] ProductDetailService.ProductDetailInfo infoInsert)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = ProductDetailService.GetInstance().InsertProductDetail(connection, infoInsert);
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
        [Route("api/manager/ProductDetail/delete")]

        public IActionResult DeleteProductDetail([FromBody] string ProductID)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = ProductDetailService.GetInstance().DeleteProductDetail(connection, ProductID);
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
        [Route("api/manager/ProductDetail/update")]
        public IActionResult UpdateProductDetail([FromBody] ProductDetailService.ProductDetailInfo infoUpdate)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = ProductDetailService.GetInstance().UpdateProductDetail(connection, infoUpdate);
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
