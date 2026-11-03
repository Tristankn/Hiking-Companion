using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

using gearApi.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GearDb>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("GearDb")));

builder.Services.AddOpenApi();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGearEndpoints();
app.MapCategoryEndpoints();
app.MapPackingListEndpoints();

app.Run();






