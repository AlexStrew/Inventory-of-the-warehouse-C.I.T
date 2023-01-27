using InvAPI.Models;
using InvAPI.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

// For Entity Framework
builder.Services.AddDbContext<InventarisationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("InventarisationDB")));

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.User.RequireUniqueEmail = false;
    //задаем требования для пароля
    opts.Password.RequiredLength = 5;   // минимальная длина
    opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
    opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
    opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
    opts.Password.RequireDigit = false; // требуются ли цифры
})
    .AddEntityFrameworkStores<InventarisationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddMvc();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
