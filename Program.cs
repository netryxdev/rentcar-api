using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using rentcar_api.Data;
using rentcar_api.Services.JWT;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DotNetEnv.Env.Load();

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
//var connectionString ParseException: 'Parsing failure: unexpected 'J'; expected end of input (Line 5, Column 1); recently consumed: r_db;"= builder.Configuration.GetConnectionString("CONNECTION_STRING");

// Para funcionar rodando o docker, seguir estes passos? 
//https://forums.docker.com/t/container-cannot-connect-to-the-hosts-mysql-database/3493/5

//testar se funciona conectar em banco remoto rodando no docker

// Nao funciona conectar em banco localmente/localhost rodando no docker, so no http/https
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString,
    ServerVersion.AutoDetect(connectionString)));

// Configurando a autenticação JWT
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer, // Aqui
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret!)) // Aqui
    };
});


//Services
builder.Services.AddScoped<IJwtService>(provider =>
{
    var secretKey = jwtSecret; // Adapte conforme necessário
    var expirationMinutes = 60; // Defina conforme desejado
    return new JwtService(secretKey!, expirationMinutes);
});

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
