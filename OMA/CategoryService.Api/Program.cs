using CategoryService.Api.Middleware;
using CategoryService.Core;
using CategoryService.Core.Interfaces;
using CategoryService.Core.Services;
using CategoryService.Infrastructure.Data;
using CategoryService.Infrastructure.Interfaces;
using CategoryService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<CategoryContext>(o => o.UseSqlServer
    (builder.Configuration["CategoryConnection"]), ServiceLifetime.Singleton);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
}
    );

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService.Core.Services.CategoryService>();
builder.Services.AddScoped<CategoryServiceFactory>();

builder.Services.AddScoped<EelectronicCategoryService>()
            .AddScoped<ICategoryService, EelectronicCategoryService>(s => s.GetService<EelectronicCategoryService>());

builder.Services.AddScoped<HomeApplianceCategoryService>()
            .AddScoped<ICategoryService, HomeApplianceCategoryService>(s => s.GetService<HomeApplianceCategoryService>());

builder.Services.AddScoped<CategoryService.Core.Services.CategoryService>()
            .AddScoped<ICategoryService, CategoryService.Core.Services.CategoryService>(s => s.GetService<CategoryService.Core.Services.CategoryService>());

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
