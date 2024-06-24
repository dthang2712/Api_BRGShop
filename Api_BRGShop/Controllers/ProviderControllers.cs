using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using BRG.libary.BusinessService;
using BRG.libary.BusinessService.Common;
using BRG.libary.APICalling;

namespace Api_BRGShop.Controllers
{
    public class ProviderControllers : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/manager/Provider/get-all")]
        public IActionResult GetAllProvider()
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<ProviderService.ProviderInfo> listProviderInfo = ProviderService.GetInstance().GetListProvider(connection);

                    return Ok(listProviderInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/manager/Provider/search")]
        public IActionResult SearchProvider(string search)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<ProviderService.ProviderInfo> listProviderInfo = ProviderService.GetInstance().GetListProvider(connection, search);

                    return Ok(listProviderInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("api/manager/Provider/insert")]
        public IActionResult InsertProvider([FromBody] ProviderService.ProviderInfo infoInsert)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = ProviderService.GetInstance().InsertProvider(connection, infoInsert);

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
        [Route("api/manager/Provider/delete")]
        public IActionResult DeleteProvider([FromBody] string ProviderID)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = ProviderService.GetInstance().DeleteProvider(connection, ProviderID);

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
        [Route("api/manager/Provider/update")]
        public IActionResult UpdateProvider([FromBody] ProviderService.ProviderInfo infoUpdate)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = ProviderService.GetInstance().UpdateProvider(connection, infoUpdate);

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

