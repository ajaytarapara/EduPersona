using EduPersona.Core.Api.HostedServices;
using EduPersona.Core.Api.Middleware;
using EduPersona.Core.Data;
using Microsoft.EntityFrameworkCore;
using EduPersona.Core.Data.Extension;
using EduPersona.Core.Business.Extension;
using EduPersona.Core.Api.Extensions;
using EduPersona.Core.Business.MappingProfile;
using AutoMapper;

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

//mapper 
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddSwaggerGen();

builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy", policy => policy.WithOrigins("http://localhost:3005").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

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

