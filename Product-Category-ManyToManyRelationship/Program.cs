using Application.Config;
using Application.Mapper;
using Application.Services.Implementation;
using Application.Services.Interfaces;
using Domain.Interfaces.Repository;
using Infrastructure.Database;
using Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product_Category_ManyToManyRelationship.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.Configure<Address>(builder.Configuration.GetSection("Address"));
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MapperProfile));


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ProductCategoryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContextConnectionString"));
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
