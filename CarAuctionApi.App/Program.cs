using CarAuctionApi.App;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterAppServices(builder.Configuration,builder.Host);



var app = builder.Build();

await app.RunApp();