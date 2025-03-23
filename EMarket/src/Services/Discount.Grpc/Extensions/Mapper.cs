using Discount.Grpc.Models;

namespace Discount.Grpc.Extensions;

public static class Mapper
{
    public static Coupon ToEntity(this CreateDiscountRequest request)
    {
        return new Coupon
        {
            ProductName = request.Coupon.ProductName,
            Description = request.Coupon.Description,
            Amount = request.Coupon.Amount,
        };
    }

    public static CouponModel ToModel(this Coupon coupon)
    {
        return new CouponModel
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount
        };
    }
}
