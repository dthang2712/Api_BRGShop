using BRG.libary.APICalling;
using BRG.libary.BusinessService.Common;
using BRG.libary.BusinessService;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api_BRGShop.Controllers
{
    public class CustomerAddressControllers : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/manager/CustomerAddress/get-all")]
        public IActionResult GetAllCatogory()
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<CustomerAddressService.CustomerAddressInfo> ListCustomerAddressInfo = CustomerAddressService.GetInstance().GetListCustomerAddress(connection);
                    return Ok(ListCustomerAddressInfo);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }

        }
        [HttpGet]
        [Route("api/maneger/CustomerAddress/search")]
        public IActionResult SearchCustomerAddress(string search)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<CustomerAddressService.CustomerAddressInfo> ListCustomerAddressInfo = CustomerAddressService.GetInstance().GetListCustomerAddress(connection, search);
                    return Ok(ListCustomerAddressInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500);

            }
        }
        [HttpPost]
        [Route("api/manager/CustomerAddress/insert")]

        public IActionResult InserCustomerAddress([FromBody] CustomerAddressService.CustomerAddressInfo infoInsert)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CustomerAddressService.GetInstance().InsertCustomerAddress(connection, infoInsert);
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
        [Route("api/manager/CustomerAddress/delete")]

        public IActionResult DeleteCustomerAddress([FromBody] string CustomerID)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CustomerAddressService.GetInstance().DeleteCustomerAddress(connection, CustomerID);
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
        [Route("api/manager/CustomerAddress/update")]
        public IActionResult UpdateCustomerAddress([FromBody] CustomerAddressService.CustomerAddressInfo infoUpdate)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CustomerAddressService.GetInstance().UpdateCustomerAddress(connection, infoUpdate);
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
