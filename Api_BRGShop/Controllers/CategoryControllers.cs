using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using BRG.libary.BusinessService;
using BRG.libary.BusinessService.Common;
using BRG.libary.APICalling;

namespace Api_BRGShop.Controllers
{
    public class CategoryControllers : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/manager/Category/get-all")]
        public IActionResult GetAllCategory()
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<CategoryService.CategoryInfo> ListCategoryInfo = CategoryService.GetInstance().GetListCategory(connection);
                    return Ok(ListCategoryInfo);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }

        }
        [HttpGet]
        [Route("api/maneger/Category/search")]
        public IActionResult SearchCatogory(string search)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    List<CategoryService.CategoryInfo> ListCatogoryInfo = CategoryService.GetInstance().GetListCategory(connection, search);
                    return Ok(ListCatogoryInfo);
            
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return StatusCode(500);
          

            }
        }
        [HttpPost]
        [Route("api/manager/Category/insert")]

        public IActionResult InserCategory([FromBody] CategoryService.CategoryInfo infoInsert)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CategoryService.GetInstance().InsertCategory(connection, infoInsert);
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
        [Route("api/manager/Category/delete")]

        public IActionResult DeleteCategory([FromBody] string CategoryID)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CategoryService.GetInstance().DeleteCategory(connection, CategoryID);
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
        [Route("api/manager/Category/update")]
        public IActionResult UpdateCategory([FromBody] CategoryService.CategoryInfo infoUpdate)
        {
            try
            {
                using (var connection = DefaultConnectionFactory.BRGShop.GetConnection())
                {
                    bool result = CategoryService.GetInstance().UpdateCategory(connection, infoUpdate);
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
