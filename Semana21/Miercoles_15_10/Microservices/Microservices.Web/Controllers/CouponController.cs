using Microservices.Web.Models;
using Microservices.Web.Models.CouponDtos;
using Microservices.Web.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microservices.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }


        [HttpGet]
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? listCoupon = new();

            ResponseDto? responseDto = await _couponService.GetAllCouponsAsync();
            if (responseDto != null && responseDto.IsSuccess)
            {
                listCoupon = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }

            return View(listCoupon);
        }

        #region Create

        [HttpGet]
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponRequestDto couponRequestDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await _couponService.CreateCouponAsync(couponRequestDto);
                if (responseDto != null && responseDto.IsSuccess)
                {
                    TempData["success"] = "Cupon creado exitosamente";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                }
            }

            return View(couponRequestDto);
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> CouponEdit(int couponId)
        {
            CouponDto? couponDto = new();
            CouponRequestDto? couponRequestDto = new();
            ResponseDto? responseDto = await _couponService.GetCouponByIdAsync(couponId);
            if (responseDto != null && responseDto.IsSuccess)
            {
                couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
                if (couponDto != null)
                {
                    couponRequestDto.Id = couponDto.Id;
                    couponRequestDto.Code = couponDto.Code;
                    couponRequestDto.DiscountAmount = couponDto.DiscountAmount;
                    couponRequestDto.MinimunAmount = couponDto.MinimunAmount;
                }

                return View(couponRequestDto);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CouponEdit(CouponRequestDto couponRequestDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await _couponService.UpdateCouponAsync(couponRequestDto);
                if (responseDto != null && responseDto.IsSuccess)
                {
                    TempData["success"] = "Cupon actualizado exitosamente";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                }
            }

            return View(couponRequestDto);
        }

        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            CouponDto? couponDto = new();

            ResponseDto? responseDto = await _couponService.GetCouponByIdAsync(couponId);
            if (responseDto != null && responseDto.IsSuccess)
            {
                couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));

                return View(couponDto);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? responseDto = await _couponService.DeleteCouponAsync(couponDto.Id);
            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Cupon actualizado exitosamente";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }

            return View(couponDto);
        }

        #endregion
    }
}
