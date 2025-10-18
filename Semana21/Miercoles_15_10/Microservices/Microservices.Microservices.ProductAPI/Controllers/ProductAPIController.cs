using Microservices.Microservices.ProductAPI.Data;
using Microservices.Microservices.ProductAPI.Models;
using Microservices.Microservices.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Microservices.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;

        public ProductAPIController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ResponseDto GetAll()
        {
            try
            {
                IEnumerable<Product> productList = _db.Products.Where(x => !x.IsDeleted).ToList();
                List<ProductDto> productDtoList = new List<ProductDto>();

                foreach (Product product in productList)
                {
                    ProductDto newProductDto = new ProductDto();
                    newProductDto.Id = product.Id;
                    newProductDto.Name = product.Name;
                    newProductDto.Price = product.Price;
                    newProductDto.Description = product.Description;
                    newProductDto.CategoryName = product.CategoryName;
                    newProductDto.ImageUrl = product.ImageUrl;

                    productDtoList.Add(newProductDto);
                }
                _response.Message = "Productos obtenidos con exito";
                _response.Result = productDtoList;

            }
            catch (Exception ex)
            {
                _response.Message = "Error: " + ex.Message;
                _response.IsSuccess = false;
                _response.Result = null;
            }

            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto GetById(int id)
        {
            ProductDto? productDto = new ProductDto();
            try
            {
                Product? product = _db.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
                if (product != null)
                {
                    productDto.Id = product.Id;
                    productDto.Name = product.Name;
                    productDto.Price = product.Price;
                    productDto.Description = product.Description;
                    productDto.CategoryName = product.CategoryName;
                    productDto.ImageUrl = product.ImageUrl;

                    _response.Message = $"Producto {productDto.Name} recuperado con exito";
                    _response.Result = productDto;
                }
                else
                {
                    _response.Message = $"Producto no encontrado";
                    _response.Result = null;
                    _response.IsSuccess = false;
                }

            }
            catch (Exception ex)
            {
                _response.Message = "Error: " + ex.Message;
                _response.IsSuccess = false;
                _response.Result = null;
            }

            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            Product newProduct = new Product();

            newProduct.Id = productDto.Id;
            newProduct.Name = productDto.Name;
            newProduct.Price = productDto.Price;
            newProduct.Description = productDto.Description;
            newProduct.CategoryName = productDto.CategoryName;
            newProduct.ImageUrl = productDto.ImageUrl;

            try
            {
                if (newProduct != null)
                {
                    _db.Products.Add(newProduct);
                    _db.SaveChanges();

                    _response.Result = newProduct.Id;
                    _response.Message = $"Producto con id: {newProduct.Id} REGISTRADO exitosamente";
                }
                else
                {
                    _response.Message = "El producto ingresado no es valido";
                    _response.IsSuccess = false;
                    _response.Result = null;
                }
            }
            catch (Exception ex)
            {
                _response.Message = "Error: " + ex.Message;
                _response.IsSuccess = false;
                _response.Result = null;
            }

            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ResponseDto Put(ProductDto productDto)
        {
            try
            {
                if (productDto != null)
                {
                    Product? updateProduct = _db.Products.FirstOrDefault(p => p.Id == productDto.Id && !p.IsDeleted);
                    if (updateProduct != null)
                    {
                        updateProduct.Name = productDto.Name;
                        updateProduct.Price = productDto.Price;
                        updateProduct.Description = productDto.Description;
                        updateProduct.CategoryName = productDto.CategoryName;
                        updateProduct.ImageUrl = productDto.ImageUrl;

                        _db.Products.Update(updateProduct);
                        _db.SaveChanges();

                        _response.Result = productDto.Id;
                        _response.Message = $"Producto con id: {productDto.Id} actualizado con exito";
                    }
                    else
                    {
                        _response.Message = $"Producto no encontrado";
                        _response.Result = null;
                        _response.IsSuccess = false;
                    }

                }
                else
                {
                    _response.Message = "El producto ingresado no es valido";
                    _response.IsSuccess = false;
                    _response.Result = null;
                }

            }
            catch (Exception ex)
            {
                _response.Message = "Error: " + ex.Message;
                _response.IsSuccess = false;
                _response.Result = null;
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public ResponseDto Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _response.Message = "El id el producto ingresado no es valido";
                    _response.IsSuccess = false;
                    _response.Result = null;
                }
                else
                {
                    Product? product = _db.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);

                    if (product != null)
                    {
                        product.IsDeleted = true;
                        _db.Products.Update(product);
                        _db.SaveChanges();

                        ProductDto? productDto = new ProductDto();
                        productDto.Id = product.Id;

                        _response.Result = productDto.Id;
                        _response.Message = $"Producto con id: {productDto.Id} eliminado exitosamente";
                    }
                    else
                    {
                        _response.Message = "No se encontro ningun registro con ese id";
                        _response.IsSuccess = false;
                        _response.Result = null;
                    }

                }
            }
            catch (Exception ex)
            {
                _response.Message = "Error: " + ex.Message;
                _response.IsSuccess = false;
                _response.Result = null;
            }

            return _response;
        }
    }
}
