using BRG.libary.APICalling;
using BRG.libary.BusinessService.Common;
using BRG.libary.BusinessService;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api_BRGShop.Controllers
{
    public class CustomerControllers : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/manager/Customer/get-all")]
        public IActionResult GetAllCustomer()
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                   List<CustomerService.CustomerInfo> ListCustomerInfo = CustomerService.GetInstance().GetListCustomer(connection);
                    return Ok(ListCustomerInfo);                 
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }

        }
        [HttpGet]
        [Route("api/manager/Customer/get-user")]
        public IActionResult GetCustomer(string userName)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    CustomerService.CustomerInfo CustomerInfo = CustomerService.GetInstance().GetCustomer(connection, userName);
                    return Ok(CustomerInfo);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);

            }

        }

        [HttpGet]
        [Route("api/maneger/Customer/search")]
        public IActionResult SearchCustomer(string search)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<CustomerService.CustomerInfo> ListCustomerInfo = CustomerService.GetInstance().GetListCustomer(connection, search);
                    return Ok(ListCustomerInfo);
         
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500);

            }
        }
        [HttpPost]
        [Route("api/manager/Customer/insert")]

        public IActionResult InserCustomer([FromBody] CustomerService.CustomerInfo infoInsert)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CustomerService.GetInstance().InsertCustomer(connection, infoInsert);
                    return Ok(result);
                  
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }
        }
        [HttpPost]
        [Route("api/manager/Customer/check-insert-phonenumber")]
        public IActionResult CheckInsertPhoneNumber([FromBody] string PhoneNumber)
        {
            try
            {
                    using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                    {
                        bool result = CustomerService.GetInstance().CheckInsertPhoneNumber(connection, PhoneNumber);
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
        [Route("api/manager/Customer/delete")]

        public IActionResult DeleteCustomer([FromBody] string CustomerID)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CustomerService.GetInstance().DeleteCustomer(connection, CustomerID);
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
        [Route("api/manager/Customer/update")]
        public IActionResult UpdateCustomer([FromBody] CustomerService.CustomerInfo infoUpdate)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CustomerService.GetInstance().UpdateCustomer(connection, infoUpdate);
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
