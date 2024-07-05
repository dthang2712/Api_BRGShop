using BRG.libary.APICalling;
using BRG.libary.BusinessService.Common;
using BRG.libary.BusinessService;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api_BRGShop.Controllers
{
    public class CartControllers : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/manager/Cart/get-all")]
        public IActionResult GetAllCart()
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<CartService.CartInfo> listCartInfo = CartService.GetInstance().GetListCart(connection);

                    return Ok(listCartInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500); 
            }
        }
        [HttpGet]
        [Route("api/manager/Cart/get-product-customer")]

        public IActionResult GetCartCustomer(int CustomerID)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<CartService.CartInfo> listCartInfo = CartService.GetInstance().GetCartCustomer(connection , CustomerID);

                    return Ok(listCartInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/manager/Cart/search")]
        public IActionResult SearchCart(string search)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<CartService.CartInfo> listCartInfo = CartService.GetInstance().GetListCart(connection, search);

                    return Ok(listCartInfo); 
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("api/manager/Cart/insert")]
        public IActionResult InsertCart([FromBody] CartService.CartInfo infoInsert)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CartService.GetInstance().InsertCart(connection, infoInsert);

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
        [Route("api/manager/Cart/delete")]
        public IActionResult DeleteCart([FromBody] string AutoID)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CartService.GetInstance().DeleteCart(connection, AutoID);

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
        [Route("api/manager/Cart/update")]
        public IActionResult UpdateCart([FromBody] CartService.CartInfo infoUpdate)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CartService.GetInstance().UpdateCart(connection, infoUpdate);

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
