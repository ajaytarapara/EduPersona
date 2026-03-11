using EduPersona.Assesment.Api.Extensions;
using EduPersona.Assesment.Api.HostedServices;
using EduPersona.Assesment.Api.Middleware;
using EduPersona.Assesment.Data;
using Microsoft.EntityFrameworkCore;
using EduPersona.Assesment.Data.Extension;
using EduPersona.Assesment.Business.Extension;

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

builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy", policy => policy.WithOrigins("http://localhost:3006").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

