using EduPersona.Core.Api.HostedServices;
using EduPersona.Core.Api.Middleware;
using EduPersona.Core.Data;
using Microsoft.EntityFrameworkCore;
using EduPersona.Core.Data.Extension;
using EduPersona.Core.Business.Extension;
using EduPersona.Core.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
}); ;

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Swagger
builder.Services.AddSwaggerWithJwt();

//JWT Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddHostedService<MigrationHostedService>();

builder.Services.AddInfrastructure();
builder.Services.AddBusiness();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();

