using ASP.NET_SP.DAL;
using ASP.NET_SP.Models;
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
            List<Product>? productList = _dataAccessLayer.GetAllProducts();
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

        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            try
            {
                Product? product = _dataAccessLayer.GetProductById(id);
                if (product != null)
                {
                    TempData["successMessage"] = "Product found.";
                    return View(product);
                }
                else
                {
                    TempData["errorMessage"] = "Product not found.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while fetching product details: " + ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                bool isCreated = false;
                if (ModelState.IsValid)
                {
                    isCreated = _dataAccessLayer.InsertProduct(product);
                    if (isCreated)
                    {
                        TempData["successMessage"] = "Product created successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["errorMessage"] = "Failed to create product.";
                        return View(product);
                    }
                }
                else
                {
                    TempData["errorMessage"] = "Invalid product data.";
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while creating the product: " + ex.Message;
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product? product = _dataAccessLayer.GetProductById(id);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                TempData["errorMessage"] = "Product not found.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            try
            {
                bool isUpdate = false;
                if (ModelState.IsValid)
                {
                    isUpdate = _dataAccessLayer.UpdateProduct(product);
                    if (isUpdate)
                    {
                        TempData["successMessage"] = "Product update successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["errorMessage"] = "Failed to update product.";
                        return View(product);
                    }
                }
                else
                {
                    TempData["errorMessage"] = "Invalid product data.";
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while updating the product: " + ex.Message;
                return View(product);
            }
        }
    }
}
