using AutoMapper;
using Mango.Services.CouponAPI.DbContexts;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Mango.Services.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CouponRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;   
        }
        public async  Task<CouponDto> GetCouponByCode(string couponCode)
        {
            CouponDto couponDto = new CouponDto();
            var respose =  await _db.Coupons.FirstOrDefaultAsync(u => u.CouponCode == couponCode);
            return _mapper.Map<CouponDto>(respose);
        }
    }
}
