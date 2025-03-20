using Discount.Grpc.Data;
using Discount.Grpc.Extensions;
using Discount.Grpc.Models;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly DiscountDbContext _dbContext;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(
        DiscountDbContext dbContext,
        ILogger<DiscountService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override async Task<CouponModel> CreateDiscount(
        CreateDiscountRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation($"[START] create {nameof(Coupon)}");

        var coupon = Mapper.ToEntity(request);
        var result = _dbContext.Add<Coupon>(coupon);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"[END] {nameof(Coupon)} successfully created");

        return Mapper.ToModel(coupon);
    }

    public override async Task<CouponModel> GetDiscount(
        GetDiscountRequest request,
        ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(e => e.ProductName == request.ProductName);

        return coupon is null
            ? new CouponModel { ProductName = "No Discount", Amount = 0, Description = string.Empty }
            : Mapper.ToModel(coupon);

    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(
        DeleteDiscountRequest request,
        ServerCallContext context)      
    {
        var result = await _dbContext.Coupons.Where(e => e.ProductName == request.ProductName).ExecuteDeleteAsync();
        return result == 1
            ? new DeleteDiscountResponse { Success = true }
            : new DeleteDiscountResponse { Success = false };
    }

    public override async Task<UpdateDiscountResponse> UpdateDiscount(
        UpdateDiscountRequest request,
        ServerCallContext context)
    {
        _dbContext.Coupons.ExecuteUpdate(
            amount => amount
            .SetProperty(e => e.Amount, request.Coupon.Amount)
            .SetProperty(e => e.Description, request.Coupon.Description));

        await _dbContext.SaveChangesAsync();

        return new UpdateDiscountResponse { ProductName = request.Coupon.ProductName };
    }
}
