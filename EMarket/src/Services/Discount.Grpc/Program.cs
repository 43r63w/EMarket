using Discount.Grpc.Extensions;
using Discount.Grpc.RequestPipline;
using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

await app.InitializeDbAsync();

app.MapGrpcService<DiscountService>();

app.Run();