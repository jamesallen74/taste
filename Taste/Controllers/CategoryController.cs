using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitofWork.Category.GetAll() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var str = "";
            var objFromDb = _unitofWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new {success=false, message="Error while deleting" });
            }
            str = objFromDb.Name;
            _unitofWork.Category.Remove(objFromDb);
            _unitofWork.Save();
            return Json(new { success=true, message=$"Deleted '{str}'." });
        }
    }
}
