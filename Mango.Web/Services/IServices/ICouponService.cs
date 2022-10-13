namespace Mango.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);

    }
}
