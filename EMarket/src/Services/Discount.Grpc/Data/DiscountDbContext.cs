using Discount.Grpc.Models;

namespace Discount.Grpc.Data;

public class DiscountDbContext : DbContext
{
    public DiscountDbContext(DbContextOptions options) : base(options)
    {
    }

    protected DiscountDbContext()
    {
    }

    public DbSet<Coupon> Coupons { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Coupon>().HasData(
                  new Coupon{
                      Id = 1,
                      ProductName = "Samsung S25",
                      Description = "Smartphone with ai",
                      Amount = 1,
                  },
                 new Coupon{
                  Id = 2,
                  ProductName = "Samsung S25",
                  Description = "Smartphone with ai",
                  Amount = 1,
              },
                 new Coupon{
                  Id = 3,
                  ProductName = "Samsung S25",
                  Description = "Smartphone with ai",
                  Amount = 1,
              },
               new Coupon{
                  Id = 4,
                  ProductName = "Samsung S25",
                  Description = "Smartphone with ai",
                  Amount = 1,
              },
               new Coupon{
                  Id = 5,
                  ProductName = "Samsung S25",
                  Description = "Smartphone with ai",
                  Amount = 1,
              },
               new Coupon{
                  Id = 6,
                  ProductName = "Samsung S25",
                  Description = "Smartphone with ai",
                  Amount = 1000,
              },
               new Coupon{
                  Id = 7,
                  ProductName = "Samsung S25",
                  Description = "Smartphone with ai",
                  Amount = 250,
              },
               new Coupon
               {
                   Id = 8,
                   ProductName = "Samsung S25",
                   Description = "Smartphone with ai",
                   Amount = 750,
               },
               new Coupon
               {
                   Id = 9,
                   ProductName = "Samsung S25",
                   Description = "Smartphone with ai",
                   Amount = 999,
               },       
               new Coupon
               {
                   Id = 10,
                   ProductName = "Samsung S25",
                   Description = "Smartphone with ai",
                   Amount = 1200,
               }
               ,
               new Coupon
               {
                   Id = 11,
                   ProductName = "Samsung S25",
                   Description = "Smartphone with ai",
                   Amount = 1500,
               });
           
    }
}