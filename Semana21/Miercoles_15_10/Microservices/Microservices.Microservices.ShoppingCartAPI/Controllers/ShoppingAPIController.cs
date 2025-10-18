using AutoMapper;
using Microservices.Microservices.ShoppingCartAPI.Data;
using Microservices.Microservices.ShoppingCartAPI.Models;
using Microservices.Microservices.ShoppingCartAPI.Models.Dto;
using Microservices.Microservices.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Microservices.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly ICouponService _couponService;
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public ShoppingAPIController(ApplicationDbContext db, 
            IMapper mapper, 
            ICouponService couponService, 
            IProductService productService, 
            IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _couponService = couponService;
            _productService = productService;
            _configuration = configuration;
            _response = new ResponseDto();
        }

        [HttpPost("ApplyCoupon")]
        public ResponseDto ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                CartHeader? cartHeaderFromDb = _db.CartHeaders.FirstOrDefault(x => x.UserId == cartDto.CartHeaderDto.UserId && !x.IsDeleted);
                if(cartHeaderFromDb != null)
                {
                    cartHeaderFromDb.CouponCode = cartDto.CartHeaderDto?.CouponCode;
                    _db.CartHeaders.Update(cartHeaderFromDb);
                    _db.SaveChangesAsync();
                }
                
                _response.Result = true;
                _response.Message = "Cupon aplicado exitosamente";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Ocurrio un error: {ex.Message}";
            }

            return _response;
        }


        [HttpPost("RemoveCoupon")]
        public ResponseDto RemoveCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                CartHeader? cartHeaderFromDb = _db.CartHeaders.FirstOrDefault(x => x.UserId == cartDto.CartHeaderDto.UserId && !x.IsDeleted);
                if (cartHeaderFromDb != null)
                {
                    cartHeaderFromDb.CouponCode = "";
                    _db.CartHeaders.Update(cartHeaderFromDb);
                    _db.SaveChangesAsync();

                    _response.Result = true;
                    _response.Message = "Cupon eliminado con exito";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Ocurrio un error: {ex.Message}";
            }

            return _response;
        }

        [HttpPost("UpSert")]
        public ResponseDto CartUpsert(CartDto cartDto)
        {
            try
            {
                CartHeader? cartHeaderFromDb = _db.CartHeaders.AsNoTracking().FirstOrDefault(x => x.UserId == cartDto.CartHeaderDto.UserId);
                //Post
                CartHeader newCartHeader = new();
                CartDetails newCartDetails = new();
                if(cartHeaderFromDb == null)
                {
                    newCartHeader.UserId = cartDto.CartHeaderDto.UserId;
                    newCartHeader.CouponCode = cartDto.CartHeaderDto.CouponCode;
                    newCartHeader.Discount = cartDto.CartHeaderDto.Discount;
                    newCartHeader.CartTotal = cartDto.CartHeaderDto.CartTotal;

                    _db.CartHeaders.Add(newCartHeader);
                    _db.SaveChanges();

                    //Relacionamos los cartDetails
                    cartDto.CartDetailsDto.First().CartHeaderId = newCartHeader.Id;

                    CartDetailsDto? carDetailsDtoRequest = cartDto.CartDetailsDto.First();
                    newCartDetails.CartHeaderId = newCartHeader.Id;
                    newCartDetails.ProductId = carDetailsDtoRequest.ProductId;
                    newCartDetails.Count = carDetailsDtoRequest.Count;

                    _db.CartDetails.Add(newCartDetails);
                    _db.SaveChanges();

                    _response.Result = newCartHeader.Id;
                    _response.Message = "Cart creado con exito";
                }
                //Edit
                else
                {
                    //Revisamos si los detalles tienen el mismo producto
                    CartDetails? carDetailsFromDb = _db.CartDetails.AsNoTracking().FirstOrDefault(
                        x => x.ProductId == cartDto.CartDetailsDto.First().ProductId &&
                        x.CartHeaderId == cartHeaderFromDb.Id);

                    if (carDetailsFromDb == null)
                    {
                        //cartDto.CartDetailsDto.First().CartHeaderId = cartHeaderFromDb.Id;
                        
                        CartDetailsDto? carDetailsDtoRequest = cartDto.CartDetailsDto.First();
                        newCartDetails.CartHeaderId = carDetailsDtoRequest.CartHeaderId;
                        newCartDetails.ProductId = carDetailsDtoRequest.ProductId;
                        newCartDetails.Count = carDetailsDtoRequest.Count;

                        _db.CartDetails.Add(newCartDetails);
                        _db.SaveChanges();

                        _response.Result = newCartHeader.Id;
                        _response.Message = "CartDetails agregados con exito";
                    }
                    else
                    {
                        //actualizamos los details
                        carDetailsFromDb.Count += cartDto.CartDetailsDto.First().Count;
                        carDetailsFromDb.CartHeaderId = cartDto.CartDetailsDto.First().CartHeaderId;
                        //carDetailsFromDb.Id = cartDto.CartDetailsDto.First().Id;
                        carDetailsFromDb.ProductId = cartDto.CartDetailsDto.First().ProductId;

                        _db.CartDetails.Update(carDetailsFromDb);
                        _db.SaveChanges();

                        _response.Result = true;
                        _response.Message = "CartDeatils actualizados con exito";
                    }
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Ocurrio un error: {ex.Message}";
            }

            return _response;
        }


        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCartByUserId(string userId)
        {
            try
            {
                CartHeaderDto cartHeaderDto = new CartHeaderDto();
                CartHeader? cartHeader = _db.CartHeaders.FirstOrDefault(x => x.UserId == userId && !x.IsDeleted);
                if (cartHeader != null)
                {
                    cartHeaderDto.Id = cartHeader.Id;
                    cartHeaderDto.UserId = cartHeader.UserId;
                    cartHeaderDto.CouponCode = cartHeader.CouponCode;
                    cartHeaderDto.Discount = cartHeader.Discount;
                    cartHeaderDto.CartTotal = cartHeader.CartTotal == null ? 0 : cartHeader.CartTotal;
                }
                CartDto cartDto = new CartDto()
                {
                    CartHeaderDto = cartHeaderDto,
                    CartDetailsDto = _db.CartDetails
                        .Where(x => x.CartHeaderId == cartHeaderDto.Id)
                        .Select(cd => new CartDetailsDto
                            {
                                Id = cd.Id,
                                CartHeaderId = cd.CartHeaderId,
                                ProductId = cd.ProductId,
                                Count = cd.Count
                            })
                        .ToList()
                };

                //ProductService
                IEnumerable<ProductDto> listProducts = await _productService.GetProducts();
                foreach(var item in cartDto.CartDetailsDto)
                {
                    item.ProductDto = listProducts.FirstOrDefault(x => x.Id == item.ProductId);
                    cartDto.CartHeaderDto.CartTotal += (item.Count * item.ProductDto.Price);
                }

                //CouponService
                if (!string.IsNullOrEmpty(cartDto.CartHeaderDto.CouponCode))
                {
                    CouponDto couponDto = await _couponService.GetCoupon(cartDto.CartHeaderDto.CouponCode);
                    if(couponDto != null && cartDto.CartHeaderDto.CartTotal > couponDto.MinimunAmount) 
                    {
                        cartDto.CartHeaderDto.CartTotal -= couponDto.DiscountAmount;
                        cartDto.CartHeaderDto.Discount = couponDto.DiscountAmount;
                    }
                }

                _response.Result = cartDto;
                _response.Message = "Cart obtenido con exito";

            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Ocurrio un error: {ex.Message}";
            }

            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                CartDetails? cartDetails = _db.CartDetails.FirstOrDefault(x => x.Id == cartDetailsId && !x.IsDeleted);
                cartDetails.IsDeleted = true;
                _db.CartDetails.Update(cartDetails);
                int totalCountOfCartItems = _db.CartDetails.Where(x => x.CartHeaderId == cartDetails.CartHeaderId && !x.IsDeleted).Count();

                if (totalCountOfCartItems == 1)
                {
                    CartHeader? cartHeaderFromDb = await _db.CartHeaders.FirstOrDefaultAsync(x => x.Id == cartDetails.CartHeaderId);
                    cartHeaderFromDb.IsDeleted = true;
                    _db.CartHeaders.Update(cartHeaderFromDb);
                }
                _db.SaveChanges();

                _response.Result = true;
                _response.Message = "Cart eliminado con exito";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Ocurrio un error: {ex.Message}";
            }

            return _response;
        }
    }
}
