using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess;
using WebShop.Business;
using WebShop.Api;
using FluentValidation;
using WebShop.Business.Validators;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<AddItemModelValidator>();
builder.Host.ConfigureServices(services =>
{
    services.AddDbContext<WebShopContext>(options =>
    {
        var cs = builder.Configuration.GetConnectionString("DefaultConnection");
        //options.UseSqlServer(cs);
        options.UseInMemoryDatabase(cs);
    });
    services.AddTransient<IBasketRepository, BasketRepository>();
});

var app = builder.Build();
CreateDbIfNotExists(app);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-dev");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var service = scope.ServiceProvider;
        try
        {
            var context = service.GetRequiredService<WebShopContext>();
            DBInit.Init(context);
        }
        catch (Exception ex)
        {
            var logger = service.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}
