using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

using gearApi.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GearDb>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("GearDb")));

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(
            "http://127.0.0.1:5500",
            "http://localhost:5173"
            )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("Frontend");

app.MapGearEndpoints();
app.MapCategoryEndpoints();
app.MapPackingListEndpoints();

app.Run();






