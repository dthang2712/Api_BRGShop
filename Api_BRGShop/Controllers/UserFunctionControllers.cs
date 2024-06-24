using BRG.libary.APICalling;
using BRG.libary.BusinessService.Common;
using BRG.libary.BusinessService;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api_BRGShop.Controllers
{
    public class UserFunctionControllers : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/manager/Userfunction/get-all")]
        public IActionResult GetAllUserFunction()
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<UserFunctionService.UserFunctionInfo> ListUserFunctionInfo = UserFunctionService.GetInstance().GetListUserFunction(connection);
                    return Ok(ListUserFunctionInfo);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }

        }
        [HttpGet]
        [Route("api/maneger/UserFunction/search")]
        public IActionResult SearchUserFunction(string search)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<UserFunctionService.UserFunctionInfo> ListUserFunctionInfo = UserFunctionService.GetInstance().GetListUserFunction(connection, search);
                    return Ok(ListUserFunctionInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500);

            }
        }
        [HttpPost]
        [Route("api/manager/UserFunction/insert")]

        public IActionResult InserUserFunction([FromBody] UserFunctionService.UserFunctionInfo infoInsert)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = UserFunctionService.GetInstance().InsertUserFunction(connection, infoInsert);
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
        [Route("api/manager/UserFunction/delete")]

        public IActionResult DeleteUserFunction([FromBody] string CategoryID)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = UserFunctionService.GetInstance().DeleteUserFunction(connection, CategoryID);
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
        [Route("api/manager/UserFunction/update")]
        public IActionResult UpdateUserFunction([FromBody] UserFunctionService.UserFunctionInfo infoUpdate)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = UserFunctionService.GetInstance().UpdateUserFunction(connection, infoUpdate);
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
