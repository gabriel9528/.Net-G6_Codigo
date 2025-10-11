﻿using AutoMapper;
using Microservices.Microservices.CouponAPI.Data;
using Microservices.Microservices.CouponAPI.Models;
using Microservices.Microservices.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Microservices.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        private readonly IMapper _mapper;

        public CouponAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto GetAll()
        {
            try
            {
                IEnumerable<Coupon> couponList = _db.Coupons.Where(x => !x.IsDeleted).ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(couponList);
                _response.Message = "Cupones recuperados con exito";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Ocurrio un error al recuperar los cupones: {ex.Message}";
            }

            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto GetById(int id)
        {
            try
            {
                Coupon? coupon = _db.Coupons.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                if (coupon != null)
                {
                    var couponDto = _mapper.Map<CouponDto>(coupon);
                    _response.Result = couponDto;
                    _response.Message = $"Cupon {couponDto.Code} recuperado con exito";
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cupon no encontrado.";
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Ocurrio un error al recuperar el cupon con id:{id}: {ex.Message}";
            }

            return _response;
        }

        [HttpGet]
        [Route("getByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon? coupon = _db.Coupons.FirstOrDefault(x => x.Code.ToLower().Trim() == code.ToLower().Trim() && !x.IsDeleted);
                if (coupon != null)
                {
                    var couponDto = _mapper.Map<CouponDto>(coupon);
                    _response.Result = couponDto;
                    _response.Message = $"Cupon {couponDto.Code} recuperado con exito";
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cupon no encontrado.";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Ocurrio un error al recuperar el cupon con codigo:{code}: {ex.Message}";
            }

            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponRequestDto couponRequestDto)
        {
            try
            {
                if(couponRequestDto != null)
                {
                    //Coupon coupon = _mapper.Map<Coupon>(couponDto);

                    var newCoupon = new Coupon
                    {
                        Id = couponRequestDto.Id,
                        Code = couponRequestDto.Code,
                        DiscountAmount = couponRequestDto.DiscountAmount,
                        MinimunAmount =couponRequestDto.MinimunAmount
                    };

                    _db.Coupons.Add(newCoupon);
                    _db.SaveChanges();

                    _response.Result = newCoupon.Id;
                    _response.Message = $"Cupon con {couponRequestDto.Code} creado con exito";
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "El cupon ingresado no es valido";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Ocurrio un error al crear el cupon: {ex.Message}";
            }

            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponRequestDto couponRequestDto)
        {
            try
            {
                if(couponRequestDto != null)
                {
                    //Coupon coupon = _mapper.Map<Coupon>(couponDto);

                    Coupon? coupon = _db.Coupons.FirstOrDefault(x => x.Id == couponRequestDto.Id && !x.IsDeleted);
                    if (coupon != null)
                    {
                        coupon.Code = couponRequestDto.Code;
                        coupon.DiscountAmount = couponRequestDto.DiscountAmount;
                        coupon.MinimunAmount = couponRequestDto.MinimunAmount;
                        _db.SaveChanges();

                        _response.Result = couponRequestDto;
                        _response.Message = $"Cupon {couponRequestDto.Code} recuperado con exito";
                    }

                    
                    

                    _response.Result = _mapper.Map<CouponDto>(coupon);
                    _response.Message = $"Cupon con {couponRequestDto.Code} actualizado con exito";
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "El cupon ingresado no es valido";
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Ocurrio un error al actualizar el cupon: {ex.Message}";
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                if(id < 0)
                {
                    _response.IsSuccess = false;
                    _response.Message = "El id del cupon no es valido";
                }
                else
                {
                    Coupon? coupon = _db.Coupons.FirstOrDefault(x => x.Id == id);
                    if(coupon != null) coupon.IsDeleted = true;
                    _db.SaveChanges();

                    _response.IsSuccess = true;
                    _response.Message = $"Cupon eliminado con exito";
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Ocurrio un error al eliminar el cupon: {ex.Message}";
            }

            return _response;
        }
    }
}
