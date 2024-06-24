using BRG.libary.APICalling;
using BRG.libary.BusinessService.Common;
using BRG.libary.BusinessService;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api_BRGShop.Controllers
{
    public class OrderControllers : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/manager/Order/get-all")]
        public IActionResult GetAllOrder()
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<OrderService.OrderInfo> ListOrderInfo = OrderService.GetInstance().GetListOrder(connection);
                    return Ok(ListOrderInfo);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }

        }
        [HttpGet]
        [Route("api/maneger/Order/search")]
        public IActionResult SearchOrder(string search)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<OrderService.OrderInfo> ListOrderInfo = OrderService.GetInstance().GetListOrder(connection, search);
                    return Ok(ListOrderInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500);

            }
        }
        [HttpPost]
        [Route("api/manager/Order/insert")]

        public IActionResult InsertOrder([FromBody] OrderService.OrderInfo infoInsert)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = OrderService.GetInstance().InsertOrder(connection, infoInsert);
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
        [Route("api/manager/Order/delete")]

        public IActionResult DeleteOrder([FromBody] string CategoryID)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = OrderService.GetInstance().DeleteOrder(connection, CategoryID);
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
        [Route("api/manager/Order/update")]
        public IActionResult UpdateOrder([FromBody] OrderService.OrderInfo infoUpdate)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = OrderService.GetInstance().UpdateOrder(connection, infoUpdate);
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
