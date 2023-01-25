using InvAPI.Controllers;
using InvAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<Inventory, InventoriesController>();
//builder.Services.AddDbContext<InventarisationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=InventarisationDB;Data Source=SV-SQL-02\\SVSQL02;TrustServerCertificate=True")));
//builder.Services.AddDbContext<InventarisationDbContext>(options =>
//options.UseSqlServer(Configuration.GetConnectionString("InventarisationDB")));
builder.Services.AddTransient<InventarisationDbContext>();
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

app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
//app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "MyTest Demo"));


app.Run();
