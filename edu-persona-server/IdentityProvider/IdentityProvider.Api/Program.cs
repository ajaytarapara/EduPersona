using AutoMapper;
using EduPersona.Core.Data.Extension;
using IdentityProvider.Api.HostedServices;
using IdentityProvider.Api.Middleware;
using IdentityProvider.Business.Extension;
using IdentityProvider.Business.MappingProfile;
using IdentityProvider.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructure();

builder.Services.AddBusiness();

builder.Services.AddControllers();

builder.Services.AddHostedService<MigrationHostedService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddSwaggerGen();
 

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
