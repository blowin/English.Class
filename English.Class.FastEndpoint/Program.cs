using English.Class.DependencyInjection;
using English.Class.FastEndpoint;
using English.Class.FastEndpoint.Services;
using English.Class.FastEndpoint.Services.ExceptionHandling;
using English.Class.FastEndpoint.Services.ExceptionHandling.ExceptionHandler;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder();
var service = new AppService(builder.Services);
service.Configure();

var webApp = builder.Build();
new App(webApp).AddMiddleware().Run();