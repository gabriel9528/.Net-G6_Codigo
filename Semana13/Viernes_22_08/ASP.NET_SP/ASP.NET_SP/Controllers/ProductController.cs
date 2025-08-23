using ASP.NET_SP.DAL;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_SP.Controllers
{
    public class ProductController : Controller
    {
        private readonly Product_DAL _dataAccessLayer;

        public ProductController(Product_DAL DAL)
        {
            _dataAccessLayer = DAL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var productList = _dataAccessLayer.GetAllProducts();
            if(productList != null && productList.Count > 0)
            {
                TempData["successMessage"] = "Success.";
                return View(productList);
            }
            else
            {
                TempData["errorMessage"] = "No products found.";
                return View();
            }
            
        }
    }
}
