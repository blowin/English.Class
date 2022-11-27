using English.Class.FastEndpoint;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder();
var service = new AppService(builder.Services);
service.Configure();

var webApp = builder.Build();
new App(webApp).AddMiddleware().Run();