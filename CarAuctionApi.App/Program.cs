using CarAuctionApi.App.Middlewares;
using CarAuctionApi.Data;
using CarAuctionApi.Service;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataService(builder.Configuration);
builder.Services.AddService();
builder.Services.AddRouting(opt=>opt.LowercaseUrls=true);
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
var app = builder.Build();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.HandleException();

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.Run();
