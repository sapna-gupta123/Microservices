using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductService.Infrastructure.Commands;
using ProductService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Category API", Version = "v1" });
});


// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<ProductContext>(o => o.UseSqlServer
    (builder.Configuration["ProductConnection"]), ServiceLifetime.Singleton);

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Category API V1");
        c.RoutePrefix = string.Empty; // Set to empty to serve Swagger UI at the app's root
    });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
