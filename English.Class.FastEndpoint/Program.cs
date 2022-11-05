using English.Class.DependencyInjection;
using English.Class.FastEndpoint;
using English.Class.FastEndpoint.ExceptionHandling;
using English.Class.FastEndpoint.ExceptionHandling.ExceptionHandler;
using English.Class.Infrastructure.Database;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints(e =>
{
    e.IncludeAbstractValidators = true;
});
builder.Services.AddSwaggerDoc(settings =>
{
    settings.Title = "English";
    settings.Version = "v1";
});
builder.Services.AddAppServices(optionsBuilder => optionsBuilder.UseSqlite("Data Source=app.db"));
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new DateOnlyConverter());
});
builder.Services.Scan(e =>
{
    e.FromAssemblyOf<Program>()
        .AddClasses(f => f.AssignableTo<IExceptionHandler>()).AsImplementedInterfaces().WithSingletonLifetime();
});

var app = builder.Build();
app.UseAppExceptionHandler();
app.UseAuthorization();
app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
});
app.UseOpenApi(); //add this
app.UseSwaggerUi3(s => s.ConfigureDefaults()); //add this
BeforeRunApp(app);
app.Run();

void BeforeRunApp(WebApplication webApplication)
{
    using var serviceScope = webApplication.Services.CreateScope();
    var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    appDbContext.Database.EnsureCreated();
}