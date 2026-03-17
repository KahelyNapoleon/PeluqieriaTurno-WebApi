using DAL.Data;
using DAL.Identity;
using FluentValidation;
using BLL.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DomainLayer.Models;
using DAL.Repositorios.Interfaces;
using DAL.Repositorios;
using BLL.Services.Interfaces;
using BLL.Services;
using PeluqueriaTurnoWebApi.ContainerGroupRegistration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

//Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//SWAGGER - (DOCUMENTATION)
builder.Services.AddSwaggerGen();

//SERILOG

//FLUENTVALIDATION INYECTIONS
builder.Services.AddValidation();

//REPOSITORIES Services
builder.Services.AddRepositoriesOfDAL();

//SERVICES INYECTIONS 
builder.Services.AddServicesOfBLL();

//Authentication Manner - JWTBearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var secret = builder.Configuration["JwtConfig:Secret"];
    var issuer = builder.Configuration["JwtConfig:ValidIssuer"];
    var audience = builder.Configuration["JwtConfig:ValidAudiences"];

    if (secret is null || issuer is null || audience is null)
    {
    throw new ApplicationException("Jwt is not set in the configuration");
    }
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
    
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
