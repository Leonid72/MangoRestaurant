using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Mango.Services.ShoppingCartAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CouponDto, Coupon>().ReverseMap();
        }
    }
}
