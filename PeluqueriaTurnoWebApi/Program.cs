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
builder.Services.AddScoped<IValidator<Cliente>, ClienteValidator>();
builder.Services.AddScoped<IValidator<EstadoTurno>,EstadoTurnoValidator>();
builder.Services.AddScoped<IValidator<HistorialTurno>,HistorialTurnoValidator>();
builder.Services.AddScoped<IValidator<MetodoPago>,MetodoPagoValidator>();
builder.Services.AddScoped<IValidator<Pago>,PagoValidator>();
builder.Services.AddScoped<IValidator<Servicio>,ServicioValidator>();
builder.Services.AddScoped<IValidator<TipoServicio>,TipoServicioValidator>();
builder.Services.AddScoped<IValidator<TurnoServicio>,TurnoServicioValidator>();
builder.Services.AddScoped<IValidator<Turno>, TurnoValidator>();

//REPOSITORIES Services
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEstadoTurnoRepository,EstadoTurnoRepository>();
builder.Services.AddScoped<IHistorialTurnoRepository,HistorialTurnoRepository>();
builder.Services.AddScoped<IMetodoPagoRepository,MetodoPagoRepository>();
builder.Services.AddScoped<IPagoRepository,PagoRepository>();
builder.Services.AddScoped<IServicioRepository,ServicioRepository>();
builder.Services.AddScoped<ITipoServicioRepository,TipoServicioRepository>();
builder.Services.AddScoped<ITurnoRepository,TurnoRepository>();
builder.Services.AddScoped<ITurnoServicioRepository,TurnoServicioRepository>();

//SERVICES INYECTIONS 
builder.Services.AddScoped<IClienteService,ClienteService>();
builder.Services.AddScoped<IEstadoTurnoService, EstadoTurnoService>();
builder.Services.AddScoped<IHistorialTurnoService, HistorialTurnoService>();
builder.Services.AddScoped<IMetodoPagoService, MetodoPagoService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddScoped<ITipoServicioService, TipoServicioService>();
builder.Services.AddScoped<ITurnoService, TurnoService>();
builder.Services.AddScoped<ITurnoServicioService, TurnoServicioService>();

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
