using BRG.libary.APICalling;
using BRG.libary.BusinessService.Common;
using BRG.libary.BusinessService;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api_BRGShop.Controllers
{
    public class OrderDetailControllers : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/manager/OrderDetail/get-all")]
        public IActionResult GetAllOrderDetail()
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<OrderDetailService.OrderDetailInfo> ListOrderDetailInfo = OrderDetailService.GetInstance().GetListOrderDetail(connection);
                    return Ok(ListOrderDetailInfo);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500); 
            }

        }
        [HttpGet]
        [Route("api/maneger/OrderDetail/search")]
        public IActionResult SearchOrderDetail(string search)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<OrderDetailService.OrderDetailInfo> ListOrderDetailInfo = OrderDetailService.GetInstance().GetListOrderDetail(connection, search);
                    return Ok(ListOrderDetailInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500);

            }
        }
        [HttpPost]
        [Route("api/manager/OrderDetail/insert")]

        public IActionResult InsertOrderDetail([FromBody] OrderDetailService.OrderDetailInfo infoInsert)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = OrderDetailService.GetInstance().InsertOrderDetail(connection, infoInsert);
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
        [Route("api/manager/OrderDetail/delete")]

        public IActionResult DeleteOrderDetail([FromBody] string CategoryID)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = OrderDetailService.GetInstance().DeleteOrderDetail(connection, CategoryID);
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
        [Route("api/manager/OrderDetail/update")]
        public IActionResult UpdateOrderDetail([FromBody] OrderDetailService.OrderDetailInfo infoUpdate)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = OrderDetailService.GetInstance().UpdateOrderDetail(connection, infoUpdate);
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
